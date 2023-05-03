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
        SpawnFurniture(furnitureItem, "960b0408-aa22-4042-bad3-14c7d65ed075");
        SpawnFurniture(furnitureItem, "0e565c9b-dbab-4e86-ab26-4ac4019eeb66");
    }

    private void OnApplicationQuit()
    {
        jsonManager.SaveData();
    }

    public void SpawnFurniture(Furniture furnitureData, string instanceId)
    {
        furnitureData.FurnitureAsset.InstantiateAsync().Completed += handle =>
        {
            GameObject obj = handle.Result;
            System.Random random = new System.Random();
            
            // Find the corresponding RuntimeFurniture
            RuntimeFurniture runtimeData = jsonManager.RuntimeFurnitureList
                .Find(data => data.instanceGUID == instanceId);
            
            if (runtimeData == null)
            {
                // If there's no corresponding RuntimeFurniture, create one with the default values
                runtimeData = new RuntimeFurniture
                {
                    instanceGUID = instanceId,
                    prefabGUID = furnitureData.FurnitureAsset.AssetGUID,
                    dyeColor = furnitureData.DefaultColor,
                    pos = obj.transform.position,
                    rot = obj.transform.rotation,
                    scale = obj.transform.localScale
                };
                jsonManager.RuntimeFurnitureList.Add(runtimeData);
            }
            
            obj.transform.position = runtimeData.pos;
            obj.transform.rotation = runtimeData.rot;
            obj.transform.localScale = runtimeData.scale;

            // Apply the dye color
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = runtimeData.dyeColor;
            }
        };
    }
}
