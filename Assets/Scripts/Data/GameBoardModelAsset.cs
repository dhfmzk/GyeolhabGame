using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameBoardModelAsset", menuName = "ScriptableObject/GameBoardModelAsset")]
public class GameBoardModelAsset : ScriptableObject {

    public Sprite[] cardSpriteList;
}
