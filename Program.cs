var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{apiKey}", (string apiKey) =>
{
    if (apiKey != app.Configuration["ApiKey"])
    {
        return "Dje ces kraaalju";
    }
    
    return "Hello World!";
});

app.Run();
