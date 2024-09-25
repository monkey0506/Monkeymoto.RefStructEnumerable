namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructEnumerator<TEnumerator, T> :
        IRefStructEnumerable<TEnumerator, T>
        where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
    {
        public T Current { get; }
        public bool MoveNext();
    }
}
