namespace LeadSync.Application.Common.Interfaces;

public interface IEmailProvider
{
    void SendEmail(string to, string subject, string body);
}
