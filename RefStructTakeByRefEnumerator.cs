using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructTakeByRefEnumerator<TEnumerator, T>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructByRefEnumerator<RefStructTakeByRefEnumerator<TEnumerator, T>, T>
        where TEnumerator : struct, IRefStructByRefEnumerator<TEnumerator, T>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = count;

        public ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref enumerator.Current;
        }

        T IRefStructEnumerator<RefStructTakeByRefEnumerator<TEnumerator, T>, T>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructTakeByRefEnumerator<TEnumerator, T> GetEnumerator() => this;

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
