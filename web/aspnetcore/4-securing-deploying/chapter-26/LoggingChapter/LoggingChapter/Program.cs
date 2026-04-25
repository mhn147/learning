var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFile();
builder.Logging.AddSeq();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
