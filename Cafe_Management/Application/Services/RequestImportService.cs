using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Model;

namespace Cafe_Management.Application.Services
{
    public class RequestImportService
    {
        private readonly IRequestImportRepository _requestImportRepository;
        public RequestImportService(IRequestImportRepository requestImportRepository)
        {
            _requestImportRepository = requestImportRepository;
        }
        public async Task<IEnumerable<IngredientSupplierLink>> getListRequest(int status)
        {
            return await _requestImportRepository.getListRequest(status);
        }
        public async Task<bool> CreateSupplierLinkAndDetailsAsync(SupplierLinkDTO supplierLinkDTO, List<SupplierDetailDTO> supplierDetailDTOs)
        {
            return await _requestImportRepository.CreateSupplierLinkAndDetailsAsync(supplierLinkDTO, supplierDetailDTOs);
        }
    }
}
