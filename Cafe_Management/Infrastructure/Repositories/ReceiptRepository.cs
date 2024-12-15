using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class ReceiptRepository: IReceiptRepository
    {
        private readonly AppDbContext _context;
        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Receipt>> Get(Nullable<int> Receipt_ID = null)
        {
            Expression<Func<Receipt, bool>> _Filter = r => true;

            if (Receipt_ID != null)
            {
                _Filter = Function.AndAlso(_Filter, x => x.Receipt_ID == Receipt_ID);
            }
            List<Receipt> Receipt = await _context.Receipt.Where(_Filter).ToListAsync();
            List<ReceiptDetail> ReceiptDetail = await _context.ReceiptDetail.Where(x => x.IsActive == true).ToListAsync();
            var JoinData = (from h in Receipt
                            join d in ReceiptDetail
                            on h.Receipt_ID equals d.Receipt_ID
                            into groups
                            select new Receipt
                            {
                                Receipt_ID = h.Receipt_ID,
                                Staff_ID = h.Staff_ID,
                                Customer_ID = h.Customer_ID,
                                Status = h.Status,
                                IsActive = h.IsActive,
                                TotalPrice = h.TotalPrice,
                                CreatedDate = h.CreatedDate,
                                ModifiedDate = h.ModifiedDate,
                                Details = groups.ToList()
                            }).ToList();
            return JoinData;
        }

        public async Task Create(Receipt Receipt )
        {
            Receipt.CreatedDate = DateTime.Now;
            Receipt.ModifiedDate = DateTime.Now;
            int TotalPrice = 0;
            var maxId = await _context.Receipt.MaxAsync(p => (int?)p.Receipt_ID) ?? 0;

            int ID = maxId + 1;
            if (Receipt.Details != null &&  Receipt.Details.Count > 0)
            {
                
                foreach (var d in Receipt.Details) {
                    if(d.Quantity > 0)
                    {
                        TotalPrice += d.Quantity * d.Price;
                        d.Receipt_ID = ID;
                        await _context.ReceiptDetail.AddAsync(d);
                        List<ProductRecipe> ProductRecipes = await _context.ProductRecipe.Where(x => x.Product_ID == d.Product_ID).ToListAsync();
                        foreach (var recipe in ProductRecipes) 
                        {

                            double TotalRecipe = 0;
                            Ingredient? ingredient = await _context.Ingredient.FindAsync(recipe.Ingredient_ID);
                            if (ingredient != null)
                            {
                                TotalRecipe = (double)(recipe.Unit == 2 ? (recipe.Unit * ingredient.MaxPerTransfer * ingredient.TransferPerMin) : recipe.Unit == 1 ? (recipe.Quantity * ingredient.TransferPerMin) : recipe.Quantity);
                            }
                            StoreIngredient? storeIngredient = await _context.StoreIngredient.Where(x => x.Ingredient_ID == recipe.Ingredient_ID).SingleOrDefaultAsync();
                            if (storeIngredient != null) 
                            {
                                //TRU KHO
                                double Quan = (double)storeIngredient.Quality - TotalRecipe;
                                storeIngredient.Quality = Quan;
                            }
                            else
                            {
                                StoreIngredient add = new StoreIngredient();
                                add.Warehouse_ID = 0;
                                add.Ingredient_ID = recipe.Ingredient_ID;
                                add.Price = 0;
                                add.Quality = TotalRecipe;
                                await _context.StoreIngredient.AddAsync(add);
                            }
                        }
                    }
                    
                }
                
            }
            Receipt.TotalPrice = TotalPrice;
            if(Receipt.Cuppon_ID != null)
            {
                var cuppon = await _context.Cuppon.FindAsync(Receipt.Cuppon_ID);
                double priceDis = 0.0;
                if(cuppon.Cuppon_Type == 0)
                {
                    priceDis = (double)((double)cuppon.Disscount / 100 * Receipt.TotalPrice);
                }
                if(cuppon.Cuppon_Type == 1)
                {
                    priceDis = (double)cuppon.Disscount;
                }
                HistoryDisscount hd = new HistoryDisscount
                {
                    Customer_ID = (int) Receipt.Customer_ID,
                    Cuppon_ID = (int)cuppon.Cuppon_ID,
                    PriceDisscount = (int)priceDis,
                    Receipt_ID =(int) ID,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                await _context.HistoryDisscount.AddAsync(hd);
                Receipt.TotalPrice =(int) (TotalPrice - priceDis);
            }
            
            await _context.Receipt.AddAsync(Receipt);
            await _context.SaveChangesAsync();
        }

        public async Task CreateReceiptCheckout(Receipt receipt)
        {
           
            var newReceipt = new Receipt
            {
                Staff_ID = receipt.Staff_ID,
                Customer_ID = receipt.Customer_ID,
                TotalPrice = receipt.TotalPrice,
                Status = 1,
                IsActive = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Details = receipt.Details
            };
            await _context.Receipt.AddAsync(newReceipt);
            await _context.SaveChangesAsync();

           
            if (receipt.Details != null && receipt.Details.Count > 0)
            {

                foreach(var item in receipt.Details)
                {
                    var receiptDetal = new ReceiptDetail
                    {
                        Receipt_ID = (int)newReceipt.Receipt_ID,
                        Product_ID = item.Product_ID,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        IsActive = true,
                        status=1,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    await _context.ReceiptDetail.AddAsync(receiptDetal);
                    

                }
                await _context.SaveChangesAsync();
            }
          }







        public async Task CreateCart(ReceiptDetail receiptDetail, int customerId)
        {

            // Kiểm tra giỏ hàng hiện tại của khách hàng
            var existingCart = await _context.Receipt.FirstOrDefaultAsync(r => r.Customer_ID == customerId && r.Status == 0);

            if (existingCart == null)
            {
                // Tạo giỏ hàng mới nếu chưa tồn tại
                var newReceipt = new Receipt
                {
                    Staff_ID = 0,
                    Customer_ID = customerId,
                    TotalPrice = receiptDetail.Price * receiptDetail.Quantity,
                    Status = 0,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Details = new List<ReceiptDetail> { receiptDetail }
                };

                // Thêm giỏ hàng mới vào cơ sở dữ liệu
                await _context.Receipt.AddAsync(newReceipt);
                await _context.SaveChangesAsync();

                // Thêm chi tiết hóa đơn (sản phẩm) vào giỏ hàng mới
                receiptDetail.Receipt_ID = (int) newReceipt.Receipt_ID;
                receiptDetail.CreatedDate = DateTime.Now;
                receiptDetail.ModifiedDate = DateTime.Now;
                receiptDetail.IsActive = true;
                receiptDetail.status = 0;

                await _context.ReceiptDetail.AddAsync(receiptDetail);
            }
            else
            {
                // Lấy danh sách chi tiết hóa đơn  của giỏ hàng hiện tại
                List<ReceiptDetail> listDetail = await _context.ReceiptDetail
                    .Where(r => r.Receipt_ID == existingCart.Receipt_ID && r.status == 0)
                    .ToListAsync();

                // Kiểm tra nếu sản phẩm đã tồn tại trong giỏ hàng
                var existingDetail = listDetail?.FirstOrDefault(d => d.Product_ID == receiptDetail.Product_ID);

                if (existingDetail != null)
                {
                    // Cập nhật số lượng và giá sản phẩm nếu đã có trong giỏ hàng
                    existingDetail.Quantity += receiptDetail.Quantity;
                    existingDetail.Price = receiptDetail.Price;
                    existingDetail.status = 0;
                    existingDetail.ModifiedDate = DateTime.Now;

                    _context.Entry(existingDetail).State = EntityState.Modified;
                }
                else
                {
                    // Thêm sản phẩm mới vào giỏ hàng nếu chưa có
                    receiptDetail.Receipt_ID =(int) existingCart.Receipt_ID;
                    receiptDetail.CreatedDate = DateTime.Now;
                    receiptDetail.ModifiedDate = DateTime.Now;
                    receiptDetail.IsActive = true;
                    receiptDetail.status = 0;

                    await _context.ReceiptDetail.AddAsync(receiptDetail);
                }

         
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();


        }

    

       public async Task<IEnumerable<CartDto>> GetCartByIdCustomer(int id)
        {
             List<Receipt> receipt = await _context.Receipt.Where(r => r.Customer_ID == id && r.Status == 0).ToListAsync();
            List<ReceiptDetail> detailReceipt = await _context.ReceiptDetail.Where(r => r.status == 0).ToListAsync();
            List<Product> product = await _context.Products.ToListAsync();
            var JoinData = (from h in receipt
                            join d in detailReceipt on h.Receipt_ID equals d.Receipt_ID
                            join p in product on d.Product_ID equals p.Product_ID
                            select new CartDto
                            {
                                Receipt_ID = h.Receipt_ID,
                                Customer_ID = h.Customer_ID,
                                Status = h.Status,
                                CreatedDate = h.CreatedDate,
                                ModifiedDate = h.ModifiedDate,
                                Price = (int)p.Price,
                                ProductName = p.Product_Name,
                                Product_ID = d.Product_ID,
                                Quantity = d.Quantity,
                                Product_Image=p.Product_Image,
                                Detail_ID =d.Detail_ID
                            }).ToList();
            return JoinData;
        }

        public async Task <IEnumerable<CartDto>> GetDetailReceiptById(int id)
        {

            List<Receipt> receipt = await _context.Receipt.Where(r =>r.Status!=0 && r.Receipt_ID==id).ToListAsync();
            List<ReceiptDetail> detailReceipt = await _context.ReceiptDetail.Where(r => r.status!=0&&r.Receipt_ID==id).ToListAsync();
            List<Product> product = await _context.Products.ToListAsync();
            var JoinData = (from h in receipt
                            join d in detailReceipt on h.Receipt_ID equals d.Receipt_ID
                            join p in product on d.Product_ID equals p.Product_ID
                            select new CartDto
                            {
                                Receipt_ID = h.Receipt_ID,
                                Customer_ID = h.Customer_ID,
                                Status = h.Status,
                                CreatedDate = h.CreatedDate,
                                ModifiedDate = h.ModifiedDate,
                                Price = (int)p.Price,
                                ProductName = p.Product_Name,
                                Product_ID = d.Product_ID,
                                Quantity = d.Quantity,
                                Product_Image = p.Product_Image,
                                Detail_ID = d.Detail_ID
                            }).ToList();
            return JoinData;
        }

        public async Task ChangeQuantity(int id, int quantity)
        {
            ReceiptDetail findDetailReceipt = await _context.ReceiptDetail.FirstOrDefaultAsync(d => d.Detail_ID == id);
            if (findDetailReceipt != null)
            {
                if(findDetailReceipt.Quantity+quantity <= 0)
                {
                    _context.ReceiptDetail.Remove(findDetailReceipt);
                   
                }
                else
                {
                    findDetailReceipt.Quantity = findDetailReceipt.Quantity + quantity;
                    _context.ReceiptDetail.Update(findDetailReceipt);
                }
              
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDetailReceipt(int id)
        {
            ReceiptDetail findDetailReceipt = await _context.ReceiptDetail.FirstOrDefaultAsync(d => d.Detail_ID == id);
            if (findDetailReceipt != null)
            {
                _context.ReceiptDetail.Remove(findDetailReceipt);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CheckoutFromCart(Receipt receipt, List<ReceiptDetail> receiptDetail)
        {
            Receipt findReceipt = await _context.Receipt.FirstOrDefaultAsync(r => r.Receipt_ID == receipt.Receipt_ID);
            if (receipt != null)
            {
                findReceipt.Status = receipt.Status;
                _context.Receipt.Update(findReceipt);
              foreach(var item in receiptDetail)
                {
                    ReceiptDetail findDetail = await _context.ReceiptDetail.FirstOrDefaultAsync(d => d.Detail_ID == item.Detail_ID);
                    if (findDetail != null)
                    {
                        findDetail.status = item.status;
                        _context.ReceiptDetail.Update(findDetail);
                    }
                }
                await _context.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<ReceiptDto>> getReceiptByStatus(int status)
        {
            List<Receipt> Receipt = await _context.Receipt.Where(r=>r.Status==status).ToListAsync();
            List<Customer> Customer = await _context.Customer.Where(c => c.IsActive == true).ToListAsync();
            List<ReceiptDetail> receiptDetail = await _context.ReceiptDetail.Where(r => r.status == status).ToListAsync();
            var JoinData = (from r in receiptDetail
                            join h in Receipt on r.Receipt_ID equals h.Receipt_ID
                            join c in Customer on h.Customer_ID equals c.Customer_Id
                            group r by new
                            {
                                h.Receipt_ID,
                                h.Staff_ID,
                                h.Customer_ID,
                                h.Status,
                                h.IsActive,
                                h.CreatedDate,
                                h.ModifiedDate,
                                c.Customer_Name
                            } into grp
                            select new ReceiptDto
                            {
                                Receipt_ID = grp.Key.Receipt_ID,
                                Staff_ID = grp.Key.Staff_ID,
                                Customer_ID = grp.Key.Customer_ID,
                                Status = grp.Key.Status,
                                IsActive = grp.Key.IsActive,
                                TotalPrice = grp.Sum(x => x.Price*x.Quantity), 
                                CreatedDate = grp.Key.CreatedDate,
                                ModifiedDate = grp.Key.ModifiedDate,
                                customer_Name = grp.Key.Customer_Name
                            }).ToList();
            return JoinData;
        }
















        //public async Task Update(Receipt Receipt)
        //{
        //    var existing = await _context.Receipt.SingleOrDefaultAsync(x => x.Receipt_ID == Receipt.Receipt_ID);
        //    if (existing != null)
        //    {


        //        existing.ModifiedDate = DateTime.Now;
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
