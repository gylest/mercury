namespace AngularServer.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public DateTime? RecordCreated { get; set; }
    public DateTime? RecordModified { get; set; }

    public Customer()
    {
    }
}
