using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Model;

namespace Cafe_Management.Core.Interfaces
{
    public interface IRequestImportRepository
    {
        public Task<IEnumerable<IngredientSupplierLink>> getListRequest(int status);
        public Task<bool> CreateSupplierLinkAndDetailsAsync(SupplierLinkDTO supplierLinkDTO, List<SupplierDetailDTO> supplierDetailDTOs);

    }
}
