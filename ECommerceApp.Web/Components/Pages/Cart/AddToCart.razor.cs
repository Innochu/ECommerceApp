
// Updated Code-behind (.razor.cs file)
using Blazored.Toast.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using ECommerceApp.Web.Components.BaseComponent;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ECommerceApp.Web.Components.Pages.Cart
{
    public partial class AddToCartPage : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; } = default!;

        [Inject]
        private IToastService ToastService { get; set; } = default!;

        public List<Product> products { get; set; } = new List<Product>();
        public Dictionary<int, int> selectedQuantities { get; set; } = new Dictionary<int, int>();
        public bool isLoading { get; set; } = false;
        public bool showModal { get; set; } = false;
        public string modalTitle { get; set; } = "";
        public string modalMessage { get; set; } = "";
        public AppModal? Modal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadProducts();
        }

        protected async Task LoadProducts()
        {
            try
            {
                var res = await ApiClient.GetFromJsonAsync<BaseResponseModel>("api/products");
                if (res != null && res.success)
                {
                    var productsJson = res.Data?.ToString();
                    if (!string.IsNullOrEmpty(productsJson))
                    {
                        products = JsonConvert.DeserializeObject<List<Product>>(productsJson) ?? new List<Product>();

                        // Initialize quantities dictionary
                        selectedQuantities.Clear();
                        foreach (var product in products)
                        {
                            selectedQuantities[product.Id] = 1;
                        }

                        StateHasChanged(); // Force UI update
                    }
                }
                else
                {
                    var errorMsg = res?.ErrorMessage ?? "Failed to load products";
                    ToastService.ShowError(errorMsg);
                }
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"Error loading products: {ex.Message}");
                Console.WriteLine($"LoadProducts Error: {ex}"); // For debugging
            }
        }

        protected async Task AddToCart(int productId)
        {
            try
            {
                // Prevent multiple simultaneous calls
                if (isLoading) return;

                isLoading = true;
                StateHasChanged();

                // Validate quantity
                var quantity = selectedQuantities.ContainsKey(productId) ? selectedQuantities[productId] : 1;
                if (quantity <= 0)
                {
                    quantity = 1;
                    selectedQuantities[productId] = 1;
                }

                var cartItem = new
                {
                    ProductId = productId,
                    Quantity = quantity
                };

                Console.WriteLine($"Adding to cart: ProductId={productId}, Quantity={quantity}"); // Debug log

                var response = await ApiClient.PostAsync<object, BaseResponseModel>("api/cart", cartItem);

                if (response != null)
                {
                    ToastService.ShowSuccess("Item added to cart successfully!");
                    modalTitle = "Success";
                    modalMessage = "Item has been added to your cart successfully!";
                    showModal = true;
                }
                else
                {
                    var errorMessage = "Failed to add item to cart";
                    ToastService.ShowError(errorMessage);
                    modalTitle = "Error";
                    modalMessage = errorMessage;
                    showModal = true;

                }
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error adding to cart: {ex.Message}";
                ToastService.ShowError(errorMsg);
                modalTitle = "Error";
                modalMessage = $"An error occurred: {ex.Message}";
                showModal = true;

            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        protected void CloseModal()
        {
            showModal = false;
            StateHasChanged();
        }
    }}