namespace Sharp_Models
{
    public class SignUpResponseDto
    {
        public bool IsRegisterationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
