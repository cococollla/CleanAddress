using CleanAddress.Dadata.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var clientSettings = builder.Configuration.GetSection("ClientOption");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<ClientOption>(clientSettings);
builder.Services.AddSingleton<IDadataClient, DadataClient>();
builder.Services.AddHttpClient<IDadataClient, DadataClient>(
    client =>
    {
        client.BaseAddress = new Uri(clientSettings["BaseAddress"]);
        client.DefaultRequestHeaders.UserAgent.ParseAdd("DadataClient");
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "Dadata service");
    });
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.Run();
