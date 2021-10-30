using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC._7417.DAL.DBO
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Product name cannot be empty")]
        [MinLength(2, ErrorMessage = "Product name should have at least 2 characters")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        // category id cannot be empty
        public int CategoryId { get; set; }
        public Category ProductCategory { get; set; }

        [Required(ErrorMessage = "Price cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Price cannot be negative value")]
        public decimal Price { get; set; }
    }
}
