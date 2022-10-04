using ECommerce.Helpers.MailHelper;
using ECommerce.Shared.Models;

namespace ECommerce.Shared.Service.Abtract;

public interface IEmailService
{
    bool SendEmail(MailRequest mailRequest);
}