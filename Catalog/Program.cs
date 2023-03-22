using Auth.API;
using Auth.Services;
using Catalog;
using Catalog.Repositories;
using Common.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{ 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasicAuth", Version = "v1" });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApp.xml");
    c.IncludeXmlComments(filePath);
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme. Login = admin; Password = Pa$$WoRd"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "BasicAuthentication";
}).AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddDbContext<WandsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WandsContext"),
        x => x.MigrationsAssembly("Catalog")));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();
//var c = app.Services.GetRequiredService<WandsContext>();
//CatalogSeed.CreateData(c);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(x => x.RouteTemplate = "catalog/swagger/{documentname}/swagger.json");
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/catalog/swagger/v1/swagger.json", "catalog");
        c.RoutePrefix = "catalog/swagger";
    });
}
app.UseMiddleware(typeof(ErrorHandlerMiddleware));
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
