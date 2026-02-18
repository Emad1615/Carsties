using AuctionService.Consumers;
using AuctionService.Data;
using AuctionService.Features.Auction.Validators;
using AuctionService.Features.Auctions.Commands;
using AuctionService.Middleware;
using AuctionService.RequestHelpers;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(x => x.AddMaps(typeof(MappingProfile).Assembly));

builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblyContaining<AddAuction.Command>();
    opt.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateAuctionValidator>();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = builder.Configuration["IdentityServiceUrl"];
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;

//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = builder.Configuration["IdentityServiceUrl"],
//        ValidateAudience = false,
//        ValidateIssuerSigningKey = true,
//        NameClaimType = "username",
//    };
//});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:5001";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false, // عطل هذا مؤقتاً
            ValidateIssuerSigningKey = false, // عطل هذا مؤقتاً للتجربة
            SignatureValidator = delegate (string token, TokenValidationParameters parameters)
            {
                var jwt = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token);
                return jwt;
            },
            NameClaimType = "username",
        };

        // ده السطر السحري اللي بيحل مشاكل الاتصال المحلي بين الخدمات
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
    });

builder.Services.AddMassTransit(x=> {

    // for outbox implementation, we need to add the outbox repository to the db context and then configure the outbox in the mass transit configuration. status rabbitMQ down!!
    x.AddEntityFrameworkOutbox<AuctionDbContext>(d =>
    {
        d.QueryDelay = TimeSpan.FromSeconds(5);
        d.UsePostgres();
        d.UseBusOutbox();
    });

    x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));

    x.UsingRabbitMq((context,cfg)=> {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddTransient<ExceptionMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.DbInit(app);
}
catch (Exception e)
{
    Console.WriteLine($"Error during migration: {e}");
}

app.Run();
