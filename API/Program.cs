using API;
using Entity.DataContext;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Repository.Repository;
using Services.IService;
using Services.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBCS"));
});
builder.Services.AddAutoMapper(typeof(MapConfig));

builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IPatientService,PatientService>();

builder.Services.AddScoped<ITableRepository,TableRepository>();
builder.Services.AddScoped<IPatientRepository,PatientRepository>();
builder.Services.AddScoped<IAuthRepository,AuthRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();  
