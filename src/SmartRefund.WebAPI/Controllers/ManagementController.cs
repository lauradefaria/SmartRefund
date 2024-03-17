﻿using Microsoft.AspNetCore.Mvc;
using SmartRefund.Application.Interfaces;
using SmartRefund.ViewModels.Requests;
using SmartRefund.WebAPI.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartRefund.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ManagementController : Controller
    {
        private readonly IInternalAnalyzerService _analyzerService;

        public ManagementController(IInternalAnalyzerService analyzerService)
        {
            _analyzerService = analyzerService;
        }

        [HttpGet("receipts/submitted")]
        [SwaggerOperation("Get all Receipts with submitted Status")]
        [TypeFilter(typeof(CombinedAuthorizationFilter))]
        public async Task<IActionResult> GetAllByStatus()
        {
            var receipts = await _analyzerService.GetAllByStatus();
            if (receipts != null && receipts.Count() != 0)
                return Ok(receipts);

            return NotFound();
        }

        [HttpPatch]
        [Route("update-status")]
        [SwaggerOperation("Update TranslatedVisionReceipt's Status.")]
        [TypeFilter(typeof(AuthorizationFilterFinance))]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateTVRStatusRequest updateRequest)
        {
            var UpdatedObject = await _analyzerService.UpdateStatus(updateRequest.Id, updateRequest.NewStatus);
            return Ok(UpdatedObject);
        }

        // Apenas para visualização
        [HttpGet("receipts")]
        [TypeFilter(typeof(CombinedAuthorizationFilter))]
        public async Task<IActionResult> GetAll()
        {
            var receipts = await _analyzerService.GetAll();
            //if (receipts != null && receipts.Count() != 0)
            return Ok(receipts);

            //return NotFound();
        }

    }
}
