using KD.KnowledgeBase.UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.KnowledgeBase.UWP.Databases
{
    public class RulesDbContext : IRuleDbContext
    {
        private IList<SingleServiceModel> Database = new List<SingleServiceModel>();

        public IEnumerable<string> Salons => new List<string>
        {
            "Ostra brzytwa",
            "Cięte",
            "Włosomania"
        };
        public IEnumerable<string> Hairdressers => new List<string>
        {
            "Maciek",
            "Tomek",
            "Ania",
            "Agata"
        };
        public IEnumerable<string> Services => new List<string>
        {
            "Strzyżenie włosów męskie",
            "Strzyżenie włosów damskie",
            "Strzyżenie brody",
            "Mycie",
            "Modelowanie"
        };
        public IEnumerable<string> Prices => new List<string>
        {
            "15 zł",
            "30 zł",
            "35 zł",
            "50 zł",
            "75 zł"
        };

        public RulesDbContext()
        {
            this.FillDatabase();
        }

        public bool RuleExists(SingleServiceModel model)
        {
            var result = (from r in Database
                          where r.Cost.Equals(model.Cost)
                          && r.Hairdresser.Equals(model.Hairdresser)
                          && r.Salon.Equals(model.Salon)
                          && r.Service.Equals(model.Service)
                          select r)
                          .ToList()
                          .FirstOrDefault();
            return result != null;
        }

        private void FillDatabase()
        {
            for (int i = 0; i < 100; ++i)
            {
                var model = new SingleServiceModel
                {
                    Cost = this.Prices.ElementAt(new Random().Next(this.Prices.Count() - 1)),
                    Hairdresser = this.Hairdressers.ElementAt(new Random().Next(this.Hairdressers.Count() - 1)),
                    Salon = this.Salons.ElementAt(new Random().Next(this.Salons.Count() - 1)),
                    Service = this.Services.ElementAt(new Random().Next(this.Services.Count() - 1))
                };

                if (!this.RuleExists(model))
                {
                    this.Database.Add(model);
                }
            }
        }
    }
}
