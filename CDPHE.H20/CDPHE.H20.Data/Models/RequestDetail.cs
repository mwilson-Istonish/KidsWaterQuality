﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Models
{
    public class RequestDetail
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int RequestId { get; set; }
        public string SampleName { get; set; }
        public DateTime? InitialSampleDate { get; set; }
        public int SampleResultOperator { get; set; }
        public decimal? InitialSampleResult { get; set; }
        public DateTime? FlushSampleDate { get; set; }
        public int FlushResultOperator { get; set; }
        public decimal? FlushSampleResult { get; set; }
        public int? RemedialActionId { get; set; }
        public decimal? ExpectedMaterialCost { get; set; }
        public decimal? ExpectedLaborCost { get; set; }
        public decimal? ActualMaterialCost { get; set; }
        public decimal? ActualLaborCost { get; set; }
        public DateTime? ConfirmationSampleResultDate { get; set; }
        public int ConfirmationSampleResultOperator { get; set; }
        public decimal? ConfirmationSampleResult { get; set; }
        public bool InHouseLabor { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; }
    }
}
