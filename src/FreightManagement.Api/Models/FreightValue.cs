namespace FreightManagement.Api.Models
{
    public class FreightValue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string BeginZipCode { get; set; }

        public string EndZipCode { get; set; }

        public double BeginWeight { get; set; }

        public double EndWeight { get; set; }

        public int Deadline { get; set; }
    }
}