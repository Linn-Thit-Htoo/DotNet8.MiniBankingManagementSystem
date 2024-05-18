using DotNet8.MiniBankingManagementSystem.Api.Features.Account;
using DotNet8.MiniBankingManagementSystem.Api.Features.Deposit;
using DotNet8.MiniBankingManagementSystem.Api.Features.State;
using DotNet8.MiniBankingManagementSystem.Api.Features.Township;
using DotNet8.MiniBankingManagementSystem.Api.Features.TransactionHistory;
using DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;

namespace DotNet8.MiniBankingManagementSystem.Api;

public static class ModularService
{
    #region Add Services

    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .AddJsonService()
            .AddDbContextServices(builder)
            .AddDataAccessServices()
            .AddBusinessLogicServices();
        return services;
    }

    #endregion

    #region Json Service

    private static IServiceCollection AddJsonService(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        return services;
    }

    #endregion

    #region Add Business Logic Services

    private static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<BL_Account>();
        services.AddScoped<BL_State>();
        services.AddScoped<BL_Township>();
        services.AddScoped<BL_Deposit>();
        services.AddScoped<BL_Withdraw>();
        services.AddScoped<BL_TransactionHistory>();
        return services;
    }

    #endregion

    #region Add Data Access Services

    private static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<DA_Account>();
        services.AddScoped<DA_State>();
        services.AddScoped<DA_Township>();
        services.AddScoped<DA_Deposit>();
        services.AddScoped<DA_Withdraw>();
        services.AddScoped<DA_TransactionHistory>();
        return services;
    }

    #endregion

    #region Add DbContext Services

    private static IServiceCollection AddDbContextServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        return services;
    }

    #endregion
}