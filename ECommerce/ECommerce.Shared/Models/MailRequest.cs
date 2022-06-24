namespace ECommerce.Helpers.MailHelper;

public class MailRequest
{
    public string ToMail { get; set; }
    public string ConfirmationLink { get; set; }
    public string MailSubject { get; set; }
    public bool IsBodyHtml { get; set; }
    public string MailLinkTitle { get; set; }
    
}