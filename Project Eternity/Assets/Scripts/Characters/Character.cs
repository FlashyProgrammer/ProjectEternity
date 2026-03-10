using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string name;
    public Sprite characterSprite;
    [TextArea(3, 10)]
    public string[] lines; 

}
