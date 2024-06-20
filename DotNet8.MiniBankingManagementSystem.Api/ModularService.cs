using DotNet8.MiniBankingManagementSystem.Modules.Features.Account;
using DotNet8.MiniBankingManagementSystem.Modules.Features.Deposit;
using DotNet8.MiniBankingManagementSystem.Modules.Features.State;
using DotNet8.MiniBankingManagementSystem.Modules.Features.Township;
using DotNet8.MiniBankingManagementSystem.Modules.Features.TransactionHistory;
using DotNet8.MiniBankingManagementSystem.Modules.Features.Withdraw;

namespace DotNet8.MiniBankingManagementSystem.Api;

public static class ModularService
{
    #region Add Services

    public static IServiceCollection AddServices(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
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
        services
            .AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        return services;
    }

    #endregion

    #region Add Business Logic Services

    private static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services
            .AddScoped<BL_Account>()
            .AddScoped<BL_State>()
            .AddScoped<BL_Township>()
            .AddScoped<BL_Deposit>()
            .AddScoped<BL_Withdraw>()
            .AddScoped<BL_TransactionHistory>();
        return services;
    }

    #endregion

    #region Add Data Access Services

    private static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services
            .AddScoped<DA_Account>()
            .AddScoped<DA_State>()
            .AddScoped<DA_Township>()
            .AddScoped<DA_Deposit>()
            .AddScoped<DA_Withdraw>()
            .AddScoped<DA_TransactionHistory>();
        return services;
    }

    #endregion

    #region Add DbContext Services

    private static IServiceCollection AddDbContextServices(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder.Services.AddDbContext<AppDbContext>(
            opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            },
            ServiceLifetime.Transient,
            ServiceLifetime.Transient
        );
        return services;
    }

    #endregion
}
