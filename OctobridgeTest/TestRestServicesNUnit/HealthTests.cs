namespace Tests;

public class HealthTests
{
    HttpClient     _client;
    const string   _base     = "https://localhost:44366/";
    const string   _healthy = "Healthy";

    [SetUp]
    public void Setup()
    {
        // Create 
        _client = new HttpClient();

        // Set data format and base address
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.BaseAddress = new Uri(_base);
    }

    [Test, Description("Get - Health Check - OK")]
    public async Task Get_HealthCheck()
    {
        Uri uri = new Uri("health", UriKind.Relative);

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            // Get customer  
            string reply  = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

            ClassicAssert.IsTrue(string.Compare(reply, _healthy, true, CultureInfo.CurrentCulture)==0);
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

}
