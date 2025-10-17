namespace Tests;

public class ProductTests : DatabaseTestFixture
{

    [Test]
    public void Add_Product()
    {
        const decimal itemCost = 19.99M;

        DateTime dtNow = DateTime.Now;

        // Create a unique product number
        Guid myuuid = Guid.NewGuid();
        string productNumber = myuuid.ToString();

        var product = new Product()
        {
            Name = "Joker",
            ProductNumber = productNumber,
            ProductCategoryId = 2,
            Cost = itemCost,
            RecordCreated = dtNow,
            RecordModified = dtNow
        };

        // Save product
        this.Context.Products.Add(product);
        Context.SaveChanges();
        int id = product.ProductId;

        // Read product and check value
        var readProduct = Context.Products.Find(id);

        // Check query results
        if (readProduct.Cost == itemCost)
        {
            Assert.Pass($"New product added and value read successfully = {readProduct.Cost}");
        }
        else
        {
            Assert.Fail($"Unable to verify that product successfully stored!");
        }

    }


    [Test]
    public void Update_Product()
    {
        const decimal updatedCost = 17.50M;

        int productId = 3;

        // Check product exists
        var existingProduct = Context.Products.Find(productId);
        Context.Entry<Product>(existingProduct).State = EntityState.Detached;

        // Update fields 
        existingProduct.Cost = updatedCost;
        existingProduct.RecordModified = DateTime.Now;

        // Update product
        Context.Products.Update(existingProduct);

        // Save changes in database
        Context.SaveChanges();

        // Read product and check value
        var readProduct = Context.Products.Find(productId);

        // Check query results
        if (readProduct.Cost == updatedCost)
        {
            Assert.Pass($"Existing product updated successfully , product cost = {readProduct.Cost}");
        }
        else
        {
            Assert.Fail($"Existing product not updated successfully!");
        }

    }

    [Test]
    public void Delete_Product()
    {
        const decimal itemCost = 79.99M;

        DateTime dtNow = DateTime.Now;

        var product = new Product()
        {
            Name = "Hearthstone",
            ProductNumber = "Blizz010204A",
            ProductCategoryId = 6,
            Cost = itemCost,
            RecordCreated = dtNow,
            RecordModified = dtNow
        };

        // Save product
        this.Context.Products.Add(product);
        Context.SaveChanges();
        int id = product.ProductId;

        // Delete product
        Context.Products.Remove(product);
        Context.SaveChanges();

        // Check product does not exist
        var readProduct = Context.Products.Find(id);

        // Check query results
        if (readProduct is null)
        {
            Assert.Pass($"Product Successfully deleted");
        }
        else
        {
            Assert.Fail($"Product not deleted!");
        }

    }

}
