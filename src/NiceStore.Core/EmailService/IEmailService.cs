using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Core.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(string to, string from, string subject, string body);
    }
}
