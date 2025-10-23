using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Phone]
        [Required]
        [MaxLength(11, ErrorMessage = "Only 11 Digit Number is Acceptable")]

        public string Contact { get; set; }

        [Required]
        public string Specialization { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;

    }
}
