var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDeveloperExceptionPage(); // isn't strictly necessary - already added by WebApplication by default
app.UseStaticFiles();
app.UseRouting();

var people = new List<Person>
{
    new("Tom", "Hanks"),
    new("Al", "Pacino")
};

app.MapGet("/", () => "Hello World!!");
app.MapGet("/person/{name}", (string name) =>
    people.Where(p => p.FirstName.StartsWith(name)));

app.Run();

record Person(string FirstName, string LastName);