using Api.Extensions;
using Application.Command.Consult;
using Application.Command.Customer;
using Application.Command.Service;
using Application.Command.User;
using FluentValidation.AspNetCore;
using Infra.ACL.Jwt;
using Infra.Data.Consult;
using Infra.Data.Customer;
using Infra.Data.Service;
using Infra.Data.User;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables()
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

//MediatR
builder.Services.AddMediatR(typeof(LoginCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(PasswordCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(CustomerCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetDocumentCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(ConsultCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetConsultCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetServiceCommand).GetTypeInfo().Assembly);

//Jwt
builder.Services.AddTransient<IJwt, Jwt>();

//Data
builder.Services.AddTransient<IUserReader, UserReader>();
builder.Services.AddTransient<IUserWriter, UserWriter>();
builder.Services.AddTransient<ICustomerReader, CustomerReader>();
builder.Services.AddTransient<ICustomerWriter, CustomerWriter>();
builder.Services.AddTransient<IConsultReader, ConsultReader>();
builder.Services.AddTransient<IConsultWriter, ConsultWriter>();
builder.Services.AddTransient<IServiceReader, ServiceReader>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Mapping));

builder.Services.ConfigureProblemDetailsModelState();

builder.Services.AddControllers().AddNewtonsoftJson(json =>
{
    json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
}).AddFluentValidation(validator =>
{
    validator.RegisterValidatorsFromAssemblyContaining<ConsultCommand>();
    validator.LocalizationEnabled = false;
});

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Odonto Puc - API", Version = "v1" });
    //c.DocumentFilter<SwaggerFilter>();
    c.ExampleFilters();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Solicite e informe: 'Bearer ' + token'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

    c.EnableAnnotations();
    c.CustomSchemaIds(x => x.Name);
});

string jwtSecret = configuration.GetSection("Jwt:Secret").Value;
var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret));

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = signingKey,
    ValidateIssuer = false,
    ValidateAudience = false
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(configureOptions =>
{
    configureOptions.RequireHttpsMetadata = false;
    configureOptions.SaveToken = true;
    configureOptions.TokenValidationParameters = tokenValidationParameters;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
           .RequireAuthenticatedUser().Build());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("v1/swagger.json", "Odonto Puc V1");
    c.DocumentTitle = "Documentação Odonto Puc API";
    c.DocExpansion(DocExpansion.None);
});
//}

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Trace).AddConsole());
app.UseExceptionHandlerMiddleware(loggerFactory);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
