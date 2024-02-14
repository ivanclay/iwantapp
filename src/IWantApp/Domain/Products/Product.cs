namespace IWantApp.Domain.Products;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public bool HasStock { get; set; }
    public string CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; }
    DateTime EditedOn { get; set; }
}
