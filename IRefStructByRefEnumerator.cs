namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructByRefEnumerator<TEnumerator, T> :
        IRefStructByRefEnumerable<TEnumerator, T>,
        IRefStructEnumerator<TEnumerator, T>
        where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
    {
        public new ref T Current { get; }
    }
}
