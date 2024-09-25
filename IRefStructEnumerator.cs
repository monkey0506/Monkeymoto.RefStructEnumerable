namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructEnumerator<T, TEnumerator> :
        IRefStructEnumerable<T, TEnumerator>
        where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
    {
        public T Current { get; }
        public bool MoveNext();
    }
}
