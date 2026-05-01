namespace SISOL.Application.Common.DTOs;

internal class PagedResponse<T>
{
    public ICollection<T> Result { get; set; } = [];
    public int TotalRowsPerPage { get; set; }
    public int TotalRows { get; set; }
    public int TotalPages { get; set; }
}
