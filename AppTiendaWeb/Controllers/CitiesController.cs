using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.Models;

namespace Presentation.AppTiendaWeb.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CityDataModelView.Current.Cities);
        }

        [HttpGet("{id}")]
        public JsonResult GetCity(int id)
        {
            return new JsonResult(CityDataModelView.Current.Cities.FirstOrDefault(c => c.Id == id));
        }
    }
}
