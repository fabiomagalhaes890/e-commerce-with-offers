namespace Klir.TechChallenge.Application.Offers
{
    public class CommandBase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
