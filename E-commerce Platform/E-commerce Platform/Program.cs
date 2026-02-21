using Azure.Storage.Blobs;
using ECommercePlatform.Data;
using ECommercePlatform.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Logging;


var builder = WebApplication.CreateBuilder(args);
var sqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var blobConnection = builder.Configuration.GetConnectionString("BlobStorage");

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(sqlConnection);
});
builder.Services.AddSingleton(new BlobContainerClient(blobConnection, "product-images"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
