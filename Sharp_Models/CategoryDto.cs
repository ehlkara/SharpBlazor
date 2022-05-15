using System.ComponentModel.DataAnnotations;

namespace Sharp_Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter name..")]
        public string Name { get; set; }
    }
}
