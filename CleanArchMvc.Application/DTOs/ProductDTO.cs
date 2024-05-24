using CleanArchMvc.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required!")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description is required!")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price is required!")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Stock is required!")]
        [Range(0, 9999)]
        [DisplayName("Stock")]
        public int Stock { get; set; }

        [MaxLength(250)]
        [DisplayName("Product Extension Image")]
        [DataType(DataType.ImageUrl)]
        public string? ExtensionImage { get; set; }

        [DisplayName("File To Copy For Image")]
        [JsonIgnore]
        public string? FileToCopyForImage { get; set; }

        [DisplayName("Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Category!")]
        public int CategoryId { get; set; }
        
        [JsonIgnore]
        public Category? Category { get; set; }

        public string ImageName()
        {
            return $"{Id}{ExtensionImage}";
        }

    }
}
