
// <ImplicitUsings>enable</ImplicitUsings>
// Create a web application builder
var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddRazorPages();

// Build
var app = builder.Build();

// Configure pipeline
//app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
