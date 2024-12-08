using Cafe_Management.Core.Entities;

namespace Cafe_Management.Infrastructure.Model
{
    public class RequestImport
    {
        public SupplierLinkDTO SupplierLink { get; set; }
        public List<SupplierDetailDTO> SupplierDetails { get; set; }
    }
}
