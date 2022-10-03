using Inventory_Tracker.DAL;
using Inventory_Tracker.Helpers;
using Inventory_Tracker.Services;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "origin_policy";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddDbContext<global::Inventory_Tracker.DAL.DbContext>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();
