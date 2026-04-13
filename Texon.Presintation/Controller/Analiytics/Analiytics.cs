using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Texon.Service.Abstraction.IService;
using Texon.Shared.AnaliyticsVeiwModel;

namespace Texon.Presentation.Controller.Analiytics
{
    public class Analiytics(IAnaliytics analiytics) : ApiBaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAnaliytics()
        {
            var result = await analiytics.GetAnaliyticsData();
            return Ok(result);
        }

    }
}
