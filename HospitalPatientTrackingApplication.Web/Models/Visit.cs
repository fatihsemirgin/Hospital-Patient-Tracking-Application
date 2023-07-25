using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalPatientTrackingApplication.Web.Models
{
    public class Visit
    {
        [Key]
        [Required]
        public int Visit_Id { get; set; }

        [Required]
        public int Patient_Id { get; set;}
        [ForeignKey("Patient_Id")]
        public virtual Patient? Patient { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "timestamp(0) without time zone")]
        public DateTime Visit_Date { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$")]
        public string? Doctor_Name  { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string? Complaint { get; set; }
        [Column(TypeName = "text")]
        [Required]
        public string? Treatment { get; set; }
    }
}
