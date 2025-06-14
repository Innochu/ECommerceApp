using Newtonsoft.Json;

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

    public async Task<T1> PostAsync<T1, T2>(string path, T2 postModel)
    {
       

        var res = await httpClient.PostAsJsonAsync(path, postModel);
        if (res != null && res.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T1>(await res.Content.ReadAsStringAsync());
        }
        return default;
    }

    internal async Task<string?> PostAsync<T1, T2>(string v, object cartItem)
    {
        throw new NotImplementedException();
    }
}
