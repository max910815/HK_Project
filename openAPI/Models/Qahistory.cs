using System.ComponentModel.DataAnnotations;

namespace openAPI.Models {
    public partial class Qahistory
    {
        [Key]
        public string QahistoryId { get; set; }
        public string QahistoryQ { get; set; }
        public string QahistoryA { get; set; }
        public string QahistoryVectors { get; set; }
        public string ChatId { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
