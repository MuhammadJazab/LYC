using System.Text;
using LYC.Controllers.IdentityResponsibilityService;
using LYC.Helpers;
using LYC.Models;
using LYC.Models.Identity;
using LYC.service;
using LYC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Utilities;
using static Utilities.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers().AddMvcOptions(x => x.EnableEndpointRouting = false);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"), builder => builder.EnableRetryOnFailure());
});

// For Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add(nameof(SessionKey.ExpiredToken), "true");
                context.Response.Headers.Add(nameof(SessionKey.ExpiredTokenMessage), SessionKey.ExpiredTokenMessage);
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsConstants.CorsPolicy, builder =>
    {
        builder.WithOrigins(CorsConstants.AllowedUrls)
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders(nameof(SessionKey.ExpiredToken), nameof(SessionKey.ExpiredTokenMessage))
               .SetIsOriginAllowed(origin => true)
               .Build();
    });
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
});

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    var openApiInfo = new OpenApiInfo
    {
        Title = "LYC Web API",
        Version = "v2",
        Description = ResponseMessages.TokenDescription
    };

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = ResponseMessages.TokenDescription,
        Name = HeaderNames.Authorization,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    c.SwaggerDoc("v2", openApiInfo);
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
       { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IIdentityResponsibilityService, IdentityResponsibilityService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IRoomService, RoomService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "LYC Web API");
});

app.UseHttpsRedirection();
app.UseCors(CorsConstants.CorsPolicy);
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();


app.UseMvc(endPoints =>
{
    endPoints.MapRoute(
        name: "default",
        template: "api/{controller}/{action}/{id?}");
});

app.Run();