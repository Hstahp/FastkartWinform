using Common.Enum; 

namespace DTO
{
    public class LoginResultDTO
    {
        public LoginStatus Status { get; set; }
        public UserDTO User { get; set; }
    }
}