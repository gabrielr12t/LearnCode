using System.Collections.Generic;
 
namespace LearnCode.Client.FderivControl.Util
{
    public interface IPagedList<T> : IList<T>, ICollection<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
