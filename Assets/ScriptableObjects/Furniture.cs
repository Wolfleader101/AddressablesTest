using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Furniture", menuName = "Furniture", order = 0)]
    public class Furniture : ScriptableObject
    {
        [SerializeField] private AssetReference furnitureAsset;
        [SerializeField] private Color defaultColor;

        public AssetReference FurnitureAsset => furnitureAsset;
        public Color DefaultColor => defaultColor;
    }
}