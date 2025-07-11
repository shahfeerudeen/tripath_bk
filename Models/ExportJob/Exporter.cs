using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Exporter
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ExporterGeneralId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? ExporterMasterId { get; set; }  // FK from ExporterMaster

    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }  // FK from UserManagement

   
    public string? ExporterName { get; set; }  // FK from NewCustomer

    public string? ExportAddress { get; set; }

    public string? ExportBranchSerialNumber { get; set; }

    public string? ExportIECodeNumber { get; set; }

    public string? ExportState { get; set; }

    public string? EXportRegistrationType { get; set; }

    public string? ExportRegistrationNumber { get; set; }

    public string? ExportDBKBank { get; set; }

    public string? ExportDBKAccountNumber { get; set; }

    public string? ExportConsignee { get; set; }  // FK from ConsigneeMaster

    public string? ExportConsigneeAddress { get; set; }

    public string? ExportConsigneeCountry { get; set; }  // FK from CountryMaster

    public bool IsExportBuyerOtherthanConsignee { get; set; }

    public string? ExportRefType { get; set; }

    public DateTime? ExportReferenceDate { get; set; }

    public string? ExportType { get; set; }

    public string? ExportSBNumber { get; set; }

    public DateTime? ExportSBNumberDate { get; set; }

    public string? ExporyRBIApprovalNumber { get; set; }

    public DateTime? ExporyRBIApprovalDate { get; set; }

    public bool IsExportGRWaived { get; set; }

    public string? ExportGRNumber { get; set; }

    public string? ExportRBIWaiverNumber { get; set; }

    public string? ExportBankorDealer { get; set; }

    public string? ExportAccountNumber { get; set; }

    public string? ExportADCode { get; set; }

    public string? ExportEPZCode { get; set; }

    public string? ExportNotify { get; set; }

    public string? ExportNotifyAddress { get; set; }

    public string? ExportSalesPerson { get; set; }

    public string? ExportQuotation { get; set; }

    public string? ExportStatus { get; set; } = "Y";

    public DateTime? ExportEntryDate { get; set; }

    public DateTime? ExportUpdateDate { get; set; }
}
