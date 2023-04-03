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

        public decimal CalculateDiscount(int quantityPurchased, decimal productPrice)
        {
            int promotionalAmount = quantityPurchased / this.Quantity;
            int amount = quantityPurchased % this.Quantity;
            decimal promotionalPrice = promotionalAmount * this.Price;
            decimal totalPrice = amount * productPrice;

            return (quantityPurchased * productPrice) - (promotionalPrice + totalPrice);
        }
    }
}
