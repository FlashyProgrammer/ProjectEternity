using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ClassPractice : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Activity 1")]
    [SerializeField] float a;
    [SerializeField] float b;

    [Header("Activity 2")]
    [SerializeField] string playerName;
    [SerializeField] string playerSurname;

    [Header("Activity 3")]
    [SerializeField] int minNumber;
    [SerializeField] int maxNumber;

    [Header("Activity 4")]
    [SerializeField] int[] num = {1,2,3,4,5,};


    [Header("Activity 1 Week 10")]
    public List<string> charName = new List<string>();
    public List<int> intNumbers = new List<int>();
    public Dictionary<string, int> nameAge = new Dictionary<string, int>();

    private void Awake()
    {
        Division(a, b);
        NameSurname(playerName, playerSurname); 
        intNumbers = new List<int> { 1, 2, 3, 4, 5 };
        charName = new List<string> { "Jim", "Joe" };
        charName.Add("John");
        charName.Add("Jane");
        charName.Add("Jack");
        charName.Sort();

        nameAge = new Dictionary<string, int> { { "Michael", 22 }, { "Sarah", 25 }, { "David", 30 }, { "Emily", 28 },{ "George", 29} };

    }

    void Start()
    {
        Debug.Log("The Third value in the dictionary is: " + nameAge.ElementAt(2).Value);

      

      
    }
    void Division(float value1, float value2)
    {
        float result = value1 / value2;
        Debug.Log(result);
        return;
    }

    void NameSurname(string name, string surname)
    {
        Debug.Log("Hello "+ name + " "+ surname);
        return;
    }
    
    void PrintNums(int min, int max)
    {
        for (int i = min; i <= max; i++)
        {
            Debug.Log(i);
            return;
        } 
    }


}
