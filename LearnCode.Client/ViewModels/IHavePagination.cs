namespace LearnCode.Client.ViewModels
{
    public partial interface IHavePagination<T>
    {
        Pagination<T> Pagination { get; }
    }
}
