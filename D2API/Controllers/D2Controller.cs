using D2API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace D2API.Controllers
{
    [ApiController]
    [Route("/{b64input}")]
    public class D2Controller : Controller
    {
        private readonly ID2Service _d2Service;
        private readonly IStringInputService _stringInputService;

        public D2Controller(
            ID2Service d2Service,
            IStringInputService stringInputService)
        {
            _d2Service = d2Service;
            _stringInputService = stringInputService;
        }
        [HttpGet]
        public IActionResult D2(string b64input)
        {
            string response = "";

            try
            {
                var decode = Base64UrlTextEncoder.Decode(b64input);

                var input = _stringInputService.DecompressString(decode);

                response = _d2Service.CreateSVG(input);
            }
            catch (Exception ex)
            {
                response = _d2Service.CreateSVG($"Something -> Horribly Wrong: {ex.Message}");
            }

            return Content(response, "image/svg+xml");
        }
    }
}
