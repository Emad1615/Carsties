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
using Microsoft.IdentityModel.JsonWebTokens;
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServiceUrl"];
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = false,
        SignatureValidator = delegate (string token, TokenValidationParameters parameters)
        {
            var jwt = new JsonWebToken(token);
            return jwt;
        },
        NameClaimType = "username",
        RoleClaimType = "role",
    };
});

#region another code for authentication, this code is for testing purposes only and should not be used in production because it disables important security checks like issuer and signing key validation. it's here just to help you test the authentication flow without needing a fully configured identity server. remember to remove or properly configure this before deploying to production.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "http://localhost:5001";
//        options.RequireHttpsMetadata = false;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//            ValidateIssuer = false, // عطل هذا مؤقتاً
//            ValidateIssuerSigningKey = false, // عطل هذا مؤقتاً للتجربة
//            SignatureValidator = delegate (string token, TokenValidationParameters parameters)
//            {
//                var jwt = new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token);
//                return jwt;
//            },
//            NameClaimType = "username",
//        };

//        // ده السطر السحري اللي بيحل مشاكل الاتصال المحلي بين الخدمات
//        options.BackchannelHttpHandler = new HttpClientHandler
//        {
//            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
//        };
//    });
#endregion

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
        cfg.UseMessageRetry(r => r.Interval(5,TimeSpan.FromSeconds(5)));
        cfg.ReceiveEndpoint("auction-auction-finished", e => { e.ConfigureConsumer<AuctionFinishedConsumer>(context); });
        cfg.ReceiveEndpoint("auction-bid-placed", e => { e.ConfigureConsumer<BidPlacedConsumer>(context); });

        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h => { 
            h.Username(builder.Configuration.GetValue("RabbitMQ:Username","guest"));
            h.Password(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
        });
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
