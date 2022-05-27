namespace Sharp_Models
{
    public class SignInResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDto UserDto { get; set; }
    }
}
