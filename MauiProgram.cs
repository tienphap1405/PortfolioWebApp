using Microsoft.Extensions.Logging;
using LoginAndRegistration.Database;
using Microsoft.Extensions.DependencyInjection; // Make sure to have this using directive
using Microsoft.AspNetCore.Components.Authorization;
using LoginAndRegistration.Services;
namespace LoginAndRegistration
{
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
                });

            // Register database and authentication services
            builder.Services.AddTransient<UsersDatabase>();
            // Add Maui Blazor WebView and optionally developer tools in debug mode
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
