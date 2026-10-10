using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoData _productoData;

        public ProductoController(ProductoData productoData)
        {
            _productoData = productoData;
        }

        // 1. GET: api/Producto
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<ProductoDetalle> Lista = await _productoData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        // 2. GET: api/Producto/5
        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(int Id)
        {
            ProductoDetalle objeto = await _productoData.Obtener(Id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        // 3. POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoDetalle objeto)
        {
            bool respuesta = await _productoData.Crear(objeto);
            if (respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = respuesta });
            }
        }

        // 4. PUT: api/Producto
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] ProductoDetalle objeto)
        {
            bool respuesta = await _productoData.Editar(objeto);
            if (respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = respuesta });
            }
        }

        // 5. DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int Id)
        {
            bool respuesta = await _productoData.Eliminar(Id);
            if (respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = respuesta });
            }
        }
    }
}