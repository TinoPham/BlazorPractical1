using BlazorProducts.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

		public async Task<List<Product>> GetProductsAsync()
		{
			var response = await _httpClient.GetAsync("products");
			var content = await response?.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			var products = JsonSerializer.Deserialize<List<Product>>(content, _jsonSerializerOptions);
			return products;
		}
	}
}
