using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Pages
{

    public class SettingsModel : PageModel
    {
        private readonly LotteryProgram LotteryProgram;

        public SettingsModel(LotteryProgram lotteryProgram)
        {
            LotteryProgram = lotteryProgram;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostResetLottery()
        {
            try
            {
                LotteryProgram.ResetPeriod();
            }
            catch (Exception ex)
            {
                return RedirectToPage();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDrawWinners()
        {
            try
            {
                LotteryProgram.ClosePeriodSales();
                LotteryProgram.Period.DrawWinningTicket();
                LotteryProgram.Period.ComputeWinners();

                var totalRev = GetRevenueForThisPeriod();
               
                var totalProfit = GetProfitForThisPeriod();
                
                var totalLoss = totalRev - totalProfit;
            }
            catch (Exception ex)
            {
                return RedirectToPage("./Error");
            }

            return RedirectToPage();
        }

        private decimal GetRevenueForThisPeriod()
        {
            var totalTickets = LotteryProgram.Period.ResultsByWinLevel().Count();
            return totalTickets * 2;
        }

        private decimal GetProfitForThisPeriod()
        {
            var rev = GetRevenueForThisPeriod();
            var loss = LotteryProgram.Period.winningTicketsL.Sum(t => t.winAmtDollars);
            return rev - loss;
        }
    }
}
