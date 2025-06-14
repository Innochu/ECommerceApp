namespace ECommerceApp.Web;

public class ApiClient(HttpClient httpClient)
{
    public Task<T> GetFromJsonAsync<T>(string path)
    {
        return httpClient.GetFromJsonAsync<T>(path);
    }
    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        return await httpClient.DeleteAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
    {
        return await httpClient.PostAsync(requestUri, content);
    }
}
 