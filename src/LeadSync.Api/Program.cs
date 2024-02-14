using LeadSync.Api;
using LeadSync.Application;
using LeadSync.Infrastructure;
using LeadSync.Infrastructure.Common; // Add this line

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseInfrastructure();
    app.UseCors("AllowAll");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }
    DatabaseMigration.MigrateDatabase(app.Services); // Add this line

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}
