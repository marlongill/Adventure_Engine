using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.General;
using AdventureEngine.Engine;

public class Bootstrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject windowHolder = GameObject.Find("Windows");
        WindowHandler.RegisterWindow("Scene", CustomObjects.CreatePanel(canvas, "SceneWindow", "Scene Information", 0, 0, 956, 644));
        WindowHandler.RegisterWindow("Log", CustomObjects.CreatePanel(canvas, "LogWindow", "Log", 0, 648, 956, 320));
        WindowHandler.RegisterWindow("Inventory", CustomObjects.CreatePanel(canvas, "InventoryWindow", "Inventory", 960, 0, 476, 482));
        WindowHandler.RegisterWindow("Actors", CustomObjects.CreatePanel(canvas, "ActorsWindow", "Actors", 960, 486, 476, 482));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
