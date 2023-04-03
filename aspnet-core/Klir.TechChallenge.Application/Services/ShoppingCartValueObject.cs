namespace Klir.TechChallenge.Application.Services
{
    public class ShoppingCartValueObject
    {
        public List<ProductCheckoutValueObject> Products { get; set; }
        public int UserId { get; set; }
    }    
}
