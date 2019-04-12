using KD.KnowledgeBase.UWP.Models;
using System.Collections.Generic;

namespace KD.KnowledgeBase.UWP.Databases
{
    public class RulesDbContext
    {
        public List<SingleServiceModel> Database { get; } = new List<SingleServiceModel>();

        public RulesDbContext()
        {
            this.FillDatabase();
        }

        private void FillDatabase()
        {
            // Salon 1
            this.AddModelToDatabase(new SingleServiceModel
            {
                Location = "Wola",
                Salon = "Ostra brzytwa",
                Hairdressers = new List<HairdresserModel>
                {
                    new HairdresserModel
                    {
                        Name = "Maciek",
                        Services = new List<string>
                        {
                            "Strzyżenie brody"
                        }
                    },
                    new HairdresserModel
                    {
                        Name = "Tomek",
                        Services = new List<string>
                        {
                            "Strzyżenie włosów",
                            "Modelowanie"
                        }
                    }
                }
            });

            // Salon 2
            this.AddModelToDatabase(new SingleServiceModel
            {
                Location = "Centrum",
                Salon = "Cięte",
                Hairdressers = new List<HairdresserModel>
                {
                    new HairdresserModel
                    {
                        Name = "Ania",
                        Services = new List<string>
                        {
                            "Strzyżenie damskie",
                            "Modelowanie"
                        }
                    },
                    new HairdresserModel
                    {
                        Name = "Andrzej",
                        Services = new List<string>
                        {
                            "Farbowanie"
                        }
                    }
                }
            });

            // Salon 3
            this.AddModelToDatabase(new SingleServiceModel
            {
                Location = "Centrum",
                Salon = "Włosomania",
                Hairdressers = new List<HairdresserModel>
                {
                    new HairdresserModel
                    {
                        Name = "Monika",
                        Services = new List<string>
                        {
                            "Strzyżenie damskie",
                            "Farbowanie"
                        }
                    },
                    new HairdresserModel
                    {
                        Name = "Agata",
                        Services = new List<string>
                        {
                            "Modelowanie"
                        }
                    }
                }
            });

            // Salon 4
            this.AddModelToDatabase(new SingleServiceModel
            {
                Location = "Wola",
                Salon = "Hairness",
                Hairdressers = new List<HairdresserModel>
                {
                    new HairdresserModel
                    {
                        Name = "Karol",
                        Services = new List<string>
                        {
                            "Strzyżenie brody",
                            "Strzyżenie włosów"
                        }
                    },
                    new HairdresserModel
                    {
                        Name = "Justyna",
                        Services = new List<string>
                        {
                            "Modelowanie"
                        }
                    }
                }
            });
        }

        private void AddModelToDatabase(SingleServiceModel model)
        {
            this.Database.Add(model);
        }
    }
}
