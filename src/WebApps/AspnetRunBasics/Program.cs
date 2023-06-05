
using AspnetRunBasics.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
/*builder.Services.AddDbContext<AspnetRunContext>(c =>
               c.UseSqlServer(builder.Configuration.GetConnectionString("AspnetRunConnection")));*/
builder.Services.AddHttpClient<ICatalogService, CatalogService>(c=>c.BaseAddress=new Uri(builder.Configuration["ApiSettings:GatewayAddress"]));
builder.Services.AddHttpClient<IBasketService, basketService>(c => c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]));
builder.Services.AddHttpClient<IOrderService, OrderService>(c => c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]));
builder.Services.AddRazorPages();

    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapRazorPages();

app.Run();
