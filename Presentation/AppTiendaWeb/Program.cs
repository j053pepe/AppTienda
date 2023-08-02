using Infraestructure.Data.StoreDbMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Transversal.Resolver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ConnectionString
builder.Services.AddDbContext<AppTiendaContext>(options =>
        options.UseJet(builder.Configuration.GetConnectionString("DbMsAccess")));
//Registration Service with transversa.resolver
IoCRegister.AddRegistration(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();

app.Run();