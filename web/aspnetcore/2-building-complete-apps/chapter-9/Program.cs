using MyApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEmailSender();

var app = builder.Build();

app.MapGet("/register/{username}", RegisterUser);

app.Run();

string RegisterUser(string username, IEmailSender emailSender)
{
    emailSender.SendEmail(username);
    return $"Email sent to {username}";
}

public static class EmailSenderServiceCollectionExtensions
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<NetworkClient>();
        services.AddSingleton<MessageFactory>();
        services.AddScoped(
            provider => new EmailServerSettings("smtp.server.com", 80)
        );
        return services;
    }
}