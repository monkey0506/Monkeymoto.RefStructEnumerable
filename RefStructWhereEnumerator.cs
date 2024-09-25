using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructWhereEnumerator<T, TEnumerator>
    (
        TEnumerator enumerator,
        Func<T, bool> predicate
    ) :
        IRefStructEnumerator<T, RefStructWhereEnumerator<T, TEnumerator>>
        where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private readonly Func<T, bool> predicate = predicate;

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructWhereEnumerator<T, TEnumerator> GetEnumerator() => this;

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
