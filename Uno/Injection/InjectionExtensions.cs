using Microsoft.Extensions.DependencyInjection;
using Uno;
using Uno.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InjectionExtensions
    {
        public static void AddUno(this IServiceCollection services)
        {
            services.AddSingleton<IPartie, Partie>();
            services.AddSingleton<IPioche, Pioche>();
            services.AddSingleton<ITalon, Talon>();
            services.AddSingleton<ITour, Tour>();
        }
    }
}