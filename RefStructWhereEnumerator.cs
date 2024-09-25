using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructWhereEnumerator<TEnumerator, T>
    (
        TEnumerator enumerator,
        Func<T, bool> predicate
    ) :
        IRefStructEnumerator<RefStructWhereEnumerator<TEnumerator, T>, T>
        where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private readonly Func<T, bool> predicate = predicate;

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructWhereEnumerator<TEnumerator, T> GetEnumerator() => this;

        public bool MoveNext()
        {
            for (bool moveNext = enumerator.MoveNext(); moveNext; moveNext = enumerator.MoveNext())
            {
                if (predicate(Current))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
