var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecksUI()
    .AddInMemoryStorage();

builder.Logging.AddJsonConsole();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
var pathBase = builder.Configuration["PATH_BASE"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHealthChecksUI(config =>
{
    config.ResourcesPath = string.IsNullOrEmpty(pathBase)
        ? "/ui/resources"
        : $"{pathBase}/ui/resources";
});

app.MapGet(string.IsNullOrEmpty(pathBase)
    ? "/"
    : pathBase, () => Results.LocalRedirect("~/healthchecks-ui"));
app.MapHealthChecksUI();

app.Run();
