namespace SISOL.Application.Common.DTOs
{
    internal class PagedRequest
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
    }
}
