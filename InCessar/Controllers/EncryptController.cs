using Microsoft.AspNetCore.Mvc;
using InCessar.Models;
using System.Text;

namespace InCessar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncryptController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CaesarResponse> Encrypt([FromBody] CaesarRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Text))
            {
                return BadRequest("El texto y el desplazamiento son requeridos.");
            }

            // Aseguramos que el desplazamiento sea positivo y dentro del rango del alfabeto
            int shift = request.Shift % 26;
            if (shift < 0)
            {
                shift += 26;
            }

            var result = new StringBuilder();

            foreach (char c in request.Text)
            {
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)(((c - baseChar + shift) % 26) + baseChar));
                }
                else
                {
                    result.Append(c); // Mantener caracteres no alfabéticos
                }
            }

            return Ok(new CaesarResponse { ResultText = result.ToString() });
        }
    }
}