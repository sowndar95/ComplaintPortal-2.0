using ComplaintPortal;
using ComplaintPortal_2._0.SeedData;
using ComplaintPortalEntities;
using ComplaintPortalEntities.Authentication;
using ComplaintPortalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var applicationSettings = builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>();
builder.Services.Configure<ApplicationSettings>(options => builder.Configuration.GetSection(nameof(ApplicationSettings)).Bind(options));
builder.Services.AddSingleton<ApplicationSettings>(x => x.GetRequiredService<IOptions<ApplicationSettings>>().Value);
builder.Services.AddScoped<IRoleValidator<ApplicationRole>, DepartmentRoleValidator>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
}).AddDefaultTokenProviders()
.AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(applicationSettings!.DatabaseSettings.Connection_String, applicationSettings.DatabaseSettings.DataBase_name);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

        });
});

//Seed Data
builder.Services.AddScoped<SeedUser>();
builder.Services.AddScoped<SeedMenu>();

//Register Authorization Services
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationOptions(builder.Configuration);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Load Seed Data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

//Load Seed Menu
var menuSeedData = services.GetRequiredService<SeedMenu>();
await menuSeedData.RunAsync();

var adminUser = services.GetRequiredService<SeedUser>();
await adminUser.RunAsync();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
