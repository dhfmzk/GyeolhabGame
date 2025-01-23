using System.Collections;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;


[CreateAssetMenu(fileName = "New GlobalGameModel", menuName = "ScriptableObject/GlobalGameModel")]
public class GlobalGameModel : SingletonScriptableObject<GlobalGameModel> {

    public int maxListSize = 10;
    public List<ScoreData> rankingList;
}
