using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testing hud", gameObject);
        GameUI hud= FindObjectOfType<GameUI>();
        if(hud)
        {
            hud.SetHeroHUDName(1, "Jim");
            hud.SetHeroHUDName(2, "Bob");
            hud.SetHeroHUDName(3, "Bill");
            hud.SetHeroHUDName(4, "Jam");

            hud.AddHeroHUDMove(3);
            hud.AddHeroHUDMove(3);
            hud.AddHeroHUDRepair(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
