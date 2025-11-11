using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

var _fruit = new ConcurrentDictionary<string, Fruit>();

app.UseStatusCodePages();

app.MapGet("/test", () => "Hello World!")
    .WithName("hello");

app.MapGet("/redirect-me", () => Results.RedirectToRoute("hello"));

app.MapGet("/product/{name}", (string name) => $"The product is {name}")
    .WithName("product");

app.MapGet("/links", (LinkGenerator links) =>
{
    //var link = links.GetPathByName("product", new { name = "big-widget" });
    var urlLink = links.GetUriByName("product", new { Name = "super-fancy-widget" }, "https", new HostString("localhost"));
    return $"View the product at {urlLink}";
});

var fruitApi = app.MapGroup("/fruit");

fruitApi.MapGet("/", () => _fruit);

var fruitApiWithValidation = fruitApi.MapGroup("/")
    .AddEndpointFilter(ValidationHelper.ValidateId);

fruitApiWithValidation.MapGet("/{id}", (string id) =>
{
    return _fruit.TryGetValue(id, out var fruit)
        ? TypedResults.Ok(fruit)
        : Results.NotFound();
}).AddEndpointFilter(ValidationHelper.ValidateId);

fruitApiWithValidation.MapPost("/{id}", (string id, Fruit fruit) =>
    _fruit.TryAdd(id, fruit)
        ? TypedResults.Created($"/fruit/{id}", fruit)
        : Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {
                "id",
                new [] { "A fruit with this is already exists" }
            }
        }));

fruitApiWithValidation.MapPut("/{id}", (string id, Fruit fruit) =>
{
    _fruit[id] = fruit;
    return Results.NoContent();
});

fruitApiWithValidation.MapDelete("/{id}", (string id) =>
{
    _fruit.TryRemove(id, out _);
    return Results.NoContent();
});

app.Run();

record Fruit(string Name, int Stock) { }

class ValidationHelper
{
    internal static async ValueTask<object?> ValidateId(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        var id = context.GetArgument<string>(0);
        if (string.IsNullOrEmpty(id) || !id.StartsWith('f'))
        {
            return Results.ValidationProblem(
                new Dictionary<string, string[]>
                {
                    { "id", new [] { "Invalida format. Id must start with 'f'"}}
                });
        }
        return await next(context);
    }
}