public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    protected string version = "v1";

    public ApiService(string baseUrl)
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_baseUrl);
        //_httpClient.DefaultRequestVersion = new Version("1.0");
    }

    public async Task<string> GetHelloWorld()
    {
        var response = await _httpClient.GetAsync($"api/{version}/Person/hello");
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> GetAllPersons()
    {
        var response = await _httpClient.GetAsync($"api/{version}/Person/persons");
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> GetPersonById(int id)
    {
        var response = await _httpClient.GetAsync($"api/{version}/Person/persons/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}