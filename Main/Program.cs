using ICategoryServices;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models;
using Repositories;
using Services;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

//----------------SERVICES ADDING ------------------

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<VehicleServices>();
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<ModelRepository>();
builder.Services.AddScoped<ModelServices>();
builder.Services.AddScoped<BrandRepository>();
builder.Services.AddScoped<BrandServices>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


// --------------IDENTITY ET DBCONTEXT-------------------
builder.Services.AddDbContext<DatabaseContext>(dbContextBuilderOptions =>
    dbContextBuilderOptions.UseSqlServer(builder.Configuration.GetConnectionString("ContextCS"))
);
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
    jsonOptions.JsonSerializerOptions.ReferenceHandler =
    ReferenceHandler.IgnoreCycles
);

builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Car share", Version = "V1" });
    string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swaggerOptions.IncludeXmlComments(xmlPath);

    swaggerOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    swaggerOptions.OperationFilter<SecurityRequirementsOperationFilter>();
});


WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<AppUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
