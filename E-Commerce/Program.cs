using E_Commerce;
using E_Commerce.Business.Hubs;
using E_Commerce.Business.Middelware;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var config = builder.Configuration;
builder.Services.Registration(config);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseCors();
StripeConfiguration.ApiKey = config.GetSection("Stripe:Secret_key").Get<string>();
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");
app.Run();



