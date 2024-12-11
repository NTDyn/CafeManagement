using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;
        public WarehouseController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouse(Nullable<int> warehouse_ID, Nullable<bool> IsActive)
        {
            APIResult result = new APIResult();
            try
            {
                var warehouses = await _warehouseService.GetWarehouse(warehouse_ID, IsActive);


                if (warehouses != null)
                {
                    result.Data = warehouses;
                    result.Message = "Successfully";
                    result.Status = 200;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = 0;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehouse([FromBody] Warehouse warehouse)
        {
            try
            {
                if (warehouse.WareHouse_Name == null)
                {
                    return BadRequest("Warehouse name can not be empty");
                }

                await _warehouseService.AddWarehouse(warehouse);
                return CreatedAtAction(nameof(AddWarehouse), new { wareHouse_ID = warehouse.WareHouse_ID }, warehouse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWarehouse([FromBody] Warehouse warehouse)
        {
            try
            {
                if (warehouse.WareHouse_ID == null)
                {
                    return BadRequest("Warehouse ID can not be empty");
                }
                await _warehouseService.UpdateWarehouse(warehouse);
                var result = await _warehouseService.GetWarehouse(warehouse.WareHouse_ID, null);
                if (result.Count() == 0)
                {
                    return BadRequest("Warehouse id does not exist");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("active")]
        public async Task<IActionResult> getWareHouseActive()
        {
            
            try
            {
                var warehouse = await _warehouseService.GetWareHouseActive();
                APIResult result = new APIResult();

                if (warehouse!=null && warehouse.Any())
                {
                    result = new APIResult
                    {
                        Data = warehouse,
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
                        Message = "Successfuly",
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
        public async Task<IActionResult>getWareHouseById(int id)
        {
            try
            {
                var warehouse = await _warehouseService.getWareHouseById(id);
                APIResult result = new APIResult();
                if (warehouse != null)
                {
                    result = new APIResult
                    {
                        Data = warehouse,
                        Message = "Successful",
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
            }catch(Exception ex)
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
