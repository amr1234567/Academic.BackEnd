namespace Academic.API.Models
{
    public class PagingQuery
    {
        public int Size { set; get; } = 10;
        public int Page { set; get; } = 1;
    }
}