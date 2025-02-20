using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseApiTemplate.Middlewares.Swagger;
using CloudComputingProvider.AutoMapper;
using CloudComputingProvider.DI;
using CloudComputingProvider.Extensions.Auth;
using CloudComputingProvider.Extensions.EF;
using CloudComputingProvider.Infrastructure.Persistence.DI;
using CloudComputingProvider.Middlewares;
using FluentValidation.AspNetCore;
using MediatR;
using Serilog;
using System.Reflection;

// read configuration files
string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{env}.json", optional: true)
                            .Build();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Persistence
builder.Services.AddPersistence(builder.Configuration);

//AutoMapper
builder.Services.AddAutoMapper(typeof(DefaultMapperProfile));

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

//Fluent Validations
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Register the Swagger generator, defining 1 or more Swagger documents
SwaggerMiddleware.AddSwaggerGen(builder.Services, builder.Configuration);

// Register Auth Extension
builder.Services.RegisterAuth(configuration);
builder.Services.ConfigureServices(configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new RegisterAutofacAssemblies());
    });

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//Enable Swagger middleware 
SwaggerMiddleware.UseSwagger(app);

//Correlation Id Middleware
app.UseMiddleware<RequestContextLoggingMiddleware>();

//Enable Logger Middleware 
app.UseMiddleware<LoggerMiddleware>();

////Error Handling Middleware 
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.ExecuteMigrations();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
