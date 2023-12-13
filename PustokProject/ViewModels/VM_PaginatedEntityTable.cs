namespace PustokProject.ViewModels
{
    public class VM_PaginatedEntityTable<T>
    {
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
