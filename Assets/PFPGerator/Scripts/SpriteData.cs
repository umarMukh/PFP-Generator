using UnityEngine;

[CreateAssetMenu(fileName = "SpriteData", menuName = "ScriptableObjects/SpriteData", order = 1)]
public class SpriteData : ScriptableObject
{

    
    public Sprite[] sprites;  // Array of sprites to switch between.
    public Sprite[] SmallSprites;  // Array of sprites to switch between.
}
