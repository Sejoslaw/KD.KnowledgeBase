using System.Collections.Generic;

namespace KD.KnowledgeBase.UWP.Models
{
    public class SingleServiceModel
    {
        public string Location { get; set; }
        public string Salon { get; set; }
        public List<HairdresserModel> Hairdressers { get; set; }
    }

    public class HairdresserModel
    {
        public string Name { get; set; }
        public List<string> Services { get; set; }
    }
}
