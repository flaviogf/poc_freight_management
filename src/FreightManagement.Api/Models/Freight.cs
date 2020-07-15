using System.Collections.Generic;

namespace FreightManagement.Api.Models
{
    public class Freight
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FreightValue> Values { get; set; } = new List<FreightValue>();
    }
}