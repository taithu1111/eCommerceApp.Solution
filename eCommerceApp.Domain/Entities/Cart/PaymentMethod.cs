namespace eCommerceApp.Domain.Entities.Cart
{
    public class PaymentMethod
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
    }
}
