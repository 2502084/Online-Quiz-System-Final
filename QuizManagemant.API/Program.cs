var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS add karo yahan
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy => {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// CORS yahan lagao — UseAuthorization se PEHLE
app.UseCors("AllowAngular");

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<YourDbContextName>(); // Apne DbContext ka asal naam likhein
        context.Database.EnsureCreated(); // Yeh khud hi table bana dega agar nahi bane hue
    }
    catch (Exception ex)
    {
        // Log error if needed
    }
}
app.Run();
