using Common.Models;
using Logic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("FollowTheTokenAPI")]
    [ApiController]
    public class Bep20Controller : ControllerBase
    {
        private readonly IBep20Logic _bep20Logic;

        public Bep20Controller(IBep20Logic bEP20Logic)
        {
            _bep20Logic = bEP20Logic;
        }

        [Route("getBEP20TokenTransactions")]
        [HttpPost]
        public async Task<ActionResult<Bep20TokenTransactionResponse>> GetBEP20TokenTransactions([FromBody] Bep20TokenTransactionRequest request)
        {
            var response = await _bep20Logic.GetBep20TokenTransactions(request);
            return Ok(response);
        }
    }
}
