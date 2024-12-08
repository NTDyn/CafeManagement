using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{
    public class StaffGroupController:ControllerBase
    {
        private readonly StaffGroupService _staffGroupService;
        public StaffGroupController(StaffGroupService staffGroupService)
        {
            _staffGroupService = staffGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Nullable<int> StaffGroup_ID,
                                                Nullable<bool> IsActive)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _staffGroupService.Get(StaffGroup_ID, IsActive);


                if (data != null)
                {
                    result.Data = data;
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
        public async Task<IActionResult> Create([FromBody] StaffGroup staffGroup)
        {
            APIResult result = new APIResult();
            try
            {
                if (staffGroup.StaffGroup_Name == null)
                {
                    result.Status = 0;
                    result.Message = "StaffGroup_Name can not be empty";
                    return BadRequest(result);
                }
                

                await _staffGroupService.Create(staffGroup);
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(Get), new { id = staffGroup.StaffGroup_ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StaffGroup staffGroup)
        {
            APIResult result = new APIResult();
            try
            {
                if (staffGroup.StaffGroup_ID == null)
                {
                    result.Status = 0;
                    result.Message = "StaffGroup_ID cannot be empty";
                    return BadRequest(result);
                }
                await _staffGroupService.Update(staffGroup);
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            result.Status = 200;
            result.Message = "Successfully";
            return Ok(result);
        }
    }
}
