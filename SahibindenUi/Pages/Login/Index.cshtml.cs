using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SahibindenUi.Pages.Login
{
    public class IndexModel : PageModel
    {
        public bool IsLogin { get; set; } = true;
        public void OnGet()
        {
        }
    }
}
