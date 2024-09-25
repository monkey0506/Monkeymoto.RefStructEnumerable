using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSelectEnumerator<TSource, TResult, TEnumerator>
    (
        TEnumerator enumerator,
        Func<TSource, TResult> selector
    ) :
        IRefStructEnumerator<TResult, RefStructSelectEnumerator<TSource, TResult, TEnumerator>>
        where TEnumerator : struct, IRefStructEnumerator<TSource, TEnumerator>, allows ref struct
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
        public RefStructSelectEnumerator<TSource, TResult, TEnumerator> GetEnumerator() => this;

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
