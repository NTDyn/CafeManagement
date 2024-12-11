using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Model;

namespace Cafe_Management.Core.Interfaces
{
    public interface IRequestImportRepository
    {
        public Task<IEnumerable<IngredientSupplierLink>> getListRequest(int status);
        public Task<bool> CreateSupplierLinkAndDetailsAsync(SupplierLinkDTO supplierLinkDTO, List<SupplierDetailDTO> supplierDetailDTOs);
        public Task<IEnumerable<IngredientSupplierDetail>> getListRequestDetail(int id);

        public Task<IngredientSupplierLink> getRequest(int id);
        public Task<List<DetaiImportWithIngredientDto>> getDetailwithIgrdient(int id);

        public Task updateStatusImportDetail(int id, int status, int id_staff);

        public Task<bool> UpdateSupplierLinkAndDetailAsync(SupplierLinkUpdateDto supplierLinkDto, List<SupplierDetailUpdateDto> supplierDetailDTOs);
        
    }
}
