using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PullupBars.Data;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PullupBarsAPIDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("PullUpBarsConnectionString")
    ));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
});

//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//    .AddEntityFrameworkStores<PullupBarsAPIDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
