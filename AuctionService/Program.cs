using AuctionService.Data;
using AuctionService.Features.Auctions.Commands;
using AuctionService.Features.Auction.Validators;
using AuctionService.Middleware;
using AuctionService.RequestHelpers;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MassTransit;

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

var root = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context,cfg)=> {
        cfg.SetLicenseLocation(Path.Combine(root, "license.txt"));
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddTransient<ExceptionMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

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
