using ECommerce.Helpers.MailHelper;

namespace ECommerce.Shared.Service.Abtract;

public interface IEmailService
{
    bool SendEmail(MailRequest mailRequest);
}