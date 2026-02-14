namespace E_Commerce.Shared
{
    public record PagenatedResult<TResult>(int PageIndex, int PageCount, int TotalCount, IEnumerable<TResult> Data);

}
