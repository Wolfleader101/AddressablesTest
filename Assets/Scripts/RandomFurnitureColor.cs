using System.Collections;
using System.Collections.Generic;
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
        var jsonManager = jsonMangers[0];
        
        System.Random random = new System.Random();
    
        foreach (var furnitureItem in furnitureItems)
        {
            // Get the InstanceID component attached to the furniture item
            var instanceIDComponent = furnitureItem.GetComponent<InstanceGUID>();
            if (instanceIDComponent != null)
            {
                // Find the corresponding RuntimeFurniture data in the JsonManager
                var instanceId = instanceIDComponent.GUID;
                var runtimeData = jsonManager.RuntimeFurnitureList
                    .Find(data => data.instanceGUID == instanceId);

                if (runtimeData != null)
                {
                    // Generate a random color
                    Color randomColor = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

                    // Update the dye color in the RuntimeFurniture data
                    runtimeData.dyeColor = randomColor;

                    // Apply the new random color to the furniture item
                    Renderer renderer = furnitureItem.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = randomColor;
                    }
                }
            }
        }

        
        
        jsonManager.SaveData();

    }
}
