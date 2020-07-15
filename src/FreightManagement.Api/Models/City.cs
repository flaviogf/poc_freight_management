namespace FreightManagement.Api.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BeginZipCode { get; set; }

        public string EndZipCode { get; set; }
    }
}