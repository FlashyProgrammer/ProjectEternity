using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Condition", menuName = "Scriptable Objects/Condition")]
public class Condition : ScriptableObject
{
    public float changedSpeed;
    public GameObject enemyTypePrefab;
    public GameObject puzzleAsset;
    public Scene levelScene;
    public AudioClip levelMusic;
}
