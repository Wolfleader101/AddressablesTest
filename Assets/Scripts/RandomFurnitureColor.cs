using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomFurnitureColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRandomColor(InputAction.CallbackContext value)
    {
        if (!value.started) return;
        
        var furnitureItems = GameObject.FindGameObjectsWithTag("Furniture");
        var jsonMangers = new List<FurnitureJsonManager>(Object.FindObjectsOfType<FurnitureJsonManager>());
        var spawner = new List<FurnitureSpawner>(Object.FindObjectsOfType<FurnitureSpawner>()).ElementAt(0);
        var jsonManager = jsonMangers[0];
        
        System.Random random = new System.Random();
    
        foreach (var furnitureItem in furnitureItems)
        {
            Color randomColor = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            spawner.ChangeDyeColor(furnitureItem, randomColor);
        }

        
        
        jsonManager.SaveData();

    }
}
