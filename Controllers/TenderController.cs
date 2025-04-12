using Microsoft.AspNetCore.Mvc;
using BidingManagementSystem.Application.DTOs.Tender;
using BidingManagementSystem.Application.Services;
using BidingManagementSystem.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;

namespace BidingManagementSystem.API.Controllers
{  
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TenderController : ControllerBase
    {
        private readonly ITenderService _tenderService;

        public TenderController(ITenderService tenderService)
        {
            _tenderService = tenderService;
        }

        // إنشاء عطاء جديد
      [Authorize(Roles = "ProcurementOfficer")]
        [HttpPost]
        public async Task<IActionResult> CreateTender([FromForm] CreateTenderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tenderService.CreateTenderAsync(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
         [Authorize(Roles = "Admin,Procurement")]
         [HttpGet]
        public async Task<IActionResult> GetAllTenders()
        {
            var result = await _tenderService.GetAllTendersAsync();
            return Ok(result);
        }
     [Authorize(Roles = "Admin,Procurement")]
     [HttpPut("{tenderId}")]
     public async Task<IActionResult> UpdateTender(int tenderId, [FromBody] UpdateTenderDto request)
    {
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var result = await _tenderService.UpdateTenderAsync(tenderId, request);

    if (!result.Success)
        return NotFound(result.Message);

    return Ok(result);
    }



[Authorize(Roles = "ProcurementOfficer")]
[HttpDelete("{tenderId}")]
public async Task<IActionResult> Delete(int tenderId)
{
    var result = await _tenderService.DeleteTenderAsync(tenderId);

    if (!result.Success)
        return BadRequest(result.Message);

    return Ok(result);
}
[HttpGet("open")]
[Authorize(Roles = "Bidder")]
public async Task<IActionResult> GetOpenTenders()
{
    var result = await _tenderService.GetOpenTendersAsync();
    return Ok(result);
}




    }
}
