namespace PustokProject.ViewModels.Books.Non_Admin
{
    public class PagedBooksVm<T>
    {
        public PagedBooksVm(bool hasNext, int skip, int take, T items)
        {
            HasNext = hasNext;
            Skip = skip;
            Take = take;
            Items = items;
        }

        public bool HasNext { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public T Items { get; set; }

    }
}
