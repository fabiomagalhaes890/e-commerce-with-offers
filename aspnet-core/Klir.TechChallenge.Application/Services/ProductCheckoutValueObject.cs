namespace Klir.TechChallenge.Application.Services
{
    public class ProductCheckoutValueObject
    {
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductValueObject Product { get; set; }
    }
}
