using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using System.Linq;

namespace SlnCibertec.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICibertecContext _cibertecContext;

        public CategoriesController(ICibertecContext cibertecContext)
        {
            _cibertecContext = cibertecContext;
        }

        [HttpGet]
        public ActionResult ObtenerTodo(int top)
        {
            if (top != 0)
            {
                return Ok(_cibertecContext.Categories.Take(top));
            }
            return Ok(_cibertecContext.Categories);
        }

        [HttpGet("{id}")]
        public ActionResult ObtenerPorId(int id)
        {
            return Ok(_cibertecContext.Categories.Find(id));
        }

        [HttpPost]
        public ActionResult Insertar(Category request)
        {
            _cibertecContext.Categories.Add(request);
            var result = _cibertecContext.Commit();
            if (result > 0)
            {
                return Ok(request.CategoryId);
            }

            return Ok("No se pudo insertar el registro");
        }

        [HttpGet("data-prueba")]
        [AllowAnonymous]
        public ActionResult InsertarCategoriasPrueba()
        {
            for (int i = 0; i < 10; i++)
            {
                _cibertecContext.Categories.Add(new Category { CategoryName = $"Categoria Prueba {i + 1}", Description = $"Descripción de la categoría {i + 1}", Picture = "url" });
            }

            return Ok(_cibertecContext.Commit());
        }
    }
}
