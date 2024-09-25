using System.Runtime.CompilerServices;
using static Monkeymoto.RefStructEnumerable.RefStructEnumerable;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructWhereByRefEnumerator<TEnumerator, T>
    (
        TEnumerator enumerator,
        WhereByRefPredicate<T> predicate
    ) :
        IRefStructByRefEnumerator<RefStructWhereByRefEnumerator<TEnumerator, T>, T>
        where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private readonly WhereByRefPredicate<T> predicate = predicate;

        public ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref enumerator.Current;
        }

        T IRefStructEnumerator<RefStructWhereByRefEnumerator<TEnumerator, T>, T>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructWhereByRefEnumerator<TEnumerator, T> GetEnumerator() => this;

        public bool MoveNext()
        {
            for (bool moveNext = enumerator.MoveNext(); moveNext; moveNext = enumerator.MoveNext())
            {
                if (predicate(ref Current))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
