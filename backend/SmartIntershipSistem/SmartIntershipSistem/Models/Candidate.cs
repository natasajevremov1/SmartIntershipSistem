using System.ComponentModel.DataAnnotations;

namespace SmartIntershipSistem.Models
{
    public class Candidate
    {
        [Key]
        public Guid UserId {  get; set; }
        public User User { get; set; }

        public string Education { get; set; }
        public string CV {  get; set; }
        public string GitHub {  get; set; }


    }
}
