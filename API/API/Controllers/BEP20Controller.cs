using Common.Models;
using Logic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("FollowTheTokenAPI")]
    [ApiController]
    public class BEP20Controller : ControllerBase
    {
        private readonly IBEP20Logic _bep20Logic;

        public BEP20Controller(IBEP20Logic bEP20Logic)
        {
            _bep20Logic = bEP20Logic;
        }

        [Route("getBEP20TokenTransactions")]
        [HttpPost]
        public async Task<ActionResult<BEP20TokenTransactionResponse>> GetBEP20TokenTransactions([FromBody] BEP20TokenTransactionRequest request)
        {
            var response = await _bep20Logic.GetBEP20TokenTransactions(request);
            return Ok(response);
        }
    }
}
