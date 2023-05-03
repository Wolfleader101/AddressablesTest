using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class ListSerializer<T>
{
    public List<T> data;

    public ListSerializer(List<T> data)
    {
        this.data = data;
    }
}

public class FurnitureJsonManager : MonoBehaviour
{
    private string _filePath;

    private List<RuntimeFurniture> _runtimeFurnitureList = new List<RuntimeFurniture>();
    public List<RuntimeFurniture> RuntimeFurnitureList => _runtimeFurnitureList;

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "furnitureData.json");
        LoadData();
    }
    
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(new ListSerializer<RuntimeFurniture>(_runtimeFurnitureList));
        File.WriteAllText(_filePath, jsonData);
    }

    public void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string jsonData = File.ReadAllText(_filePath);
            _runtimeFurnitureList = JsonUtility.FromJson<ListSerializer<RuntimeFurniture>>(jsonData).data;
        }
    }
}
