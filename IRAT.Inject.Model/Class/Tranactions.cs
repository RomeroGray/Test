using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRAT.Inject.Model.Class
{
    public class Tranactions
    {
        public string errorMessage { get; set; }

        public List<Data> data { get; set; }

        public class Data
        {
            public string processDate { get; set; }
            public string entityId { get; set; }
            public DateTime transactionDate { get; set; }
            public DateTime createdDate { get; set; }
            public DateTime startDate { get; set; }
            public string confirmation { get; set; }
            public string advancePurchaseDays { get; set; }
            public string participantSold { get; set; }
            public string productSold { get; set; }
            public double priceTotal { get; set; }
            public string feeTotal { get; set; }
            public string cancellationFeeTotal { get; set; }
            public string optionTotal { get; set; }
            public string discountTotal { get; set; }
            public string processingTotal { get; set; }
            public double taxTotal { get; set; }
            public double grossProfit { get; set; }
            public string productId { get; set; }
            public string productName { get; set; }
            public DateTime productStartDate { get; set; }
            public string productCity { get; set; }
            public string productState { get; set; }
            public string productCountry { get; set; }
            public string pickupLocation { get; set; }
            public string returnLocation { get; set; }
            public string customerName { get; set; }
            public object customerCity { get; set; }
            public object customerState { get; set; }
            public object customerCountry { get; set; }
            public string customerPostalCode { get; set; }
            public object agencyId { get; set; }
            public object agencyName { get; set; }
            public object agencyCity { get; set; }
            public object agencyState { get; set; }
            public object agencyCountry { get; set; }
            public object agencyPostalCode { get; set; }
            public object agentName { get; set; }
            public object agentEmail { get; set; }
            public string agentCommission { get; set; }
            public string createdBy { get; set; }
            public string createdByEmail { get; set; }
            public string createdByCommission { get; set; }
            public string originalCreatedBy { get; set; }
            public string originalCreatedByEmail { get; set; }
            public string supplierId { get; set; }
            public string supplierName { get; set; }
            public string supplierUserName { get; set; }
            public string supplierUserEmail { get; set; }
            public double supplierNetRate { get; set; }
            public string activityId { get; set; }
            public string supplierOptionTotal { get; set; }
            public double supplierTaxTotal { get; set; }
            public string supplierFeeTotal { get; set; }
            public string supplierServiceFeeTotal { get; set; }
            public object cancelledDate { get; set; }
            public object referenceId { get; set; }
            public object agentAlternativeId { get; set; }
            public object createdByAlternativeId { get; set; }
            public object originalCreatedByAlternativeId { get; set; }
            public object supplierUserAlternativeId { get; set; }
            public int productNumber { get; set; }
            public object billWeek { get; set; }
            public object billMonth { get; set; }
            public object billYear { get; set; }
            public string transactionId { get; set; }
            public string productLocation { get; set; }
            public string productType { get; set; }
            public string productClass { get; set; }
            public object salesChannel { get; set; }
            public object productSupplier { get; set; }
            public string taxExemptTotal { get; set; }
            public string processingAbsorbTotal { get; set; }
            public double collectedSupplierTaxTotal { get; set; }
            public bool serviceFeeBillable { get; set; }
            public object billDate { get; set; }
            public TierName tierName { get; set; }
            public string supplierDiscountTotal { get; set; }
            public string salesChannelId { get; set; }
            public string processorEntityId { get; set; }
            public bool dateChange { get; set; }
            public object orderAgentCreatedBy { get; set; }
            public object orderAgentCreatedByEmail { get; set; }
            public object orderAgentCreatedByAlternativeId { get; set; }
            public string productCreatedBy { get; set; }
            public string productCreatedByEmail { get; set; }
            public object productCreatedByAlternativeId { get; set; }
            public object productAgentCreatedBy { get; set; }
            public object productAgentCreatedByEmail { get; set; }
            public object productAgentCreatedByAlternativeId { get; set; }
            public object agencyConnectionType { get; set; }
            public string supplierConnectionType { get; set; }
            public DateTime orderStartDate { get; set; }
            public object arDrProcessDate { get; set; }
            public string pickupZone { get; set; }
            public string pickupZoneId { get; set; }
            public string pickupLocationId { get; set; }
            public object discountCount { get; set; }
            public object feeCount { get; set; }
            public object taxCount { get; set; }
            public string itemAddonQuantity { get; set; }
            public string itemAddonTotal { get; set; }
            public string orderCreatedBy { get; set; }
            public double netGrossProfit { get; set; }
            public string processingPayable { get; set; }
            public string percentage { get; set; }
            public string orderId { get; set; }
        }


        public class TierName
        {
            public string key { get; set; }
            public string name { get; set; }
        }



    }
}
