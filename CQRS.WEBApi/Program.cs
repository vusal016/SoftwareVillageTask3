using StreamVibe.WEBApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Enumun resoponse da string kimi gorunmesi ucun elave etdim.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
builder.Services.AddSwaggerDocumentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();