using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardView : MonoBehaviour {

    
    public Toggle[] cards = new Toggle[9];

    // private Subject<int> _onClickCards = new Subject<int>();
    // public Subject<int> OnClickCards { get { return _onClickCards; } }


    private List<IDisposable> eventCache;
    void Start()
    {
        // eventCache = cards.Select((e, i) => {
        //                     var index = i;
        //                     return e.OnValueChangedAsObservable()
        //                         .Subscribe(x => _onClickCards.OnNext(index));
        //                 })
        //                 .ToList();
    }

    // public void UpdateView(ReactiveCollection<Sprite> data)
    // {
    //     for (int i = 0; i < data.Count; ++i) {
    //
    //         cards[i].GetComponent<Image>().sprite = data[i];
    //     }
    // }
}
