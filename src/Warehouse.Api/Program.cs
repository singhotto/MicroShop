using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Warehouse.Business;
using Warehouse.Business.Kafka;
using Warehouse.Business.Kafka.MessageHandlers;
using Warehouse.Business.Profiles;
using Warehouse.Repository;

var builder = WebApplication.CreateBuilder(args);
// Add configuration to the container.
string connectionString = "Server=mssql-server;Database=WAREHOUSE;User Id=sa;Password=p4ssw0rD;Encrypt=False";
// Add services to the container.
builder.Services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Warehouse.Api")));

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();

builder.Services.AddAutoMapper(typeof(AssemblyMarker));

builder.Services.AddKafkaProducerService<KafkaTopics, ProducerService>(builder.Configuration);
builder.Services.AddKafkaConsumerService<KafkaTopics, MessageHandlerFactory>(builder.Configuration);

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/data/ProtectionKeys"))
    .SetApplicationName("MicroShop");

builder.Services.AddAuthentication("Identity.Application")
    .AddCookie("Identity.Application", options =>
    {
        options.Cookie.Name = ".AspNet.SharedCookie";
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WarehouseDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
