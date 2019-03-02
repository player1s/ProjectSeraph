using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages
{
    public class IndexModel : PageModel
    {
        public ArrayList Jobs = new ArrayList();
        

        public void OnGet()
        {
            Jobs.Add("Hello");
            Jobs.Add("World");
            Jobs.Add("!");

        }
    }
}
