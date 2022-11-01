using GreetingApp;

var builder = WebApplication.CreateBuilder(args);


// app.Environment.

// app.Configuration

// how to read db config from environment variable...

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddSingleton<IGreet, Greet>();
builder.Services.AddSingleton<IGreet, GreetWithMangoDB>( x => 
    new GreetWithMangoDB(builder.Configuration.GetConnectionString("client"))
);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
