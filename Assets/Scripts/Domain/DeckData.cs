using System.Linq;

namespace Domain
{
    public class DeckData
    {
        public int[] Cards { get; private set; }

        public readonly int Size;

        public DeckData(int size)
        {
            this.Size = size;
        }

        public void Reset()
        {
            this.Cards = Enumerable.Range(0, 27)
                .OrderBy(_ => UnityEngine.Random.value)
                .ToArray()[..this.Size];
        }
        
        public int this[int index] => this.Cards[index];
    }
}