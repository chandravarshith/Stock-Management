using System.ComponentModel.DataAnnotations;

namespace Stock_Management_System.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        [Display(Name ="Quantity")]
        public int AvailableQuantity { get; set; } 
        [Required]
        [Range(1,100000,ErrorMessage ="Price must be only between 1 to 100000!")]
        public decimal Price { get; set; }
        [Display(Name = "Availability Status")]
        public string AvailabilityStatus { get; set; }
        public int DispatchedQuantity { get; set; }
        public int DeliveredQuantity { get; set; }

    }
}
