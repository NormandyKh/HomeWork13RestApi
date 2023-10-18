using System.ComponentModel.DataAnnotations;

namespace RestApiHW13.Contract.Requests
{
    public class UpsertProductRequest
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
