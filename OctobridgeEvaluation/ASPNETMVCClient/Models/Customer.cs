namespace MVCClient.Models;

public partial class Customer
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [StringLength(50)]
    [Display(Name = "Middle Name")]
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    [Required]
    [StringLength(60)]
    [Display(Name = "Address Line 1")]
    public string AddressLine1 { get; set; }
    [StringLength(60)]
    [Display(Name = "Address Line 2")]
    public string AddressLine2 { get; set; }
    [Required]
    [StringLength(30)]
    public string City { get; set; }
    [Required]
    [StringLength(15)]
    [Display(Name = "Post Code")]
    public string PostalCode { get; set; }
    [Required]
    [StringLength(25)]
    public string Telephone { get; set; }
    [Required]
    [StringLength(25)]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email doesn't look like a valid email address.")]
    public string Email { get; set; }
    [Display(Name = "Record Created")]
    public DateTime RecordCreated { get; set; }
    [Display(Name = "Record Modified")]
    public DateTime RecordModified { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
