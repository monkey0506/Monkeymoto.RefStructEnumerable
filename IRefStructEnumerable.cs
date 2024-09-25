using System.Diagnostics.CodeAnalysis;

namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructEnumerable<T, TEnumerator>
        where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
    {
        [UnscopedRef]
        public TEnumerator GetEnumerator();
    }
}
