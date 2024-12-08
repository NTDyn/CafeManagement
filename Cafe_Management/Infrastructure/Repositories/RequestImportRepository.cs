using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Infrastructure.Model;
using DelegateDecompiler;
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

        public async Task<IEnumerable<IngredientSupplierLink>> getListRequest(int status)
        {
            var getListRequest =await _appDbContext.IngredientSupplierLink
                                            .Where(x => x.Status == status).ToListAsync();
            return getListRequest;
        }
    }
}
