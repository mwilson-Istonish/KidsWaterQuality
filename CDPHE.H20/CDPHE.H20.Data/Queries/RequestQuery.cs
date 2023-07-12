using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class RequestQuery
    {
        public static string GetNewSampleData()
        {
            string sql = "SELECT lcca.results.createdate, lcca.samplelocations.facility_orgcode AS facility_orgcode, lcca.samplelocations.description AS description, lcca.samplelocations.code AS i_code, lcca.results.edep_samplecollectionstartdate AS i_edep_samplecollectionstartdate, lcca.results.edep_analytemeasurementvalue AS i_edep_analytemeasurementvalue, f_samplelocations.code AS f_code, f_results.edep_samplecollectionstartdate AS f_edep_samplecollectionstartdate, f_results.edep_analytemeasurementvalue AS f_edep_analytemeasurementvalue, lcca.samplelocations.affiliations_id AS affiliations_id FROM (((lcca.samplelocations JOIN lcca.results ON (((lcca.results.edep_samplelocationidentifier = lcca.samplelocations.code) AND (lcca.results.edep_pwsidentifier = lcca.samplelocations.facility_orgcode)))) JOIN lcca.samplelocations f_samplelocations ON (((CONCAT(LEFT(lcca.samplelocations.code, 3), 'F') = f_samplelocations.code) AND (lcca.samplelocations.facility_orgcode = f_samplelocations.facility_orgcode)))) JOIN lcca.results f_results ON (((f_results.edep_samplelocationidentifier = f_samplelocations.code) AND (f_results.edep_pwsidentifier = f_samplelocations.facility_orgcode)))) WHERE (lcca.samplelocations.facility_orgcode IN (SELECT lcca.samplelocations.facility_orgcode FROM (lcca.samplelocations LEFT JOIN lcca.results ON (((lcca.results.edep_samplelocationidentifier = lcca.samplelocations.code) AND (lcca.results.edep_pwsidentifier = lcca.samplelocations.facility_orgcode)))) WHERE ((lcca.results.edep_dwp_lab_data_id IS NULL) AND (lcca.samplelocations.enddate IS NULL) AND (NOT ((lcca.samplelocations.code LIKE '%C'))))) IS FALSE AND lcca.samplelocations.affiliations_id IN (SELECT lcca.documents.affiliations_id FROM lcca.documents WHERE (lcca.documents.type LIKE 'RMDN%')) IS FALSE AND (lcca.samplelocations.code LIKE '%I') AND (lcca.results.edep_analytemeasurementvalue > 4.44) AND results.createdate > DATE_SUB(CURDATE(), INTERVAL 5 DAY)";
            return sql;
        }

        public static string GetAllRequests()
        {
            string sql = "SELECT * FROM [dbo].[Request] where IsActive = 1";
            return sql;
        }

        public static string GetRequestById()
        {
            string sql = "SELECT * FROM [dbo].[Request] WHERE Id = @Id;";
            return sql;
        }

        public static string UpdateRequest()
        {
            string sql = "UPDATE [dbo].[Request] SET UserId = @UserId, FacilityId = @FacilityId, UpdatedBy = @UpdatedBy, LastUpdated = @LastUpdated, IsActive = @IsActive WHERE Id = @Id;";
            return sql;
        }

        public static string AssignRequestToEmployee()
        {   string sql = "UPDATE [dbo].[Request] SET IsAssignedTo = @UserId, UpdatedBy = @UserId, LastUpdated = @Now WHERE Id = @RequestId;";
            return sql;
        }

        public static string InsertNewRequest()
        {
            string sql = "INSERT INTO Request(UserId,FacilityId,Status,CreatedBy,CreatedAt,UpdatedBy,LastUpdated,IsActive) VALUES (@UserId,@FacilityId,@Status,@CreatedBy,@CreatedAt,@UpdatedBy,@LastUpdated,@IsActive); Select @@Identity";
            return sql;
        }

        public static string DeleteRequest()
        {
            string sql = "UPDATE [dbo].[Request] SET IsActive = 0, Status = 'Canceled', UpdatedBy = @UserId, LastUpdated = @Now  WHERE Id = @Id;";
            return sql;
        }

        public static string GetUserAccountRequests()
        {
            string sql = "SELECT * FROM UserAccountRequest";
            return sql;
        }

        public static string GetBudget() 
        {
            string sql = "SELECT TOP 1 * FROM Budget";
            return sql;
        }

    }
}
