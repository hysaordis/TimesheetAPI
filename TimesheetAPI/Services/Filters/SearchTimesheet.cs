namespace TimesheetAPI.Services.Filters
{
    public class SearchTimesheet
    {
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public string ActivityTypeId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int Page { get; set; }
    }
}