using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{

    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestImportController : Controller
    {
        RequestImportService _requestImportService;
        public RequestImportController(RequestImportService requestImportService)
        { 
            _requestImportService = requestImportService;
        }
        [HttpGet("{status}")]
        public async Task <IActionResult>GetListRequest(int status)
        {
            var request = await _requestImportService.getListRequest(status);
            APIResult result = new APIResult();
            if (request != null && request.Any())
            {
                result = new APIResult
                {
                    Data = request,
                    Message = "Successfully",
                    Status = 200
                };
                return Ok(result);
            }
            else
            {
                result = new APIResult
                {

                    Data = null,
                    Message = "Successfully",
                    Status = 200
                };
                return Ok(result);
            }
            }
        [HttpPost]
        public async Task<IActionResult> CreateSupplierLinkAndDetails([FromBody] RequestImport createSupplierDTO)
        {
            if (createSupplierDTO == null)
            {
                return BadRequest("Invalid data.");
            }
            else
            {
                var result = await _requestImportService.CreateSupplierLinkAndDetailsAsync(createSupplierDTO.SupplierLink, createSupplierDTO.SupplierDetails);

                if (result)
                {
                    return Ok("SupplierLink and SupplierDetails created successfully.");
                }
                return BadRequest("Failed to create SupplierLink and SupplierDetails.");
            }

          
        }

    }
}
