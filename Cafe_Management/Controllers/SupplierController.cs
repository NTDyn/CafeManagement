using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController :ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SupplierController (SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers() {
            var suppliers  = await _supplierService.GetAllSuppliers();
            APIResult result = new APIResult();
            if (suppliers != null && suppliers.Any())
            {
               result = new APIResult
                {
                    Data = suppliers,
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
        public async Task<IActionResult> AddSupplier ([FromBody] Supplier supplier)
        {
            try
            {
                await _supplierService.AddSupplier(supplier);
                APIResult result = new APIResult
                {
                    Data = supplier, // Set the added product as data
                    Message = "Successfully added the supplier",
                    Status = 200
                };
                return Ok(result);

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


     
        [HttpPut]
        public async Task<IActionResult> UpdateSupplier([FromBody]Supplier supplier)
        {
            try
            {
           
                    await _supplierService.UpdateSupplier(supplier);
                    APIResult result = new APIResult

                    {
                        Data = supplier,
                        Message = "Successfully",
                        Status = 200
                    };
                    return Ok(result);
                
            }
            catch(Exception e)
            {
                return BadRequest(new APIResult
                {
                    Message = e.Message,
                    Status = 400
                });
            }
        }
        [HttpGet("active")]
        public async Task<IActionResult> getSupplierActive()
        {
            try
            {
                var suppliers = await _supplierService.getSupplierActive();
                APIResult result = new APIResult();

                if (suppliers != null && suppliers.Any())
                {
                    result = new APIResult
                    {
                        Data = suppliers,
                        Message = "successfuly",
                        Status = 200
                    };
                    return Ok(result);
                }
                else
                {
                    result = new APIResult
                    {
                        Data = null,
                        Message = "Seccessfull",
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
        [HttpGet("id")]
        public async Task<IActionResult>getSupplierById(int id)
        {
            try
            {
                var supplier = await _supplierService.getOneSupplier(id);
                APIResult result = new APIResult();
                if (supplier != null)
                {
                    result = new APIResult
                    {
                        Data = supplier,
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
                    Data = 200
                });
            }
        }
        }
    }
