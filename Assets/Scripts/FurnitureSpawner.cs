using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [SerializeField] private FurnitureJsonManager jsonManager;

    [SerializeField] private Furniture furnitureItem;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnFurniture(furnitureItem);
    }

    private void OnApplicationQuit()
    {
        jsonManager.SaveData();
    }

    public void SpawnFurniture(Furniture furnitureData)
    {
        // Find the corresponding RuntimeFurniture
        RuntimeFurniture runtimeData = jsonManager.RuntimeFurnitureList
            .Find(data => data.prefabGUID == furnitureData.FurnitureAsset.AssetGUID);

        if (runtimeData == null)
        {
            // If there's no corresponding RuntimeFurniture, create one with the default values
            runtimeData = new RuntimeFurniture
            {
                prefabGUID = furnitureData.FurnitureAsset.AssetGUID,
                dyeColor = furnitureData.DefaultColor
            };
            jsonManager.RuntimeFurnitureList.Add(runtimeData);
        }

        furnitureData.FurnitureAsset.InstantiateAsync().Completed += handle =>
        {
            GameObject obj = handle.Result;
            obj.transform.position = new Vector3(0, 0.5f, 5); // Set desired spawn position here

            // Apply the dye color from the RuntimeFurniture
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = runtimeData.dyeColor;
            }
        };
    }
}
