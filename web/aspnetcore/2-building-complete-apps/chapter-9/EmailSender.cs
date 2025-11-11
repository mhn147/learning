namespace MyApp;

interface IEmailSender
{
    void SendEmail(string username);
}

class MessageFactory { }
class EmailServerSettings
{
    private readonly string host;
    private readonly int port;

    public EmailServerSettings(string host, int port)
    {
        this.host = host;
        this.port = port;
    }
}
class NetworkClient
{
    private readonly EmailServerSettings emailServerSettings;

    public NetworkClient(EmailServerSettings emailServerSettings)
    {
        this.emailServerSettings = emailServerSettings;
    }
}

class EmailSender : IEmailSender
{
    private readonly MessageFactory messageFactory;
    private readonly NetworkClient networkClient;

    public EmailSender(MessageFactory messageFactory, NetworkClient networkClient)
    {
        this.messageFactory = messageFactory;
        this.networkClient = networkClient;
    }

    public void SendEmail(string username)
    {
        Console.WriteLine($"Sending email to {username}...");
    }
}