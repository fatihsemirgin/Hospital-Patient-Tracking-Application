using System.ComponentModel.DataAnnotations;

namespace HospitalPatientTrackingApplication.Web.Models
{
    public class User
    {
        [Required]
        [Key]
        public int user_id { get; set; }
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
        [Range(10000000000, 99999999999)]
        [Required]
        public long id_card { get; set; }
        [Required]
        public string? role { get; set; }
    }
}
