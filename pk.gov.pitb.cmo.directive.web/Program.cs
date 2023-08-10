using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.business.Seed;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Data.Abstractions;
using pk.gov.pitb.cmo.directive.web;
using pk.gov.pitb.cmo.directive.web.Core.Middleware;

var builder = WebApplication.CreateBuilder(args);


var settings = new JsonSerializerSettings
{
    ContractResolver = new EncryptIdContractResolver()
};
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();


builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer

    (authenticationScheme: JwtBearerDefaults.AuthenticationScheme,

        configureOptions: options =>
        {
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters =
                new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF32.GetBytes("contact@sami.pk[cmodirectiveV1]")),
                    ValidAudience = "PUBLIC",
                    ValidIssuer = "PITB",
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                };


        });



builder.Services.AddScoped<RepositoryContext>();
builder.Services.AddScoped<BaseHandler>();

builder.Services.AddScoped<UserManageHandler>();

builder.Services.AddScoped<CategoryHandler>();
builder.Services.AddScoped<FeatureHandler>();
builder.Services.AddScoped<ApplicationHandler>();
builder.Services.AddScoped<PriorityHandler>();
builder.Services.AddScoped<RoleHandler>();
builder.Services.AddScoped<ProfileHandler>();
builder.Services.AddScoped<SubjectHandler>();
builder.Services.AddScoped<DepartmentHandler>();
builder.Services.AddScoped<ConstituencyHandler>();
builder.Services.AddScoped<EmployeeHandler>();

builder.Services.AddScoped<LetterHandler>();
builder.Services.AddScoped<EventsHandler>();


builder.Services.AddScoped<IFileManager, FileManager>();

builder.Services.AddScoped<ISeedProvider, SeedProvider>();


builder.Services.AddScoped<ClaimsPrincipal>(serviceProvider =>
{
    return serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.User;

});


builder.Services.AddDbContextPool<RepositoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RepositoryContext"));
});

builder.Services.AddCors(options =>
{

    options.AddPolicy(
        "CorsPolicy",
        builder => builder.WithOrigins("http://localhost:4200", "http://localhost:4200/", "http://localhost:4200/login")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "CMO ERP",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Authorize APIs",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CMO ERP APIs"

    });

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Seed Data OnInit
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var hostingEnvironment = services.GetService<IWebHostEnvironment>();
    var seed = services.GetRequiredService<ISeedProvider>();

    try
    {
        if (hostingEnvironment.IsDevelopment())
        {
            seed.InitDevelopment();
        }
        else
        {
            seed.InitProduction();
        }

    }
    catch (Exception ex)
    {
        throw ex;
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

//app.UseFileServer(new FileServerOptions //This code is for show image without load on entire page, picture will display in show manner if laod exists. 
//{
//    FileProvider = new PhysicalFileProvider(@"D:\Project\Edirective-BackendV2\ed-web\pk.gov.pitb.cmo.directive\pk.gov.pitb.cmo.directive.web\wwwroot\Files\Hrmis\"),
//    RequestPath = new PathString("/hrmis"), //it is alias from which we will access picture in Files/Hrmis folder
//    EnableDirectoryBrowsing = false
//});

app.Run();
