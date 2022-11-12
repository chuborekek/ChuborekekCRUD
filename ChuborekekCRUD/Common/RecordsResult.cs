using ChuborekekCRUD.Enum;

namespace ChuborekekCRUD.Common
{
    public class RecordsResult<T> where T : class
    {
        //-------------------------------FINAL RECORDS RETRIEVED AFTER PAGINATING, SORTING, SEARCHING
        public IEnumerable<T> Result { get; set; }
        //------------------------------------fields needed for pagination
        public int TotalItems { get; set; } //total number of records
        public int CurrentPage { get; set; } //current active page
        public int PageSize { get; set; } //number of records to be displayed in page i.e. 10 rows per page
        public int TotalPages { get; set; } //number of pages in pagination bar
        public int StartPage { get; set; } //starting number to be displayed in pagination bar
        public int EndPage { get; set; } //ending number to be displayed in pagination bar
        public string SearchKeyword { get; set; }
        public string SortProperty { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
