namespace Application.Abstractions.Interface;
public interface IEmailService
{
    Task SendWelcomeMail(string to, string subject, string name, string otp, CancellationToken cancellationToken = default);
}
