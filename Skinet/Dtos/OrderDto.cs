namespace Skinet.Dtos
{
    public class OrderDto
    {
        public string basketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingToAddress { get; set; }

    }
}
