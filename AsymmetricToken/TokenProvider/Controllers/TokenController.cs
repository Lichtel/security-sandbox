using System;
using Microsoft.AspNetCore.Mvc;
using TokenProvider.Dto;
using TokenProvider.Logic;

namespace TokenProvider.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenLogic _tokenLogic;
        private readonly ICryptoLogic _cryptoLogic;

        public TokenController(ITokenLogic tokenLogic, ICryptoLogic cryptoLogic)
        {
            _tokenLogic = tokenLogic;
            _cryptoLogic = cryptoLogic;
        }
        
        [HttpGet]
        public ActionResult<TokenResponse> Get()
        {
            var token = _tokenLogic.CreateToken();
            
            var result = new TokenResponse
            {
                Jwt = token.Jwt,
                ExpiresAt = new DateTimeOffset(token.ExpiresAt).ToUnixTimeMilliseconds()
            };

            return Ok(result);
        }

        [HttpGet("public-key")]
        public ActionResult<KeyResponse> GetPublicKey()
        {
            var publicKey = _cryptoLogic.GetPublicKey();

            var result = new KeyResponse
            {
                Key = publicKey
            };

            return result;
        }
    }
}