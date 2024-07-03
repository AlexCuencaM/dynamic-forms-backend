using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
string[] corsHostClients = builder.Configuration.GetSection("CORSDomainClients").Get<string[]>()
                ?? throw new Exception("Add a client domain for CORS to CORSDomainClients env(array)");
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "origen1",
        app =>
        {
            app
            .WithOrigins(corsHostClients)
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});
builder.Configuration.AddJsonFile($"ocelot-{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
builder.Services
    .AddOcelot(builder.Configuration);
var app = builder.Build();
app.UseCors("origen1");
await app.UseOcelot();
app.Run();