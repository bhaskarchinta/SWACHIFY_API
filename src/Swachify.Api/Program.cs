using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Swachify.Infrastructure.Data;
using Swachify.Application;
using Microsoft.AspNetCore.Identity;
using Swachify.Application.Interfaces;
using Swachify.Application.Services;
using System.Text.Json.Serialization;
using Swachify.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("spa", p => p
        .WithOrigins("http://localhost:4200", "https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

// Add services
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    // opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    // opts.JsonSerializerOptions.MaxDepth = 64;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOtpService>(sp =>
    new TwilioOtpService(
        Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
        Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"),
        Environment.GetEnvironmentVariable("TWILIO_VERIFY_SERVICE_SID")
    )
);

// Register application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICleaningService, CleaningService>();
builder.Services.AddScoped<IMasterService, MasterService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors("spa");
app.UseAuthorization();

app.MapControllers();

app.Run();
