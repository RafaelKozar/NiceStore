using MediatR;
using NiceStore.Catalog.Application.Services;
using NiceStore.Catalog.Data;
using NiceStore.Catalog.Data.Repository;
using NiceStore.Catalog.Domain;
using NiceStore.Catalog.Domain.Events;
using NiceStore.Core.Bus;
using NiceStore.Payments.Application.Commands;

namespace NiceStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // add services catolog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductBellowStockEvent>, ProductEventHandler>();

            // add services payment
            services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, OrderCommandHandler>();  


        }
    }
}
