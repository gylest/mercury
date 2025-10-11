namespace DataMaintenanceEnhanced;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // The IOptions<T> gives you access to the bound configuration values.
    private readonly AppSettings _appSettings;

    readonly HttpClient client  = new();
    readonly Uri        rootURI;
    readonly Uri        productsURI;

    List<Product> products;

    public MainWindow(IOptions<AppSettings> appSettings)
    {
        // Initialize component
        InitializeComponent();

        // Unwrap the IOptions<T> to get the settings object
        _appSettings = appSettings.Value;
        rootURI = new Uri(_appSettings.RootURI);
        productsURI = new Uri(_appSettings.ProductsURI, UriKind.Relative);

        // Set data format
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.BaseAddress = rootURI;

    }

    private static void ShowError(Exception exc, string caption = "Error")
    {
        string messageBoxText = exc.Message;
        MessageBoxButton button = MessageBoxButton.OK;
        MessageBoxImage icon = MessageBoxImage.Error;

        MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
    }

    private async void BtnGet_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await RunGetAsync().ConfigureAwait(true);

            populateGrid();
        }
        catch (Exception exc)
        {
            ShowError(exc);
        }
    }

    private void populateGrid()
    {
        dgOrders.ItemsSource = products;
    }


    private async Task RunGetAsync()
    {
        HttpResponseMessage response = await client.GetAsync(productsURI).ConfigureAwait(true);

        if (response.IsSuccessStatusCode)
        {
            // Get all products
            products = await response.Content.ReadAsAsync<List<Product>>().ConfigureAwait(true);
        }
        else
        {
            Debug.WriteLine($"Rest call failed with: Error={response.StatusCode}");
        }
    }

    private async void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Product product = new Product
            {
                ProductId = Convert.ToInt32(this.txtID.Text, CultureInfo.CurrentCulture),
                Name = this.txtName.Text,
                ProductNumber = this.txtProductNumber.Text,
                ProductCategoryId = Convert.ToInt32(this.txtCategoryId.Text) ,
                Cost = Convert.ToDecimal(this.txtCost.Text)
            };

            await RunPostAsync(product).ConfigureAwait(true);
        }
        catch (Exception exc)
        {
            ShowError(exc);
        }
    }

    private async Task RunPostAsync(Product product)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(productsURI, product).ConfigureAwait(true);
        response.EnsureSuccessStatusCode();
    }

    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Product product = new Product
            {
                ProductId = Convert.ToInt32(this.txtID.Text, CultureInfo.CurrentCulture),
                Name = this.txtName.Text,
                ProductNumber = this.txtProductNumber.Text,
                ProductCategoryId = Convert.ToInt32(this.txtCategoryId.Text),
                Cost = Convert.ToDecimal(this.txtCost.Text)
            };

            await RunPutAsync(product).ConfigureAwait(true);
        }
        catch (Exception exc)
        {
            ShowError(exc);
        }
    }

    private async Task RunPutAsync(Product product)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync($"{productsURI}/{product.ProductId}", product).ConfigureAwait(true);
        response.EnsureSuccessStatusCode();
    }
}

