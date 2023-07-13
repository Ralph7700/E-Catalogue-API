using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using e_catalog_backend.Helpers;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Categories;
using e_catalog_backend.Repositories.Images;
using e_catalog_backend.Repositories.Orders;
using e_catalog_backend.Repositories.Products;
using e_catalog_backend.Repositories.SubCategories;
using e_catalog_backend.Repositories.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
string _CORSPolicy = "_CORSPolicy";
IWebHostEnvironment _env = builder.Environment;
IConfiguration configuration = builder.Configuration;
string DbConfig = configuration["ConnectionStrings:MainDb"];

#region Services configurations

// CORS Configurations
builder.Services.AddCors(options => {
    options.AddPolicy(name: _CORSPolicy,
        builder => {
            if (_env.IsDevelopment()) {
                builder
                    .WithOrigins("http://localhost:3000") 
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Pagination")
                    .AllowCredentials();

            }
            else if(_env.IsStaging()){
                builder.AllowAnyHeader()
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Pagination")
                    .AllowCredentials();
            }

        });
});

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on text box below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// Add Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Authentication:Issuer"],
        ValidAudience = configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:SecretKey"]))
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Manager", policy =>
            
                policy.RequireClaim(ClaimTypes.Role, "Manager")
            
        );
        options.AddPolicy("Salesman", policy =>
            
                policy.RequireClaim(ClaimTypes.Role, "Salesman")
            
        );
    }
);


builder.Services.AddMvc(options => options.EnableEndpointRouting = false);


// DB Configurations
builder.Services.AddDbContext<MainDbContext>(opt => opt.UseNpgsql(DbConfig));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// handle all the dependencies
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();


// Other Configurations
builder.Services.AddHttpContextAccessor();
#endregion


#region build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseRouting();

app.UseCors(_CORSPolicy);

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});

app.Run();

#endregion
