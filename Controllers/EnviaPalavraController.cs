using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitGame.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EnviaPalavraController : ControllerBase
    {
        readonly IRequestClient<IEnviaPalavraRequest> _requestClient;

        public EnviaPalavraController(IRequestClient<IEnviaPalavraRequest> requestClient)
        {
            _requestClient = requestClient;
        }

        [HttpGet]
        public async Task <IActionResult> AdivinhaPalavra(string letraOuPalavra)
        {

            var (status, notFound) = await _requestClient.GetResponse<IEnviaPalavraResponse, IEnviaPalavraNotFound>(new { palavra = letraOuPalavra});

            if (status.IsCompletedSuccessfully)
            {
                var response = await status;
                return Ok(response.Message);
            }
            else
            {
                var response = await notFound;
                return NotFound(response.Message);
            }
        }
    }
}
