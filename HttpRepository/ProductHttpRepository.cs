using BlazorProducts.Client.Features;
using BlazorProducts.Shared.Entities;
using BlazorProducts.Shared.RequestFetures;
using Microsoft.AspNetCore.WebUtilities;
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

		public async Task<PagingResponse<Product>> GetProductsAsync(ProductParameters productParameters)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = productParameters.PageNumber.ToString(),
				["searchTerm"] = productParameters.SearchTerm ?? string.Empty
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
	}
}
