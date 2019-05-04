using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameBoardView : MonoBehaviour {

    
    public Button[] cards = new Button[9];

    private Subject<int> _onClickCards = new Subject<int>();
    public Subject<int> OnClickCards { get { return _onClickCards; } }


    private List<IDisposable> eventCache;
    void Start() {
        
        eventCache = cards.Select((e, i) => {
                            var index = i;
                            return e.OnClickAsObservable()
                                .Subscribe(x => _onClickCards.OnNext(index));
                        })
                        .ToList();
    }
}
