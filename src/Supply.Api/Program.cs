using Microsoft.EntityFrameworkCore;
using Supply.Business.Profiles;
using Supply.Business;
using Supply.Repository;
using Microsoft.AspNetCore.DataProtection;
using Supply.Client.Http.Abstractions;
using Supply.Client.Http;
using Supply.Business.Kafka;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<SupplyDbContext>(options => options.UseSqlServer("name=ConnectionStrings:SupplyDbContext", b => b.MigrationsAssembly("Supply.Api")));

// Bypass SSL validation for HttpClient
var httpClientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};
var httpClient = new HttpClient(httpClientHandler);
builder.Services.AddSingleton(httpClient);
builder.Services.AddScoped<ISupplyClient, SupplyClient>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();
builder.Services.AddKafkaProducerService<KafkaTopicsOutput, ProducerService>(builder.Configuration);


builder.Services.AddAutoMapper(typeof(AssemblyMarker));
// Add services to the container.

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

    var context = services.GetRequiredService<SupplyDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
