using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Application.DTOs
{
    public class ProductDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public bool Active { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int StockQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Depth { get; set; }

       public IEnumerable<CategoryDTO> Categories { get; set; }



    }
}
