using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomNameGenerator", menuName = "RandomNameGenerator", order = 99)]
public class RandomNameGenerator : ScriptableObject
{
    public TextAsset nameTable;

    #region Singleton Setup
    private static RandomNameGenerator activeInstance;
    public static RandomNameGenerator Instance
    {
        get
        {
            if (activeInstance == null)
            {
                activeInstance = Resources.Load<RandomNameGenerator>("RandomNameGenerator");
            }
            return activeInstance;
        }
    }
    #endregion Singleton Setup

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetRandomName()
    {
        string[] names = Instance.nameTable.text.Split('\n');

        return names[Random.Range(0, names.Length)];
    }
}
