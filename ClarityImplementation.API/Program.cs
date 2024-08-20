using ClarityImplementation.API.Services;
using ClarityImplementation.API.Services.Polling;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ClarityImplementation.API.Db.ClarityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClarityDBConnection")));
builder.Services.AddScoped<EmailService>();

builder.Services.AddHostedService<PollingService>();
builder.Services.AddHostedService<TimedBackgroundService>();

builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // React app URL
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin"); // Apply CORS policy

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to Clarity Implementation API!");
});

app.MapGet("/favicon.ico", async context =>
{
    await context.Response.WriteAsync("Welcome to Clarity Implementation API!");
});

app.Run();
