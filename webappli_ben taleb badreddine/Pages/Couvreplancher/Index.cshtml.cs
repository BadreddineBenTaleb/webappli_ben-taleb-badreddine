using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webappli_ben_taleb_badreddine.Pages.Couvreplancher
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.GetString("userAuthenticated") != "true")
                Response.Redirect("/utilisateur/Login");
        }
    }
}
