using BCA007.Shared.Service.Library;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.Library
{
    public class BookIssueHistoryController : ControllerBase
    {
        private readonly IBookIssueHistoryViewService _serv;
        public BookIssueHistoryController(IBookIssueHistoryViewService serv)
        {
            _serv = serv;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _serv.GetAllAsync());
        }
    }
}
