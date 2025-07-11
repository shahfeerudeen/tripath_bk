using tripath.Models;

namespace tripath.Utils
{
    public static class CustomerValidationHelper
    {
        //To check if any one field is given to update
        public static bool AreAllFieldsEmpty(params string?[] fields)
        {
            return fields.All(string.IsNullOrWhiteSpace);
        }

        public static bool IsCustomerDetailAddressEmpty(CustomerDetailAddress c)
        {
            return AreAllFieldsEmpty(
                c.StateId,
                c.CityId,
                c.CustomerDetailAddressType,
                c.CustomerDetailAddressName,
                c.CustomerDetailAddressLine,
                c.CustomerDetailAddressPostalCode,
                c.CustomerDetailAddressTelephone,
                c.CustomerDetailAddressFax,
                c.CustomerDetailEmailAddress
            );
        }

        public static bool IsCustomerContactEmpty(CustomerContact c)
        {
            bool allStringsEmpty = AreAllFieldsEmpty(
                c.CustomerContactName,
                c.CustomerContactDesignation,
                c.CustomerContactTelephone,
                c.CustomerContactEmailAddress,
                c.CustomerContactDepartment,
                c.CustomerContactMobile
            );

            bool allBooleansFalse =
                !c.CustomerContactIsEmailPrimary
                && !c.CustomerContactIsMobilePrimary
                && !c.CustomerContactIsContactPrimary;

            return allStringsEmpty && allBooleansFalse;
        }

        public static bool IsCustomerCarrierEmpty(CustomerCarrier c)
        {
            // Check if all transport modes are false
            bool allBooleansFalse = !c.IsAir && !c.IsSea && !c.IsRoad && !c.IsRail;

            bool allStringsEmpty = AreAllFieldsEmpty(c.Airline, c.ShippingLine);

            return allBooleansFalse && allStringsEmpty;
        }

        public static bool IsCustomerServicesEmpty(CustomerServices service)
        {
            return !service.IsBank
                && !service.IsContainerFreightStation
                && !service.IsContainerProvider
                && !service.IsContainerTerminalOperator
                && !service.IsContainerYard
                && !service.IsCustomBroker
                && !service.IsFumigationContractor
                && !service.IsInlandContainerDepo
                && !service.IsPackingDepot
                && !service.IsUnPackingDep
                && !service.IsVendor;
        }

        public static bool IsCustomerShipperEmpty(CustomerShipper s)
        {
            return AreAllFieldsEmpty(
                s.CartageHandlerAir,
                s.CartageHandlerLCL,
                s.CartageHandlerFCL,
                s.CustomerServiceSea,
                s.CustomerServiceAir,
                s.CartageCoordinatorSea,
                s.CartageCoordinatorAir,
                s.AccountManagerSea,
                s.AccountManagerAir,
                s.AccountManagerLand
            );
        }

        public static bool IsCustomerConsigneeEmpty(CustomerConsignee c)
        {
            bool stringFieldsEmpty = AreAllFieldsEmpty(
                c.DeliveryHandlerAir,
                c.DeliveryHandlerLCL,
                c.DeliveryHandlerFCL,
                c.DeliveryCoordinatorSea,
                c.DeliveryCoordinatorAir,
                c.CustomerServiceSea,
                c.CustomerServiceAir,
                c.AccountManagerAir,
                c.AccountManagerSea,
                c.AccountManagerLand
            );

            bool numericFieldsEmpty = c.CurrencyUpliftAir == 0 && c.CurrencyUpliftSea == 0;

            return stringFieldsEmpty && numericFieldsEmpty;
        }

        public static bool IsCustomerRegEmpty(CustomerReg r)
        {
            bool stringFieldsEmpty = AreAllFieldsEmpty(
                r.RegistrationType,
                r.Branch,
                r.RegistrationNo
            );

            bool dateFieldsEmpty = r.ValidDate == default && r.ValidUpto == default;

            return stringFieldsEmpty && dateFieldsEmpty;
        }

        public static bool IsCustomerIntegrationEmpty(CustomerIntegration c)
        {
            bool stringFieldsEmpty = AreAllFieldsEmpty(
                c.FrieightJobSharingIntegrationType,
                c.FrieightJobSharingPartnerId,
                c.CustomImportJobSharing,
                c.CustomExportJobSharing,
                c.InvoiceSharing
            );

            return stringFieldsEmpty && c.FrieightJobSharingEnabled == false;
        }
    }
}
