namespace Domain.DTO.Auth;
public class LoginSuccessDto
{
    public string Token { get; set; }
    public CreatedUserDto User { get; set; }
}
