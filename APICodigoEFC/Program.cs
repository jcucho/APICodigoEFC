using APICodigoEFC.Models;
using APICodigoEFC.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

//agregado ini
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:7189/",
                                              "http://www.hugotorrico.com")
                                              .AllowAnyHeader()
                                              .AllowAnyMethod();
                      });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
//agregado fin
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<CodigoContext>(
    options =>
    {
        options.UseSqlServer("Data Source=.;" +
            "Initial Catalog=CodigoDBEF;Integrated Security=true;" +
            "TrustServerCertificate=True");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//agregado
app.UseAuthentication();
app.UseAuthorization();
//agregado fin    

app.MapControllers();

//agregado ini
app.MapGet("/api/prueba/getMessage", () => "Hello World").RequireAuthorization();
app.MapGet("/api/prueba/getMessage2", () => "Mi primer minimal API");
app.MapPost("/security/createToken",
[AllowAnonymous] (User user) =>
{
    // Validar la existencia del usuario
    var userValidationResult = ValidationHelper.GetRole(user.UserName, user.Password);


    if (userValidationResult.IsValid)
    {

        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes
        (builder.Configuration["Jwt:Key"]);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            //Lo que va guardar el token
            //Claim: Información Adicional que guarda el token
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userValidationResult.Role)
             }),

            Expires = DateTime.UtcNow.AddMinutes(5),//Tiempo duración            
            Issuer = issuer,//Quien creo el token
            Audience = audience,//Para quien se creo el token
            SigningCredentials = new SigningCredentials //Tipo de encriptación
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };


        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return Results.Ok(stringToken);
    }

    return Results.Unauthorized();
});
//agregado fin

app.Run();
