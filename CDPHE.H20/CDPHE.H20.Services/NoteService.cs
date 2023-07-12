using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CDPHE.H20.Data.Models;
using Azure.Core;
using System.Diagnostics.Tracing;
using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Queries;
using Dapper;

namespace CDPHE.H20.Services
{
    public interface INoteService
    {
        public Task<string> AddNote(int id, Note note);

    }

    public class NoteService : INoteService
    {
        private readonly DapperContext _dbContext = new DapperContext();

        public async Task<string> AddNote(int id, Note note)
        {
            var query = NoteQuery.AddNote();
            string msg = "{ Success }";
            using(var connection = _dbContext.CreateConnection())
            {
                var notes = await connection.QueryAsync(query, new { RequestId = note.RequestId, Text = note.Text, CreatedAt = DateTime.Now, LastUpdated = DateTime.Now, CreatedBy = id, UpdatedBy = id });
                return msg;
            }
        }

        public async Task<List<Note>> GetNotes(int RequestId)
        {
            var query = NoteQuery.GetNotesByRequestId();
            using(var connection = _dbContext.CreateConnection())
            {
                var notes = await connection.QueryAsync<Note>(query, new { RequestId = RequestId });
                return notes.ToList();
            }
        }
    }
}
