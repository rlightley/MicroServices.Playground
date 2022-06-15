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
    services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));
    services.AddCustomEvents(config);
    services.AddEventHandlers();
}


static void ConfigureApplication(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
    app.MigrateDb();
}