namespace SISOL.Application.Common.Utils;

internal static class PagingHelpers
{
    public static int CalculateTotalPages(int totalRows, int pageSize)
    {
        if (pageSize <= 0)
            return 0;

        return (int)Math.Ceiling((double)totalRows / pageSize);
    }
}
