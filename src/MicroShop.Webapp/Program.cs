using MicroShop.Business;
using MicroShop.Business.Kafka;
using MicroShop.Business.Profiles;
using MicroShop.Client.Http;
using MicroShop.Client.Http.Abstractions;
using MicroShop.Repository;
using MicroShop.Repository.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MicroShopDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection", b => b.MigrationsAssembly("MicroShop.Webapp")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MicroShopDbContext>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

// Bypass SSL validation for HttpClient
var httpClientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};
var httpClient = new HttpClient(httpClientHandler);
builder.Services.AddSingleton(httpClient);
builder.Services.AddScoped<IMicroShopClient, MicroShopClient>();
builder.Services.AddScoped<IBusiness, Business>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AssemblyMarker));

builder.Services.AddKafkaProducerService<KafkaTopicsOutput, ProducerService>(builder.Configuration);

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/data/ProtectionKeys"))
    .SetApplicationName("MicroShop");

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = ".AspNet.SharedCookie";
});

builder.Services.AddRazorPages();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MicroShopDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string[] roles = { "Admin", "Supplier" };

    foreach (string role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    ApplicationUser? user = await userManager.FindByIdAsync("3fb56e65-45d5-425c-89a3-fa7129f29d87");
    if (user != null && !await userManager.IsInRoleAsync(user, roles[0]))
        await userManager.AddToRoleAsync(user, roles[0]);

}

app.Run();
