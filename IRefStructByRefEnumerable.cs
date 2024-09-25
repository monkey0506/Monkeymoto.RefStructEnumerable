namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructByRefEnumerable<T, TEnumerator> :
        IRefStructEnumerable<T, TEnumerator>
        where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
    { }
}
