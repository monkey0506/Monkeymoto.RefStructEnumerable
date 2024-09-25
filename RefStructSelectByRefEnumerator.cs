using System.Runtime.CompilerServices;
using static Monkeymoto.RefStructEnumerable.RefStructEnumerable;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSelectByRefEnumerator<TSource, TResult, TEnumerator>
    (
        TEnumerator enumerator,
        SelectByRefSelector<TSource, TResult> selector
    ) :
        IRefStructByRefEnumerator<TResult, RefStructSelectByRefEnumerator<TSource, TResult, TEnumerator>>
        where TEnumerator : struct, IRefStructByRefEnumerator<TSource, TEnumerator>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private readonly SelectByRefSelector<TSource, TResult> selector = selector;
        private ref TResult current;

        public ref TResult Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref current;
        }

        TResult IRefStructEnumerator<TResult, RefStructSelectByRefEnumerator<TSource, TResult, TEnumerator>>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructSelectByRefEnumerator<TSource, TResult, TEnumerator> GetEnumerator() => this;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (enumerator.MoveNext())
            {
                current = ref selector(ref enumerator.Current);
                return true;
            }
            return false;
        }
    }
}
