using System.ComponentModel.DataAnnotations;

namespace SmartIntershipSistem.Models
{
    public class Company
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string CompanyName {get; set; }
        public string Description { get; set; }
        public string WebSite {  get; set; }

    }
}
