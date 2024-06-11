using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Logging;
using Serilog;
using web_api.Configurations;
using web_api.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.File("Log/log.txt",rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
    .CreateLogger();

//Use serilog along with built-in loggers
builder.Host.UseSerilog();

//Use serilog along with built-in loggers
builder.Logging.AddSerilog();


//builder.Logging.ClearProviders();

//builder.Logging.AddDebug();
//builder.Logging.AddConsole();

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("CollegeAppDBConnection");

builder.Services.AddDbContext<CollegeDbContext>(options =>
{
    options.UseMySQL(connectionString);
});


builder.Services.AddControllers() .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//builder.Services.AddScoped<IMyLogger, LogToDB>();
//builder.Services.AddSingleton<IMyLogger, LogToDB>();
// builder.Services.AddTransient<IMyLogger, LogToDB>();

builder.Services.AddTransient<IMyLogger, LogToServer>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

