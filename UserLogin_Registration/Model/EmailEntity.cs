using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace UserLogin_Registration.Model;

public class EmailEntity
{
    public string FromEmail { get; set; } = string.Empty;
    
    public string ToEmail { get; set; } = string.Empty;
    
    public string Subject { get; set; } = string.Empty;
    
    public string Body { get; set; } = string.Empty;
    
    [ValidateNever]
    public string Attachment { get; set; } = string.Empty;
    
}