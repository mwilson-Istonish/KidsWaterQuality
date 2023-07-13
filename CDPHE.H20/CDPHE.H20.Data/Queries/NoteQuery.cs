using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class NoteQuery
    {
        public static string GetNotesByRequestId()
        {
            string sql = "SELECT Note.Id, Note.RequestId, Note.Text, [User].LastName + ', ' + [User].FirstName as CreatedBy, Note.CreatedBy as UserId, Note.CreatedAt FROM Note INNER JOIN [User] ON Note.CreatedBy = [User].id WHERE RequestId = @RequestId ORDER BY CreatedAt DESC";
            return sql;
        }

        public static string AddNote()
        {
            string sql = "INSERT INTO [Note] (RequestId, Text, CreatedAt, CreatedBy, LastUpdated, UpdatedBy, IsActive) VALUES (@RequestId, @Text, @CreatedAt, @CreatedBy, @LastUpdated, @UpdatedBy, 'True');";
            return sql;
        }

    }
}
