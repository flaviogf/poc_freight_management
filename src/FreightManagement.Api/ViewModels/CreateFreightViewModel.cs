using FreightManagement.Api.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreightManagement.Api.ViewModels
{
    public class CreateFreightViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public EFreightRegion Region { get; set; }

        public int StateId { get; set; }

        public IEnumerable<int> CitiesId { get; set; } = new HashSet<int>();

        [Required]
        [Range(0, int.MaxValue)]
        public int Deadline { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}