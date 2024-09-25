using System.Diagnostics.CodeAnalysis;

namespace Monkeymoto.RefStructEnumerable
{
    public interface IRefStructEnumerable<TEnumerator, T>
        where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
    {
        [UnscopedRef]
        public TEnumerator GetEnumerator();
    }
}
