using Klir.TechChallenge.Domain.Base;
using System;

namespace Klir.TechChallenge.Domain.AggregateModel.Offers
{
    public class Offer : EntityBase
    {
        public virtual int ProductId { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public decimal CalculateDiscount(int count, decimal price)
        {
            int promotionalAmount = count / this.Quantity;
            int amount = count % this.Quantity;
            decimal promotionalPrice = promotionalAmount * this.Price;
            decimal totalPrice = amount * price;

            return (count * price) - (promotionalPrice + totalPrice);
        }
    }
}
