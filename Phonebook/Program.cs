using PhoneBook.Extensions.Container;
using PhoneBook.Infrastructure;
using PhoneBook.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.InstallServicesInAssembly(builder.Configuration);
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
builder.Services.AddAutoMapper(typeof(Program));



var app = builder.Build();



app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=UserProfile}/{userId?}");


app.Run();
