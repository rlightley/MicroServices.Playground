var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services, builder.Configuration);

var app = builder.Build();
ConfigureApplication(app);
app.Run();

static void RegisterServices(IServiceCollection services, IConfiguration config)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(config["ConnectionStrings:DefaultConnection"],
            sqlOptions => { sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null); }));
   
    services.AddMediatR(typeof(Program));
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddCustomEvents(config);
    services.AddEventHandlers();
}


static void ConfigureApplication(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.MigrateDb();
}

