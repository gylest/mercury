namespace MVCClient.Models;

public partial class Product
{
    public int ProductId { get; set; }
    [Required]
    [StringLength(100)]
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Required]
    [StringLength(50)]
    public string ProductNumber { get; set; }
    [Display(Name = "Product Category")]
    public int ProductCategoryId { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    [Range(0.01, 99999.99)]
    public decimal Cost { get; set; }
    [Display(Name = "Record Created")]
    public DateTime RecordCreated { get; set; }
    [Display(Name = "Record Modified")]
    public DateTime RecordModified { get; set; }

    public virtual ProductCategory ProductCategory { get; set; }
}
