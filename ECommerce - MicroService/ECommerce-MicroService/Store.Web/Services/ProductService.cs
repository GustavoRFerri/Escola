using Store.Web.Models;
using Store.Web.Services.Contracts;
using System.Text;
using System.Text.Json;

namespace Store.Web.Services
{
    public class ProductService : IProductService
    {
        private IHttpClientFactory _clientFactory;
        private const string apiEndPoint = "/api/Products/";
        
        private readonly JsonSerializerOptions _options;
        private ProductViewModel productVM;
        private IEnumerable<ProductViewModel> productsVM;

        public ProductService(IHttpClientFactory clientFactory) 
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {          
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync(apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productsVM = await JsonSerializer
                        .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return productsVM;

        }

        public async Task<ProductViewModel> FindProductById(int id)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            
            using (var response = await client.GetAsync(apiEndPoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productVM = await JsonSerializer
                        .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return productVM;
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
        {
            try
            {
                var client = _clientFactory.CreateClient("ProductApi");

                StringContent content = new StringContent(JsonSerializer.Serialize(productVM),
                                    Encoding.UTF8, "application/json");

                using (var response = client.PostAsync(apiEndPoint, content))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var apiResponse = response.Result.Content.ReadAsStream();
                        productVM = JsonSerializer.Deserialize<ProductViewModel>(apiResponse, _options);
                    }
                    else
                        return null;
                }

                return productVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            ProductViewModel productViewModel = new ProductViewModel();

            using (var response = client.PutAsJsonAsync(apiEndPoint, productVM))
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Result.Content.ReadAsStreamAsync();
                    productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return productViewModel;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            var client = _clientFactory.CreateClient("ProductApi");
            using (var response = client.DeleteAsync(apiEndPoint + id))
            {
                if (response.Result.IsSuccessStatusCode)                
                    return true;
                                
            }

            return false;
        }

      

      

        
    }
}
