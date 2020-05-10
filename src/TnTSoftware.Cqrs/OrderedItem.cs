namespace TnTSoftware.Cqrs
{
    public class OrderedItem<T>
    {
        public int Order { get; set; }

        public T Item { get; set; }
    }
}