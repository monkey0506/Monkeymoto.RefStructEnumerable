namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructByRefEnumerator<T, TEnumerator> :
        IRefStructByRefEnumerable<T, TEnumerator>,
        IRefStructEnumerator<T, TEnumerator>
        where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
    {
        public new ref T Current { get; }
    }
}
