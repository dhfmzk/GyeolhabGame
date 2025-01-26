using System.Linq;
using Domain;
using R3;
using UnityEngine;

namespace View
{
    public class BoardView : MonoBehaviour
    {
        public CardView[] cards = new CardView[9];

        private readonly Subject<int> _subject = new ();

        void Awake()
        {
            foreach (var (cardView, i) in this.cards.Select((e, i) => (e, i)))
            {
                var index = i;
                cardView.OnClickAsObservable()
                    .Subscribe(x => this._subject.OnNext(index))
                    .AddTo(this);
            }
        }
        
        public Observable<int> OnClickAsObservable()
        {
            return this._subject;
        }

        public void UpdateView(in CardData[] data)
        {
            for (var i = 0; i < data.Length; ++i)
            {
                this.cards[i].UpdateView(data[i]);
            }
        }
    }
}
