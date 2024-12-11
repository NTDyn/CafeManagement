namespace Cafe_Management.Infrastructure.Model
{
    public class UpdateImportRequestDto
    {
        public SupplierLinkUpdateDto supplierLink {get;set ;}
        public List<SupplierDetailUpdateDto> supplierDetail { get; set; }
        
    }
}
