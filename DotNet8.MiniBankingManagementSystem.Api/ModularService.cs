using DotNet8.MiniBankingManagementSystem.Api.Features.Account;
using DotNet8.MiniBankingManagementSystem.Api.Features.Deposit;
using DotNet8.MiniBankingManagementSystem.Api.Features.State;
using DotNet8.MiniBankingManagementSystem.Api.Features.Township;
using DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;
using DotNet8.MiniBankingManagementSystem.DbService.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Api;

public static class ModularService
{
    #region AddServices

    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContextServices(builder);
        services.AddDataAccessServices();
        services.AddBusinessLogicServices();
        return services;
    }

    #endregion

    #region AddBusinessLogicServices

    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<BL_Account>();
        services.AddScoped<BL_State>();
        services.AddScoped<BL_Township>();
        services.AddScoped<BL_Deposit>();
        services.AddScoped<BL_WithDraw>();
        return services;
    }

    #endregion

    #region AddDataAccessServices

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<DA_Account>();
        services.AddScoped<DA_State>();
        services.AddScoped<DA_Township>();
        services.AddScoped<DA_Deposit>();
        services.AddScoped<DA_WithDraw>();
        return services;
    }

    #endregion

    #region AddDbContextServices

    public static IServiceCollection AddDbContextServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        return services;
    }

    #endregion
}
