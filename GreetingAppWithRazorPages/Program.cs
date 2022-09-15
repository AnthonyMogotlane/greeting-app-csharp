using GreetingApp;

var builder = WebApplication.CreateBuilder(args);



// app.Environment.

// app.Configuration

// how to read db config from environment variable...

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddSingleton<IGreet, Greet>();
builder.Services.AddTransient<IGreet, GreetWithDB>( x => 
    new GreetWithDB("Server=heffalump.db.elephantsql.com;Port=5432;Database=xbixatua;UserId=xbixatua;Password=MZpFuYnavsnJw65QqMIG9JtHM29yqMz6")
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
