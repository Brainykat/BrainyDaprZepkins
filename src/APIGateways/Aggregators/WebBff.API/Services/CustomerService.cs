
using CustomerDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebBff.API.Interfaces;

namespace WebBff.API.Services
{
    public class CustomerService : ControllerBase, ICustomerService
    {
        private readonly HttpClient _httpClient;
        //private readonly ILogger<CustomerService> _logger;
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            //_logger = logger;
        }
        public Task<IEnumerable<CustomerView>?> GetCustomersAsync()
        {
            var requestUri = $"api/customers/GetCustomers";
            //_httpClient.DefaultRequestHeaders.Authorization =
            //            new AuthenticationHeaderValue("Bearer", accessToken);
            return _httpClient.GetFromJsonAsync<IEnumerable<CustomerView>>(requestUri);
        }
        public Task<CustomerView?> GetCustomerAsync(Guid customerId)
        {
            var requestUri = $"api/customers/GetCustomer/{customerId}";
            //_httpClient.DefaultRequestHeaders.Authorization =
            //            new AuthenticationHeaderValue("Bearer", accessToken);
            return _httpClient.GetFromJsonAsync<CustomerView>(requestUri);
        }
        public async Task<IActionResult> PostAsync(CustomerDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/customers/Add")
            {
                Content = JsonContent.Create(dto)
            };

            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return new HttpResponseMessageService(response);
        }
        public async Task<IActionResult> UpdateAsync(Guid customerId, CustomerDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/customers/update/{customerId}")
            {
                Content = JsonContent.Create(dto)
            };

            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return new HttpResponseMessageService(response);
        }
        public async Task<IActionResult> DeleteAsync(Guid customerId)
        {
            //try
            //{
                //var token = Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
                //_httpClient.DefaultRequestHeaders.Authorization =
                //        new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await _httpClient.DeleteAsync($"api/customers/Delete/{customerId}");
                this.HttpContext.Response.RegisterForDispose(response);
                return new HttpResponseMessageService(response);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.LogError(_logger, ex, MethodBase.GetCurrentMethod());
            //    return StatusCode(StatusCodes.Status500InternalServerError, ex.SerializeException());
            //}
        }
    }
}
