using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New GameBoardModelAsset", menuName = "ScriptableObject/GameBoardModelAsset")]
    public class GameBoardModelAsset : ScriptableObject {

        public Sprite[] cardSpriteList;
    }
}
