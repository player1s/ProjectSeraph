using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Logic;
using RazorPagesMovie.Model;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        public List<Job> Jobs = new List<Job>();
        Client client = new Client();
        
        public void OnGet()
        {
            Jobs = client.GetData().Result;

        }
    }
}
