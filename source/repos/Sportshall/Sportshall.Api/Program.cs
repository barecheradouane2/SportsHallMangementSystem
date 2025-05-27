
using Sportshall.Api.Helper;
using Sportshall.Api.Middleware;
using Sportshall.infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.infrastructureConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


// Role & Admin user seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await RoleSeeder.SeedRolesAsync(services);
    await UserSeeder.SeedAdminAsync(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
