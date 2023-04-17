using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Services
{
    public interface IEmailService
    {

    }

    public class EmailService : IEmailService
    {
        public EmailService() { }
    
        public async Task<bool> EmailUser(string email)
        {
            return true;
        }
    }
}
