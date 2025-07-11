using tripath.Models;

public class CreateCustomerWithAddressRequest
{
    public required CustomerMaster Master { get; set; }
    public CustomerAddress Address { get; set; } = null!;
}
