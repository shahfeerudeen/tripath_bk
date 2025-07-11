using tripath.Models;

public class CustomerWithAddressResponse
{
    public CustomerMaster Master { get; set; } = null!;
    public List<CustomerAddress> Addresses { get; set; } = new();
}
