    
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalPatientTrackingApplication.Web.Models
{
    public class Patient
    {
        [Key]
        [Required]
        public int Id { get; set; }     /// id resetle dbde
        [Required]
        [Range(10000000000, 99999999999)]
        public long id_card { get; set; }
        [Required]
        [MaxLength(40)]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$")]
        public string? name_surname { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime birth_date { get; set; }
    }

}
