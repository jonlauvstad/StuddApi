using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StuddGokApi.Data;
using StuddGokApi.Repositories;
using StuddGokApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StuddGokApi.Extensions;
using StuddGokApi.Services.Interfaces;
using StuddGokApi.Services;
using StuddGokApi.Middlewear;

using StudentResource.Services.Interfaces;
using StudentResource.Services;
using Serilog;

using StuddGokApi.Models;
using Serilog;
using StuddGokApi.SSE;
using StuddGokApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// FOR Å TILLATE CORS: For å få fetchene i JS'n i HTML-fila til å funke når kjøres på egen maskin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:7042", "http://localhost:5170")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Configure Kestrel options -- SPØRRE YNGVE: NOEN MÅTE Å UNNGÅ DENNE PÅ I ServerSideEventController??!!
builder.WebHost.ConfigureKestrel(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();                // Registrerer validator
builder.Services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = false); //Også validering(default)

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<ILectureService, LectureService>();
builder.Services.AddScoped<ILectureRepository, LectureRepository>();
builder.Services.AddScoped<IExamImplementationService, ExamImplementationService>();
builder.Services.AddScoped<IExamImplementationRepository, ExamImplementationRepository>();

builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<ICourseImpService, CourseImpService>();
builder.Services.AddScoped<ICourseImpRepository, CourseImpRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddSingleton<AlertUserList>();

builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();


builder.Services.AddScoped<UserIdentifier>();

builder.Services.AddScoped<IStudentResourceService, StudentResourceService>();

builder.Services.AddDbContext<StuddGokDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.RegisterMappers();

// To jwt-token
// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

builder.Host.UseSerilog((context, configuration) =>
{
    //configuration.MinimumLevel.Debug().WriteTo.Console();  Men vi bruker appsettings
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

 
// KNYTTET TIL CORS: For å få fetchene i JS'n i HTML-fila til å funke når kjøres på egen maskin
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<UserIdentifier>();

app.UseAuthorization();

app.MapControllers();

//app.UseCors();  // Lagt til for ServerSideEvent fra egen maskin

app.Run();


public partial class Program { }