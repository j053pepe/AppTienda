namespace Presentation.AppTiendaWeb.Models
{
    public class CityDataModelView
    {
        public List<CityModelView> Cities { get; set; }
        public static CityDataModelView Current { get; } = new CityDataModelView();
        public CityDataModelView()
        {
            Cities = new List<CityModelView>()
            {
                new CityModelView()
                {
                    Id = 1,
                    Name ="Puebla",
                    Description="Ciudad de los camotes"
                },
                new CityModelView()
                {
                    Id=2,
                    Name="Tlaxcala",
                    Description="Ciudad que no existe"
                },
                new CityModelView()
                {
                    Id=3,
                    Name="CDMX",
                    Description="Ciudad con personas de todos los estados."
                }
            };
        }
    }
}
