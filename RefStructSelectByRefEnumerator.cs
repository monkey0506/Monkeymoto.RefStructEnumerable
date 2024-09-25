using System.Runtime.CompilerServices;
using static Monkeymoto.RefStructEnumerable.RefStructEnumerable;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSelectByRefEnumerator<TEnumerator, TSource, TResult>
    (
        TEnumerator enumerator,
        SelectByRefSelector<TSource, TResult> selector
    ) :
        IRefStructByRefEnumerator<RefStructSelectByRefEnumerator<TEnumerator, TSource, TResult>, TResult>
        where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, TSource>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private readonly SelectByRefSelector<TSource, TResult> selector = selector;
        private ref TResult current;

        public ref TResult Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref current;
        }

        TResult IRefStructEnumerator<RefStructSelectByRefEnumerator<TEnumerator, TSource, TResult>, TResult>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructSelectByRefEnumerator<TEnumerator, TSource, TResult> GetEnumerator() => this;

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
