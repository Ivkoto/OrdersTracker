namespace OrdersTracker.API.Endpoints
{
    public interface IEndpoint
    {
        void MapEndpoints(WebApplication app);

        void MapServices(IServiceCollection services);
    }
}
