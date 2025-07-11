using MediatR;
using tripath.Commands;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class CreateCustomerWithAddressHandler
        : IRequestHandler<CreateCustomerWithAddressCommand, CustomerMaster>
    {
        private readonly ICustomerMasterRepository _masterRepo;
        private readonly ICustomerAddressRepository _addressRepo;

        public CreateCustomerWithAddressHandler(
            ICustomerMasterRepository masterRepo,
            ICustomerAddressRepository addressRepo
        )
        {
            _masterRepo = masterRepo;
            _addressRepo = addressRepo;
        }

        public async Task<CustomerMaster> Handle(
            CreateCustomerWithAddressCommand request,
            CancellationToken cancellationToken
        )
        {
            var master = request.Master;
            master.CustomerEntryDate = DateTime.UtcNow;
            master.CustomerUpdateDate = DateTime.UtcNow;

            var address = request.Address;
            var createdMaster = await _masterRepo.CreateAsync(master);
            address.CustomerId = createdMaster.CustomerId!;
            address.CustomerAddressEntryDate = DateTime.UtcNow;
            address.CustomerAddressUpdateDate = DateTime.UtcNow;

            await _addressRepo.CreateAsync(address);

            return createdMaster;
        }
    }
}
