using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Infrastructure.Model;
using DelegateDecompiler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class RequestImportRepository : IRequestImportRepository
    {
        private readonly AppDbContext _appDbContext;
   
        public RequestImportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
           
        }

        public async Task<bool> CreateSupplierLinkAndDetailsAsync(SupplierLinkDTO supplierLinkDTO, List<SupplierDetailDTO> supplierDetailDTOs)
        {

            var supplierLink = new IngredientSupplierLink
            {
                Supplier_ID = supplierLinkDTO.SupplierID,
                StaffRequest_ID = supplierLinkDTO.StaffRequestID,
                StaffApproved_ID = supplierLinkDTO.StaffApprovedID,
                StaffReceived_ID = supplierLinkDTO.StaffReceivedID,
                TotalPrice = supplierLinkDTO.TotalPrice,
                Warehouse_ID = supplierLinkDTO.WarehouseID,
                IsActive = supplierLinkDTO.IsActive,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };
            await _appDbContext.IngredientSupplierLink.AddAsync(supplierLink);
           await _appDbContext.SaveChangesAsync();
            foreach(var supplierDetailDTO in supplierDetailDTOs)
            {
                var supplierDetail = new IngredientSupplierDetail
                {
                    Header_ID = supplierLink.Link_ID,  // Gắn LinkID từ SupplierLink
                    Ingredient_ID = supplierDetailDTO.IngredientID,
                    Price = supplierDetailDTO.Price,
                    Quality = supplierDetailDTO.Quality,
                    Unit = supplierDetailDTO.Unit,
                    IsActive = supplierDetailDTO.IsActive,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };
              await  _appDbContext.IngredientSupplierDetail.AddAsync(supplierDetail);
            }
          await  _appDbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<DetaiImportWithIngredientDto>> getDetailwithIgrdient(int id)
        {
            var query = from detail in _appDbContext.IngredientSupplierDetail
                        join ingredient in _appDbContext.Ingredient on detail.Ingredient_ID equals ingredient.Ingredient_ID
                       where  detail.Header_ID == id
                        select new DetaiImportWithIngredientDto
                        {
                            Detail_ID = detail.Detail_ID,
                            Header_ID = detail.Header_ID,
                            Ingredient_ID = (int)ingredient.Ingredient_ID,
                            Price = detail.Price,
                            Quality = detail.Quality,
                            Ingredient_Name = ingredient.Ingredient_Name,
                            Unit = detail.Unit,
                            Unit_Transfer = ingredient.Unit_Transfer,
                            Unit_Min = ingredient.Unit_Min,
                            Unit_Max = ingredient.Unit_Max
                        };
            return await query.ToListAsync();

        }


        public async Task<IEnumerable<IngredientSupplierLink>> getListRequest(int status)
        {
            var getListRequest =await _appDbContext.IngredientSupplierLink
                                            .Where(x => x.Status == status).ToListAsync();
            return getListRequest;
        }

        public async Task<IEnumerable<IngredientSupplierDetail>> getListRequestDetail(int id)
        {
            return  await _appDbContext.IngredientSupplierDetail.Where(x => x.Header_ID == id).ToListAsync();
            
        }

        public async Task<IngredientSupplierLink> getRequest(int id)
        {
            return await _appDbContext.IngredientSupplierLink.FindAsync(id);
        }

        public async Task updateStatusImportDetail(int id, int status, int id_staff)
        {
            var request =await _appDbContext.IngredientSupplierLink.FindAsync(id);
            if (request == null)
            {
                throw new KeyNotFoundException("Not Found");
            }

            request.Status = status;
            if (status == 1 )
            {
                request.StaffApproved_ID = id_staff;
            }
            else if (status == 3)
            {
                request.StaffReceived_ID = id_staff;
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateSupplierLinkAndDetailAsync(SupplierLinkUpdateDto supplierLinkDto, List<SupplierDetailUpdateDto> supplierDetailDTOs)
        {
            var link = await _appDbContext.IngredientSupplierLink.FindAsync(supplierLinkDto.link_ID);
            if (link == null)
            {
                throw new Exception("Invoice not found.");
            }

            bool isUpdated = false;

            foreach (var item in supplierDetailDTOs)
            {
                var findDetail = await _appDbContext.IngredientSupplierDetail.FindAsync(item.detail_Id);
                if (findDetail != null)
                {
                    // Đảm bảo rằng đối tượng đang được theo dõi
                    _appDbContext.IngredientSupplierDetail.Attach(findDetail);

                    // Kiểm tra và cập nhật nếu có thay đổi
                    if (findDetail.Unit != item.Unit)
                    {
                        findDetail.Unit = item.Unit;
                        isUpdated = true;
                    }

                    if (findDetail.Quality != item.Quality)
                    {
                        findDetail.Quality = item.Quality;
                        isUpdated = true;
                    }

                    if (findDetail.Price != item.Price)
                    {
                        findDetail.Price = item.Price;
                        isUpdated = true;
                    }

                    // Đánh dấu đối tượng là đã thay đổi
                    if (isUpdated)
                    {
                        _appDbContext.Entry(findDetail).State = EntityState.Modified;
                        await _appDbContext.SaveChangesAsync();
                    }
                }
            }

            if (isUpdated)
            {
               List<DetaiImportWithIngredientDto>  detail = await getDetailwithIgrdient(link.Link_ID);
                foreach (var item in detail)
                {
                   
                    var store = new StoreIngredient
                    {
                        Warehouse_ID = 1,
                        Ingredient_ID = item.Ingredient_ID,
                        Price = item.Price,
                        Quality = item.Quality,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    await _appDbContext.StoreIngredient.AddAsync(store);
                    await _appDbContext.SaveChangesAsync();


                }
                // Tính lại tổng tiền
                link.TotalPrice = (int) await _appDbContext.IngredientSupplierDetail.Where(d => link.Link_ID == d.Header_ID).SumAsync(d => d.Quality * d.Price);
                link.Status = 3;
                link.StaffReceived_ID = supplierLinkDto.StaffReceivedID;
                
                // Lưu các thay đổi
               await _appDbContext.SaveChangesAsync();
            
               

                
               
                return true;
            }

            return false; // Trả về false nếu không có thay đổi
        }


    }
}
