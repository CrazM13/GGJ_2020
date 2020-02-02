using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    public SoundEvents IDToTest = SoundEvents.Invalid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Test1();
        }
    }

    [ContextMenu("Test Sound")]
    public void Test1()
    {
        SoundSystem.Instance.PlaySound(IDToTest);
    }
}
