#nullable enable
namespace NS.CMS.WEB.Models;

public class ErrorViewModel
{

    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
