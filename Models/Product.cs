using System.ComponentModel.DataAnnotations;

namespace WeCommerce.Models;
/// <summary>
/// Represents a product in the store.
/// </summary>
public class Product
{
    /// <summary>
    /// The unique identifier for the product.
    /// </summary>
    [Key]
    public int ProductId { get; set; }

    /// <summary>
    /// The name of the product
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// The product's description
    /// </summary>
    [Required]
    public string Description { get; set; }

    /// <summary>
    /// The product's short description. Used in listings.
    /// </summary>
    [Required]
    public string ShortDescription { get; set; }

    /// <summary>
    /// The price per unit of the product.
    /// </summary>
    [Required]
    [Range(0, double.MaxValue)]
    [DataType(DataType.Currency)]
    public double Price { get; set; }


    /// <summary>
    /// The quantity currently available in the store.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

}