using Microsoft.AspNetCore.Mvc;
using Presentation.AppTiendaWeb.Models;

namespace Presentation.AppTiendaWeb.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<CityModelView>> GetCities()
        {
            return Ok(CityDataModelView.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityModelView> GetCity(int id)
        {
            var city = CityDataModelView.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
                return NotFound();

            return Ok(city);
        }
    }
}
