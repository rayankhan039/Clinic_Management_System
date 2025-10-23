using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_Id { get; set; }

        [Required]
        [DisplayName("Patient's Name")]
        public string Patient_Name { get; set; }

        [Phone]
        [Required]
        [MaxLength(11, ErrorMessage="Only 11 Digit Number is Acceptable")]
        [DisplayName("Patient's Contact")]
        public string Patient_Contact { get; set; }

        [Required]
        [DisplayName("Doctor")]
        public int Doctor_Id { get; set; }

        [ForeignKey("Doctor_Id")]
        public Doctor Doctor { get; set; }

        [Required]
        public decimal Fee { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(10)]
        public string TokenNumber { get; set; }
    }
}
