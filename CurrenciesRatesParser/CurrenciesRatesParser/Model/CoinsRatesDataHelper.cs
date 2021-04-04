﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrenciesRatesParser.Model
{
    public static class CoinsRatesDataHelper
    {
        public static void AddCoinsRatesRange(List<CoinsRate> rates)
        {
            try
            {
                using (var ctx = new Model.Entities())
                {
                    ctx.Database.Connection.Open();

                    rates.ForEach(x =>
                    {
                        x.IsUp = ctx.CoinsRates.OrderByDescending(r => r.Date).FirstOrDefault(g => g.Site == x.Site && g.Acronim == x.Acronim).Sell <= x.Sell;
                    });

                    ctx.CoinsRates.AddRange(rates);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception information: {0}", ex.Message);
            }
        }
    }
}
