
// <ImplicitUsings>enable</ImplicitUsings>
// Create a web application builder
var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddRazorPages();
builder.Services.AddResponseCompression();

// Build
var app = builder.Build();

// Configure pipeline
//app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

//app.MapFallbackToFile("index.html");


app.Run();
