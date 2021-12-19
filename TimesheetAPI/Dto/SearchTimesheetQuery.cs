using System;
namespace TimesheetAPI.Dto
{
    public class SearchTimesheetQuery
    {
        public int ProjectId { get; set; }
        public int ActivityTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Page { get; set; }
        public object PageSize { get; internal set; }
        public string UserId { get; internal set; }
    }
}