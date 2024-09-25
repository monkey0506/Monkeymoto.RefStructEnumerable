using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructTakeEnumerator<T, TEnumerator>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructEnumerator<T, RefStructTakeEnumerator<T, TEnumerator>>
        where TEnumerator : struct, IRefStructEnumerator<T, TEnumerator>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = Math.Max(count, 0);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructTakeEnumerator<T, TEnumerator> GetEnumerator() => this;

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
