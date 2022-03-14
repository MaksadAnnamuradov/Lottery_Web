using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClassLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Pages
{
    public class StoreModel : PageModel
    {
        private readonly LotteryProgram LotteryProgram;

        public IEnumerable<LotteryTicket> PurchasedTickets;

        [Required]
        public string PlayerNombre;
        public int NumQuickPicks;
        public string Selection;

        public StoreModel(LotteryProgram prog)
        {
            LotteryProgram = prog;
        }

        public void OnGet()
        {

        }


        public IActionResult OnPostQuickPick(string name)
        {
            PlayerNombre = name ?? "Anonymous";
            Selection = "QuickPick";
            PurchasedTickets = LotteryProgram.Period.ResultsByPlayer(name);
          
            return Page();
        }
        public IActionResult OnPostNumberPick(string name)
        {
            PlayerNombre = name ?? "Anonymous";
            Selection = "NumberPick";
            PurchasedTickets = LotteryProgram.Period.ResultsByPlayer(name);
           
            return Page();
        }

        public IActionResult OnPostQuickPickPurchase(string name, int numTickets)
        {
            try
            {
                PlayerNombre = name ?? "Anonymous";
                Selection = "QuickPick";
                NumQuickPicks = numTickets;
                LotteryProgram.Vendor.SellQuickTickets(name, numTickets);
               
            }
            catch (Exception ex)
            {

                return Page();
            }
            PurchasedTickets = LotteryProgram.Period.ResultsByPlayer(name);
            return Page();
        }

        public IActionResult OnPostNumberPickPurchase(string name, int [] ticket)
        {
            try
            {
                PlayerNombre = name ?? "Anonymous";
                Selection = "NumberPick";

                LotteryProgram.Vendor.SellTicket(name, ticket);
            }
            catch (Exception ex)
            {
                return Page();
            }

            PurchasedTickets = LotteryProgram.Period.ResultsByPlayer(name);
            return Page();
        }
    }
}
