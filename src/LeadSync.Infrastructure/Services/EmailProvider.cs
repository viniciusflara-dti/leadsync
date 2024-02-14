using LeadSync.Application.Common.Interfaces;

public class FakeEmailService : IEmailProvider
{
    public void SendEmail(string to, string subject, string body)
    {
        // Instead of sending an email, we'll just write the details to the console
        Console.WriteLine($"Sending email to {to} with subject {subject} and body {body}");
    }
}