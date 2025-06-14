using Blazored.Toast.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using ECommerceApp.Web.Components.BaseComponent;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ECommerceApp.Web.Components.Pages.ProductFolder
{
    public partial class IndexProduct
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        public List<Product> Products { get; set; }
        public int DeleteID { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }
        public AppModal Modal { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadProduct();
        }

        protected async Task LoadProduct()
        {
            var res = await ApiClient.GetFromJsonAsync<BaseResponseModel>("api/products");
            if (res != null && res.success)
            {
                Products = JsonConvert.DeserializeObject<List<Product>>(res.Data.ToString());
            }

        }
    }
}
