var builder = WebApplication.CreateBuilder(args);

// Port configuration for Cloud Deployment (Render/Railway)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Policy Setup (Allows frontend to connect easily)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable Swagger for testing endpoints (always enabled for easy testing)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz Management API v1");
    c.RoutePrefix = string.Empty; // Yeh link kholte hi direct Swagger UI khol dega
});

// Middleware pipeline (Order is very important here)
app.UseRouting();

// 1. CORS hamesha Authorization se pehle aana chahiye
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
