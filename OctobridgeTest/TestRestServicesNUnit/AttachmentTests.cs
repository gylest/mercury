namespace Tests;

public class AttachmentTests
{
    HttpClient       _client;
    const string     _base     = "https://localhost:44366/";
    List<Attachment> _attachments;

    [SetUp]
    public void Setup()
    {
        // Create 
        _client = new HttpClient();

        // Set data format and base address
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.BaseAddress = new Uri(_base);
    }


    [Test, Description("Get - Attachments All - OK (200)")]
    public async Task Get_Attachments_All()
    {
        Uri      uri = new Uri("api/v1/attachments", UriKind.Relative);

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            // Get attachments
            _attachments = await response.Content.ReadAsAsync<List<Attachment>>().ConfigureAwait(false);

            Assert.Pass($"Count of customers returned = {_attachments.Count}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Get - Attachments by filename - OK (200)")]
    public async Task Get_Attachments_ByFilename()
    {
        Uri uri = new Uri("api/v1/attachments?filename=ceville.jpg", UriKind.Relative);

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            // Get customers
            _attachments = await response.Content.ReadAsAsync<List<Attachment>>().ConfigureAwait(false);

            Assert.Pass($"Count of customers returned = {_attachments.Count}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Get - Attachment by Id - OK (200)")]
    public async Task Get_Attachment_ById_Valid()
    {
        Uri uri = new Uri("api/v1/attachments/2", UriKind.Relative);
        string fullFilename;

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            ContentDispositionHeaderValue headerValue = response.Content.Headers.ContentDisposition;
            fullFilename = Path.Combine(Directory.GetCurrentDirectory(), headerValue.FileNameStar);

            using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
            {
                
                using (Stream streamToWriteTo = File.Open(fullFilename, FileMode.Create))
                {
                    await streamToReadFrom.CopyToAsync(streamToWriteTo);
                }

                response.Content = null;
            }

            Assert.Pass($"Attachment returned and saved as {fullFilename} ");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Get - Attachment by Id - Not Found (404)")]
    public async Task Get_Attachment_ById_Invalid()
    {
        Uri uri = new Uri("api/v1/attachments/99999", UriKind.Relative);

        HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"Attachment found, but not expected");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Assert.Pass($"Expected response code = {response.StatusCode}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Post - Customer - OK (201)")]
    public async Task Post_Attachment_Valid()
    {
        const string fileName = "seaworld-san-diego.jpg";
        string filePath = $"./images/{fileName}";
        Uri uri = new Uri("api/v1/attachments", UriKind.Relative);

        // Create form data from file
        var content = new MultipartFormDataContent();
        var memoryStream = new MemoryStream(File.ReadAllBytes(filePath));
        HttpContent image = new StreamContent(memoryStream);
        string contentType = GetContentType(fileName);
        image.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(image, "file", fileName);

        // Post file
        HttpResponseMessage response = await _client.PostAsync(uri, content).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            // Get customers
            var attachment = await response.Content.ReadAsAsync<Attachment>().ConfigureAwait(false);

            Assert.Pass($"File posted successfully - length = {attachment.Length}");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    [Test, Description("Delete - Attachment by Id - OK (200)")]
    public async Task Delete_Attachment_ById_Valid()
    {
        // Get last customer id
        Uri uriAll = new Uri("api/v1/attachments", UriKind.Relative);
        int lastId = (-1);

        HttpResponseMessage response = await _client.GetAsync(uriAll).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            // Get all customers  
            _attachments = await response.Content.ReadAsAsync<List<Attachment>>().ConfigureAwait(true);

            // Get last id
            lastId = _attachments[_attachments.Count - 1].Id;
        }

        // Delete last attachment
        Uri uri = new Uri($"api/v1/attachments/{lastId}", UriKind.Relative);

        HttpResponseMessage responseDelete = await _client.DeleteAsync(uri).ConfigureAwait(true);

        if (responseDelete.IsSuccessStatusCode)
        {
            Assert.Pass($"Attachment deleted successfully");
        }
        else
        {
            Assert.Fail($"Invalid response code = {response.StatusCode}");
        }

    }

    //
    // Get MIME content type
    //
    public string GetContentType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        string contentType;
        if (!provider.TryGetContentType(fileName, out contentType))
        {
            contentType = "application/octet-stream";
        }
        return contentType;
    }
}
