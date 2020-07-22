namespace LearnCode.Client.ViewModels
{
    public partial interface IHasPagination<T>
    {
        Pagination<T> Pagination { get; }
    }
}
