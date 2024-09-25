using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructTakeEnumerator<TEnumerator, T>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructEnumerator<RefStructTakeEnumerator<TEnumerator, T>, T>
        where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = Math.Max(count, 0);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructTakeEnumerator<TEnumerator, T> GetEnumerator() => this;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (count >= 1)
            {
                --count;
                return enumerator.MoveNext();
            }
            return false;
        }
    }
}
