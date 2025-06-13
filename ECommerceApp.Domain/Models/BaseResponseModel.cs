namespace ECommerceApp.Domain.Models
{
    public class BaseResponseModel
    {
        public bool success { get; set; }
        public string ErrorMessage { get; set; }
        public Object Data { get; set; }

    }
}
