using Microsoft.AspNetCore.Mvc;
using SmartBusinessApplication.Api.Hubs;
using SmartBusinessApplication.Application.AutoMapperProfile;
using SmartBusinessApplication.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// servicesd extensions Add
builder.Services.AddServices();
builder.Services.AddAutoMapper(typeof(ClientProfile));
//service auto validation is disabled
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
//add signalR
builder.Services.AddSignalR();
builder.Services.AddTransient<DataHub>();
//build application
var app = builder.Build();
// app addded cors settin allow hepsi sdfjsdkfkl
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.MapHub<DataHub>("/DataHub");
app.Run();
