using System.Runtime.CompilerServices;
using static Monkeymoto.RefStructEnumerable.RefStructEnumerable;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructWhereByRefEnumerator<T, TEnumerator>
    (
        TEnumerator enumerator,
        WhereByRefPredicate<T> predicate
    ) :
        IRefStructByRefEnumerator<T, RefStructWhereByRefEnumerator<T, TEnumerator>>
        where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private readonly WhereByRefPredicate<T> predicate = predicate;

        public ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref enumerator.Current;
        }

        T IRefStructEnumerator<T, RefStructWhereByRefEnumerator<T, TEnumerator>>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructWhereByRefEnumerator<T, TEnumerator> GetEnumerator() => this;

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
