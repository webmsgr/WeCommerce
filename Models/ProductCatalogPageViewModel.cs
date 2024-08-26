namespace WeCommerce.Models
{
    public class ProductCatalogPageViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
