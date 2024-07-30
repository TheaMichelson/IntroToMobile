using Microsoft.Extensions.Logging;
using MVVMStarter.Services;
using MVVMStarter.ViewModels;

namespace MVVMStarter;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        //builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<BasePageViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddScoped<PlanetsPageViewModel>();
        builder.Services.AddScoped<StarsPageViewModel>();

        builder.Services.AddSingleton<IPlanetService, PlanetService>();
        builder.Services.AddSingleton<IStarService, StarService>();


        //builder.Services.AddTransient<DetailPage>();
        //builder.Services.AddTransient<DetailViewModel>();


        return builder.Build();
	}
}
