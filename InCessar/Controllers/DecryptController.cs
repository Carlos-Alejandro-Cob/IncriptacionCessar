using Microsoft.AspNetCore.Mvc;
using InCessar.Models;
using System.Text;

namespace InCessar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecryptController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CaesarResponse> Decrypt([FromBody] CaesarRequest request)
        {
            // Corrección de valor nulo
            string input = request?.Text ?? string.Empty;
            int shift = request?.Shift ?? 0;
            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)(((c - baseChar - shift + 26) % 26) + baseChar));
                }
                else
                {
                    result.Append(c);
                }
            }

            return Ok(new CaesarResponse { ResultText = result.ToString() });
        }
    }
}
