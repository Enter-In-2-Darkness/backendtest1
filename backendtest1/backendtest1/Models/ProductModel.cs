using System.ComponentModel.DataAnnotations;

namespace backendtest1.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string NumberCode { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }

    public class ProductValidateModel
    {
        [Required(ErrorMessage = "Please input your Number Code")]
        [RegularExpression(@"^[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}-[0-9A-Z]{5}$", ErrorMessage = "Number Code must be in the format XXXXX-XXXXX and limit 30 digits")]

        public string NumberCode { get; set; } = string.Empty;
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
