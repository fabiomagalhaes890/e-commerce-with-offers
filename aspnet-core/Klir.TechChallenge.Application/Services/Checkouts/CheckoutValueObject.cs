namespace Klir.TechChallenge.Application.Services.Checkouts
{
    public class CheckoutValueObject
    {
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal TotalWithDiscount { get; set; }
    }
}
