using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSelectEnumerator<TEnumerator, TSource, TResult>
    (
        TEnumerator enumerator,
        Func<TSource, TResult> selector
    ) :
        IRefStructEnumerator<RefStructSelectEnumerator<TEnumerator, TSource, TResult>, TResult>
        where TEnumerator : struct, IRefStructEnumerator<TEnumerator, TSource>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private readonly Func<TSource, TResult> selector = selector;
        private TResult current;

        public TResult Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructSelectEnumerator<TEnumerator, TSource, TResult> GetEnumerator() => this;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (enumerator.MoveNext())
            {
                current = selector(enumerator.Current);
                return true;
            }
            return false;
        }
    }
}
