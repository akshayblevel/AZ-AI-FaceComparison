using AZ_AI_FaceComparison.Interfaces;
using AZ_AI_FaceComparison.Models;
using Microsoft.AspNetCore.Mvc;

namespace AZ_AI_FaceComparison.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceController(ICompareService compareService) : ControllerBase
    {
        [HttpPost("Compare")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CompareFaces([FromForm] UploadFileModel model)
        {
            var result = await compareService.CompareFacesAsync(model.Image1, model.Image2);
            return Ok(result);
        }
    }
}
