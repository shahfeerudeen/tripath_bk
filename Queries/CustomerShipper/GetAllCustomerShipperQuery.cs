using MediatR;
using tripath.Models;

public class GetAllCustomerShipperQuery : IRequest<List<CustomerShipper>> { }