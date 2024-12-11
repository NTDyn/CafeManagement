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
        public async Task<IEnumerable<IngredientSupplierDetail>>getListRequestDetail(int id)
        {
            return await _requestImportRepository.getListRequestDetail(id);
        }

        public async Task<IngredientSupplierLink>getRequest(int id)
        {
            return await _requestImportRepository.getRequest(id);
        }
        public async Task<List<DetaiImportWithIngredientDto>>getDetailImportwithIngredient(int id)
        {
            return await _requestImportRepository.getDetailwithIgrdient(id);
        }


        public async Task ChangeStatusRequestImport(int id, int status,int id_staff)
        {
                await _requestImportRepository.updateStatusImportDetail(id, status,id_staff);
        }
        public async Task<bool> UpdateDetailSupplierandIngredient(SupplierLinkUpdateDto supplierLinkDto, List<SupplierDetailUpdateDto> supplierDetailDtos)
        {
            return await _requestImportRepository.UpdateSupplierLinkAndDetailAsync(supplierLinkDto, supplierDetailDtos);
        }
    }

   
}
