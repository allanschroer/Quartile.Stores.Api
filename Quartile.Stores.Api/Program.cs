using Microsoft.EntityFrameworkCore;
using Quartile.Stores.Api.Configuration;
using Quartile.Stores.Domain.Interfaces.Repositories;
using Quartile.Stores.Domain.Interfaces.Services;
using Quartile.Stores.Infra.Configuration;
using Quartile.Stores.Infra.Reporitories;
using Quartile.Stores.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoresContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

builder.Services.AddScoped<IStoreService, StoreService>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
