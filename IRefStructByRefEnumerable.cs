namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructByRefEnumerable<TEnumerator, T> :
        IRefStructEnumerable<TEnumerator, T>
        where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
    { }
}
