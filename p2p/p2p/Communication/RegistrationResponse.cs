namespace p2p.Communication
{

    public class RegistrationResponse:BaseResponse
    {
        public string Username { get; set; }
        public RegistrationResponse(bool success, string message, string username) : base(success, message)
        {
            Username = username;
        }
    }
}
