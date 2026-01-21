using microservicesSales.Application.Interfaces;
using microservicesSales.Application.UseCase;
using microservicesSales.Infrastructure.Hubs;
using microservicesSales.Infrastructure.Repository;
using microservicesSales.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SalesDb")));



builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SalesRepository>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<ProductUseCase>();
builder.Services.AddScoped<SalesUseCase>();


builder.Services.AddSignalR();
builder.Services.AddScoped<ISalesNotifier, SalesNotifier>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRCors", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true); 
    });
});

var app = builder.Build();

app.UseCors("SignalRCors");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SalesHub>("/hubs/sales");

app.Run();
