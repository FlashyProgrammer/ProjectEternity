using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string charName;
    public Sprite characterSprite;
    [TextArea(3, 10)]
    public string[] lines; 

}
