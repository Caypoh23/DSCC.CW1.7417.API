using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DSCC._7417.DAL.DBO
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Category name cannot be empty")]
        [MinLength(2, ErrorMessage = "Product name should have at least 2 characters")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
