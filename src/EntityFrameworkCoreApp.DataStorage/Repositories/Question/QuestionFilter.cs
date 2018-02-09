namespace EntityFrameworkCoreApp.DataStorage.Models
{
    public class QuestionFilter
    {
        public int? From { get; set; }

        public int? ToTake { get; set; }

        public bool? IsVerified { get; set; }

        public bool? IsClosed { get; set; }
    }
}
