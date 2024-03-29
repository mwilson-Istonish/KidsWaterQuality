﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class RequestDetailQuery
    {
        public static string GetRequestDetailsByRequestId()
        {
            string sql = "SELECT Request.Id, Request.Status, Request.UserId as ProviderId, Request.CreatedAt, Facility.Id as FacilityId, Facility.Name AS FacilityName, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, [User].Phone, Facility.WQCID, Facility.Address1, Facility.Address2, Facility.City, Facility.County, Facility.State, Facility.ZipCode FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.Id = @ID" +
                         " SELECT rd.Id AS Id, rd.SampleName, rd.InitialSampleDate, rd.SampleResultOperator, rd.InitialSampleResult, rd.FlushSampleDate, rd.FlushResultOperator, rd.FlushSampleResult, rd.RemedialActionId, rd.ExpectedMaterialCost, rd.ExpectedLaborCost, rd.ActualMaterialCost, rd.ActualLaborCost, rd.ConfirmationSampleResultDate, rd.ConfirmationSampleResultOperator, rd.ConfirmationSampleResult, rd.InHouseLabor, RemedialAction.Name AS RemedialAction, RemedialAction.Id AS RemedialActionID FROM RequestDetail AS rd INNER JOIN Request AS r ON rd.RequestId = r.Id INNER JOIN RemedialAction ON rd.RemedialActionId = RemedialAction.Id WHERE (r.Id = @ID) AND (rd.IsActive = 1)";
            return sql;
        }

        public static string GetRequestsByProviderId()
        {
            string sql = "SELECT Request.Id AS Id, Facility.Name as Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, Request.CreatedAt, Request.Status FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.UserId = @Id AND Request.IsActive = 1";
            return sql;
        }

        public static string GetRequestsByEmployeeId()
        {
            string sql = "SELECT Request.Id AS Id, Facility.Name as Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, Request.CreatedAt, Request.Status FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.IsAssignedTo = @Id AND Request.IsActive = 1";
            return sql;
        }

        public static string GetRequestsInDraft()
        {
            string sql = "SELECT Request.Id AS Id, Facility.Name as Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, Request.CreatedAt, Request.Status FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.Status = 'Draft' AND Request.IsActive = 1";
            return sql;
        }

        public static string GetRequests()
        {
            string sql = "SELECT Request.Id AS Id, Facility.Name as Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, Request.CreatedAt, Request.Status FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.IsActive = 1";
            return sql;
        }

        public static string GetRequestTotalCost()
        {
            string sql = "SELECT Sum(RequestDetail.ActualMaterialCost) + Sum(RequestDetail.ActualLaborCost) as TotalCost FROM Request INNER JOIN RequestDetail ON Request.Id = RequestDetail.RequestId Where RequestId = @RequestId AND RequestDetail.IsActive = 1";
            return sql;
        }

        public static string InsertNewRequestDetail()
        {
            string sql = "INSERT INTO RequestDetail(RequestId,SampleName,InitialSampleDate,SampleResultOperator,InitialSampleResult,FlushSampleDate,FlushResultOperator,FlushSampleResult,RemedialActionId,ExpectedMaterialCost,ExpectedLaborCost,ActualMaterialCost,ActualLaborCost,ConfirmationSampleResultDate,ConfirmationSampleResultOperator,ConfirmationSampleResult,InHouseLabor,CreatedBy,CreatedAt,UpdatedBy,LastUpdated,IsActive) VALUES (@RequestId,@SampleName,@InitialSampleDate,@SampleResultOperator,@InitialSampleResult,@FlushSampleDate,@FlushResultOperator,@FlushSampleResult,@RemedialActionId,@ExpectedMaterialCost,@ExpectedLaborCost,@ActualMaterialCost,@ActualLaborCost,@ConfirmationSampleResultDate,@ConfirmationSampleResultOperator,@ConfirmationSampleResult,@InHouseLabor,@CreatedBy,@CreatedAt,@UpdatedBy,@LastUpdated,@IsActive); Select @@Identity";
            return sql;
        }

        public static string UpdateRequestDetail()
        {
            string sql = "UPDATE RequestDetail SET SampleName = @SampleName, InitialSampleDate = @InitialSampleDate, SampleResultOperator = @SampleResultOperator, InitialSampleResult = @InitialSampleResult, FlushSampleDate = @FlushSampleDate, FlushResultOperator = @FlushResultOperator, FlushSampleResult = @FlushSampleResult, RemedialActionId = @RemedialActionId, ExpectedMaterialCost = @ExpectedMaterialCost, ExpectedLaborCost = @ExpectedLaborCost, ActualMaterialCost = @ActualMaterialCost, ActualLaborCost = @ActualLaborCost, ConfirmationSampleResultDate = @ConfirmationSampleResultDate, ConfirmationSampleResultOperator = @ConfirmationSampleResultOperator, ConfirmationSampleResult = @ConfirmationSampleResult, InHouseLabor = @InHouseLabor, UpdatedBy = @UpdatedBy, LastUpdated = @LastUpdated WHERE Id = @Id";
            return sql;
        }

        public static string DeleteRequestDetail()
        {
            string sql = "UPDATE RequestDetail SET IsActive = 0, UpdatedBy = @UserId, LastUpdated = @Now  WHERE Id = @Id";
            return sql;
        }

        public static string UpdateRequestFundedInformation()
        {
            string sql = "UPDATE RequestDetail SET ConfirmationSampleResultDate = @ConfirmationSampleResultDate, ConfirmationSampleResultOperator = @ConfirmationSampleResultOperator, ConfirmationSampleResult = @ConfirmationSampleResult, ActualMaterialCost = @ActualMaterialCost, ActualLaborCost = @ActualLaborCost, UpdatedBy = @UpdatedBy, LastUpdated = @LastUpdated WHERE Id = @Id; Select @@Identity";
            return sql;
        }
    }
}
