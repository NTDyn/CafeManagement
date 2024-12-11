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
        public async Task<IActionResult> GetListRequest(int status)
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
        [HttpGet("id")]
        public async Task<IActionResult> GetListDetai(int id)
        {
            try
            {
                var listDetail = await _requestImportService.getListRequestDetail(id);
                var result = new APIResult();
                if (listDetail != null && listDetail.Any())
                {
                    result = new APIResult
                    {
                        Data = listDetail,
                        Message = "successful",
                        Status = 200
                    };
                    return Ok(result);
                }
                else
                {
                    result = new APIResult
                    {
                        Data = null,
                        Message = "successful",
                        Status = 200
                    };
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.InnerException?.Message,
                    Status = 400

                });
            }
        }
        [HttpGet("id_link")]
        public async Task<IActionResult>getRequest(int id)
        {
            try
            {
                var request = await  _requestImportService.getRequest(id);
                var result = new APIResult();
                if (request != null)
                {
                    result = new APIResult
                    {
                        Data = request,
                        Message = "successful",
                        Status = 200
                    };
                    return Ok(result);
                }
                else
                {
                    result = new APIResult
                    {
                        Data = null,
                        Message = "Successful",
                        Status = 200
                    };
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.InnerException?.Message,
                    Status = 400
                });
            }
        }

        [HttpGet("detaillink")]
        public async Task<IActionResult>getListImportDetail(int id)
        {
            try
            {
                var listImportDetail =await _requestImportService.getDetailImportwithIngredient(id);
                APIResult result = new APIResult();
                if (listImportDetail != null)
                {
                    result = new APIResult
                    {
                        Data = listImportDetail,
                        Message = "successful",
                        Status = 200
                    };
                    return Ok(result);
                }
                else
                {

                    result = new APIResult
                    {
                        Data = null,
                        Message = "successful",
                        Status = 200
                    };
                    return Ok(result);
                }
            }catch(Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.InnerException?.Message,
                    Status = 400
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusRequestDto requestDto)
        {
            try
            {
                await _requestImportService.ChangeStatusRequestImport(requestDto.id, requestDto.status,requestDto.id_staff);
                return Ok(new APIResult
                {
                    Message = "Successful",
                    Status = 200
                });

            }
            catch (Exception ex) {
                return BadRequest(new APIResult
                {
                    Message = ex.InnerException?.Message,
                    Status = 400
                });
            }
        }
        [HttpPut("updatedetail")]
        public async Task<IActionResult> UpdateDetailSupplierIngredient([FromBody] UpdateImportRequestDto requestDto)
        {
            try
            {
                var update = await _requestImportService.UpdateDetailSupplierandIngredient(requestDto.supplierLink, requestDto.supplierDetail);
                if (update == true)
                {
                    return Ok(new APIResult
                    {
                        Message = "update successful",
                        Status = 200

                    });
                }
                else
                {
                    return Ok(new APIResult
                    {
                        Message = "some thing went wrong",
                        Status = 200
                    });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new APIResult
                {
                    Message = ex.InnerException?.Message,
                    Status = 400
                });
            }
        }
    }
}
