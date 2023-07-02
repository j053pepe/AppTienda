using System.ComponentModel.DataAnnotations;

namespace Presentation.AppTiendaWeb.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; internal set; }
        public int TemperatureC { get; internal set; }
        [MaxLength(200)]
        public string Summary { get; internal set; }
    }
}
