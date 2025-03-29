using Microsoft.EntityFrameworkCore;
using ProfileServices.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("anyOrigin", policyBuilder =>
    {

        policyBuilder.SetIsOriginAllowed(_ => true) // Allows any origin
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();

    });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("anyOrigin");

app.Run();
