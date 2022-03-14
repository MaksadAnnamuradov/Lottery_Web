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
    public class LotteryResultsModel : PageModel
    {
        LotteryProgram lp { get; }
        public IEnumerable<LotteryTicket> results { get; set; }


        public IEnumerable<LotteryTicket> WinLotteryTickets;
        public IEnumerable<TicketSale> CurrPeriodWinTickets;
        private readonly LotteryStatistics lotteryStatistics;

        public IEnumerable<TicketSale> ticketSales;
        public IEnumerable<(int periodid, DateTime started)> LotteryPeriods;

        public LotteryResultsModel(LotteryProgram lotteryProgram, LotteryStatistics _lotteryStatistics)
        {
            lotteryStatistics = _lotteryStatistics;
            lp = lotteryProgram;
        }
        public void OnGet()
        {
            LotteryResults();
        }

        public void LotteryResults()
        {
            WinLotteryTickets = lp.Period.ResultsByWinLevel();
            LotteryPeriods = lotteryStatistics.DBPeriodsInHistory();
            var list = LotteryPeriods.ToList();

            CurrPeriodWinTickets = lotteryStatistics.DBStatsOnePeriod(list.Last().periodid);
        }
    }
}