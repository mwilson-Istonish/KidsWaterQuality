using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.ViewModels
{
    public class RequestAndDetails
    {
        [JsonProperty("id")]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [JsonProperty("facilityId")]
        [JsonPropertyOrder(2)]
        public int FacilityId { get; set; }

        [JsonProperty("wqcid")]
        [JsonPropertyOrder(3)]
        public string WQCID { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipcode")]
        public string ZipCode { get; set; }

        [JsonProperty("provider")]
        [JsonPropertyOrder(3)]
        public string Provider { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("createdAt")]
        [JsonPropertyOrder(4)]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("status")]
        [JsonPropertyOrder(5)]
        public string Status { get; set; }

        [JsonProperty("details")]
        [JsonPropertyOrder(6)]
        public List<ReqDetails> Details = new List<ReqDetails>();

        [JsonProperty("totalCostLabor")]
        [JsonPropertyOrder(7)]
        public decimal TotalCostLabor { get; set; }

        [JsonProperty("totalCostMaterials")]
        [JsonPropertyOrder(8)]
        public decimal TotalCostMaterials { get; set; }

        [JsonProperty("totalCost")]
        [JsonPropertyOrder(9)]
        public decimal TotalCost { get; set; }
    }

    public class ReqDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("requestId")]
        public int RequestId { get; set; }

        [JsonProperty("sampleName")]
        public string? SampleName { get; set; }

        [JsonProperty("initialSampleDate")]
        public DateTime? InitialSampleDate { get; set; }

        [JsonProperty("sampleResultOperator")]
        public int SampleResultOperator { get; set; }

        [JsonProperty("initialSampleResult")]
        public decimal? InitialSampleResult { get; set; }

        [JsonProperty("flushSampleDate")]
        public DateTime? FlushSampleDate { get; set; }

        [JsonProperty("flushResultOperator")]
        public int FlushResultOperator { get; set; }

        [JsonProperty("flushSampleResult")]
        public decimal? FlushSampleResult { get; set; }

        [JsonProperty("remedialActionId")]
        public int? RemedialActionId { get; set; }

        [JsonProperty("expectedMaterialCost")]
        public decimal? ExpectedMaterialCost { get; set; }

        [JsonProperty("expectedLaborCost")]
        public decimal? ExpectedLaborCost { get; set; }

        [JsonProperty("actualMaterialCost")]
        public decimal? ActualMaterialCost { get; set; }

        [JsonProperty("actualLaborCost")]
        public decimal? ActualLaborCost { get; set; }

        [JsonProperty("confirmationSampleResultDate")]
        public DateTime? ConfirmationSampleResultDate { get; set; }

        [JsonProperty("confirmationSampleResultOperator")]
        public int ConfirmationSampleResultOperator { get; set; }

        [JsonProperty("confirmationSampleResult")]
        public decimal? ConfirmationSampleResult { get; set; }

        [JsonProperty("inHouseLabor")]
        public bool InHouseLabor { get; set; }
    }

    //public class RequestAndDetails
    //{
    //    [JsonProperty("id")]
    //    [JsonPropertyOrder(1)]
    //    public int Id { get; set; }

    //    [JsonProperty("facilityId")]
    //    [JsonPropertyOrder(2)]
    //    public int FacilityId { get; set; }

    //    [JsonProperty("wqcid")]
    //    [JsonPropertyOrder(3)]
    //    public string WQCID { get; set; }

    //    [JsonProperty("address1")]
    //    public string Address1 { get; set; }

    //    [JsonProperty("address2")]
    //    public string Address2 { get; set; }

    //    [JsonProperty("city")]
    //    public string City { get; set; }

    //    [JsonProperty("state")]
    //    public string State { get; set; }

    //    [JsonProperty("zipcode")]
    //    public string ZipCode { get; set; }

    //    [JsonProperty("provider")]
    //    [JsonPropertyOrder(3)]
    //    public string Provider { get; set; }

    //    [JsonProperty("email")]
    //    public string Email { get; set; }

    //    [JsonProperty("phone")]
    //    public string Phone { get; set; }

    //    [JsonProperty("createdAt")]
    //    [JsonPropertyOrder(4)]
    //    public DateTime CreatedAt { get; set; }

    //    [JsonProperty("status")]
    //    [JsonPropertyOrder(5)]
    //    public string Status { get; set; }

    //    [JsonProperty("details")]
    //    [JsonPropertyOrder(6)]
    //    public List<ReqDetails> Details = new List<ReqDetails>();

    //    [JsonProperty("totalCostLabor")]
    //    [JsonPropertyOrder(7)]
    //    public decimal TotalCostLabor { get; set; }

    //    [JsonProperty("totalCostMaterials")]
    //    [JsonPropertyOrder(8)]
    //    public decimal TotalCostMaterials { get; set; }

    //    [JsonProperty("totalCost")]
    //    [JsonPropertyOrder(9)]
    //    public decimal TotalCost { get; set; }
    //}
}
