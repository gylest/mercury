namespace Tests;

public class OrderTests : DatabaseTestFixture
{

    [Test]
    public void Add_Order()
    {
        const decimal freightCost = 10.00M;
        const decimal subTotal = 621.75M;

        var order = new Order()
        {
            OrderStatus = "Open",
            CustomerId = 1,
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };

        // Save order
        this.Context.Orders.Add(order);
        Context.SaveChanges();
        int id = order.Id;

        // Read Order and check value
        var readOrder = Context.Orders.Find(id);

        // Check query results
        if (readOrder.SubTotal == subTotal)
        {
            Assert.Pass($"New order added and value read successfully = {readOrder.SubTotal}");
        }
        else
        {
            Assert.Fail($"Unable to verify that order successfully stored!");
        }

    }

    [Test]
    public async Task Add_Order_SP()
    {
        const decimal freightCost = 10.00M;
        const decimal subTotal = 89.99M;

        var order = new Order()
        {
            OrderStatus = "Open",
            CustomerId = 2,
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };

        // Call the typed stored procedure method
        var rowsAffected = await Context.Procedures.AddOrderAsync(
            order.OrderStatus,
            order.CustomerId,
            order.FreightAmount,
            order.SubTotal,
            order.TotalDue
        );

        // Check query results
        if (rowsAffected == 1)
        {
            Assert.Pass($"New order added successfully, rows affected = {rowsAffected}");
        }
        else
        {
            Assert.Fail($"Problem adding order, rows affected = {rowsAffected}");
        }

    }

    [Test]
    public void Update_Order()
    {
        int orderId = 3;

        // Check order exists
        var existingOrder = Context.Orders.Find(orderId);
        Context.Entry<Order>(existingOrder).State = EntityState.Detached;

        // Update fields 
        existingOrder.OrderStatus = "Paid";
        existingOrder.PaymentDate = DateTime.Now;

        // Update order
        Context.Orders.Update(existingOrder);

        // Save changes in database
        Context.SaveChanges();

        // Read Order and check value
        var readOrder = Context.Orders.Find(orderId);

        // Check query results
        if (readOrder.OrderStatus == "Paid")
        {
            Assert.Pass($"Existing order updated successfully , order status = {readOrder.OrderStatus}");
        }
        else
        {
            Assert.Fail($"Existing order not updated successfully!");
        }

    }

    [Test]
    public void Delete_Order()
    {
        const decimal freightCost = 10.00M;
        const decimal subTotal = 27.75M;

        var order = new Order()
        {
            OrderStatus = "Open",
            CustomerId = 1,
            FreightAmount = freightCost,
            SubTotal = subTotal,
            TotalDue = subTotal + freightCost
        };

        // Save order
        this.Context.Orders.Add(order);
        Context.SaveChanges();
        int id = order.Id;

        // Delete order
        Context.Orders.Remove(order);
        Context.SaveChanges();

        // Check order does not exist
        var readOrder = Context.Orders.Find(id);

        // Check query results
        if (readOrder is null)
        {
            Assert.Pass($"Order Successfully deleted");
        }
        else
        {
            Assert.Fail($"Order not deleted!");
        }

    }

}
