using BlazorProducts.Client.Features;
using BlazorProducts.Shared.Entities;
using BlazorProducts.Shared.RequestFetures;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorProducts.Client.HttpRepository
{
	public interface IProductHttpRepository
	{
		Task<PagingResponse<Product>> GetProductsAsync(ProductParameters productParameters);
		Task CreateProduct(Product product);
		Task<string> UploadProductImage(MultipartFormDataContent content);
		Task<Product> GetProductAsync(string id);
		Task UpdateProductAsync(Product product);
	}
}
