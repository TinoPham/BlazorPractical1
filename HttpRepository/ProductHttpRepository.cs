using BlazorProducts.Client.Features;
using BlazorProducts.Shared.Entities;
using BlazorProducts.Shared.RequestFetures;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorProducts.Client.HttpRepository
{
	public class ProductHttpRepository : IProductHttpRepository
	{
		private readonly HttpClient _httpClient;
		private readonly JsonSerializerOptions _jsonSerializerOptions;

		public ProductHttpRepository(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public async Task<PagingResponse<Product>> GetProductsAsync(ProductParameters productParameters)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = productParameters.PageNumber.ToString(),
				["searchTerm"] = productParameters.SearchTerm ?? string.Empty,
				["orderBy"] = productParameters.OrderBy
			};

			var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("products", queryStringParam));
			var content = await response?.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			var paginResponse = new PagingResponse<Product>
			{
				Items = JsonSerializer.Deserialize<List<Product>>(content, _jsonSerializerOptions),
				MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _jsonSerializerOptions)
			};

			return paginResponse;
		}

		public async Task CreateProduct(Product product)
		{
			var content = JsonSerializer.Serialize(product);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

			var postResult = await _httpClient.PostAsync("products", bodyContent);
			var postContent = await postResult.Content.ReadAsStringAsync();

			if (!postResult.IsSuccessStatusCode)
			{
				throw new ApplicationException(postContent);
			}
		}

		public async Task<string> UploadProductImage(MultipartFormDataContent content)
		{
			var postResult = await _httpClient.PostAsync("https://localhost:5011/api/upload", content);
			var postContent = await postResult.Content.ReadAsStringAsync();

			if (!postResult.IsSuccessStatusCode)
			{
				throw new ApplicationException(postContent);
			}
			else
			{
				var imgUrl = Path.Combine("https://localhost:5011/", postContent);
				return imgUrl;
			}
		}

		public async Task<Product> GetProductAsync(string id)
		{
			var url = Path.Combine("products", id);

			var response = await _httpClient.GetAsync(url);
			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			var product = JsonSerializer.Deserialize<Product>(content, _jsonSerializerOptions);
			return product;
		}

		public async Task UpdateProductAsync(Product product)
		{
			var content = JsonSerializer.Serialize(product);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var url = Path.Combine("products", product.Id.ToString());

			var putResult = await _httpClient.PutAsync(url, bodyContent);
			var putContent = await putResult.Content.ReadAsStringAsync();

			if (!putResult.IsSuccessStatusCode)
			{
				throw new ApplicationException(putContent);
			}
		}

		public async Task DeleteProductAsync(Guid id)
		{
			var url = Path.Combine("products", id.ToString());

			var deleteResult = await _httpClient.DeleteAsync(url);
			var deleteContent = await deleteResult.Content.ReadAsStringAsync();

			if (!deleteResult.IsSuccessStatusCode)
			{
				throw new ApplicationException(deleteContent);
			}
		}
	}
}
