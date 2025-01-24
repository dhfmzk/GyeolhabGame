using System;
using System.Collections.Generic;
using Domain;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BoardView : MonoBehaviour
    {
        public CardView[] cards = new CardView[9];

        // private Subject<int> _onClickCards = new Subject<int>();
        // public Subject<int> OnClickCards { get { return _onClickCards; } }

        void Start()
        {
            // eventCache = cards.Select((e, i) => {
            //                     var index = i;
            //                     return e.OnValueChangedAsObservable()
            //                         .Subscribe(x => _onClickCards.OnNext(index));
            //                 })
            //                 .ToList();
        }

        public void UpdateView(in CardData[] data)
        {
            for (var i = 0; i < data.Length; ++i) {
        
                this.cards[i].UpdateView(data[i]);
            }
        }
    }
}
