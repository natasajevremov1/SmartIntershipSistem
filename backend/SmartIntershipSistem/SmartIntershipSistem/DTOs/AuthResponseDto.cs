
using SmartIntershipSistem.Models;

namespace SmartIntershipSistem.DTOs
{  //ono sto server vraca klijentu nakon uspesnog logina ili registera
    public class AuthResponseDto
    {
        public string Message {  get; set; }
        public Role Role { get; set; }
        public string JWTToken { get; set; }
    }
}
