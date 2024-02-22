
namespace WebBff.API.Services
{
    public class FinanceService
    {
        private readonly HttpClient _httpClient;

        public FinanceService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
    }
}
