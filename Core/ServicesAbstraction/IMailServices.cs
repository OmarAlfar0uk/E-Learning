using Share.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IMailServices
    {
        //Task SendResetPasswordEmailAsync(string toEmail, string resetToken);
        //Task SendConfirmationEmailAsync(string toEmail, string confirmationToken);
        Task SendEmailAsync(Email email);
    }
}
