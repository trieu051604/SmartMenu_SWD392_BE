using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.Services;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview()
        {
            var data = await _service.GetSystemOverviewAsync();
            return Ok(data);
        }

        [HttpGet("devices")]
        public async Task<IActionResult> GetDevicesReport()
        {
            var data = await _service.GetDeviceStatusReportAsync();
            return Ok(data);
        }

        [HttpGet("schedules")]
        public async Task<IActionResult> GetSchedulesReport()
        {
            var data = await _service.GetScheduleReportAsync();
            return Ok(data);
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetUserActionsReport()
        {
            var data = await _service.GetUserActionsReportAsync();
            return Ok(data);
        }
    }
}
