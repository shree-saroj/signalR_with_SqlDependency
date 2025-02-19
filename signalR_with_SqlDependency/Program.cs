using signalR_with_SqlDependency.Hubs;
using signalR_with_SqlDependency.MiddlewareExtensions;
using signalR_with_SqlDependency.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Adding SignalR to the services
builder.Services.AddSignalR();

// Add classes Through Dependency Injection
// This means that only one instance of DashboardHub & SubscribeProductTableDependency will be created and shared throughout the application.
// SignalR hubs should be transient or singleton to avoid unnecessary instance creation.
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
builder.Services.AddSingleton<SubscribeSaleTableDependency>();

var app = builder.Build();

// variable for Connection String
string? _conn = app.Configuration.GetConnectionString("DefaultConnection");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Adding Hub to the pipeline
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

// Calling Middleware to subscribe to table dependency

app.UseSqlTableDependency<SubscribeProductTableDependency>(_conn);
app.UseSqlTableDependency<SubscribeSaleTableDependency>(_conn);

app.Run();

// Flow of the application 
// 1. Middleware
// 2. Subscribe Table Dependency
// 3. Hub
// 4. Client