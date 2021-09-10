using BlazorProducts.Client.HttpRepository;
using BlazorProducts.Client.Shared;
using BlazorProducts.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Pages
{
    public partial class UpdateProduct
    {
        private Product _product;
        private SuccessNotification _notification;

        [Inject]
        IProductHttpRepository ProductRepo { get; set; }
        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _product = await ProductRepo.GetProductAsync(Id);
        }

        private async Task Update()
        {
            await ProductRepo.UpdateProductAsync(_product);
            _notification.Show();
        }

        private void AssignImageUrl(string imgUrl) => _product.ImageUrl = imgUrl;
    }
}
