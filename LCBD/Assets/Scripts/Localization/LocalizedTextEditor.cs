using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text; // Encoding.UTF8 사용을 위해 추가

public class LocalizedTextEditor : MonoBehaviour
{
    public LocalizationData localizationData;
    private void LoadCSVFile()
    {
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.dataPath, "csv");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath, Encoding.UTF8);
            string[] stringBigList = dataAsJson.Split('\n');
            localizationData = new LocalizationData();
            localizationData.items = new LocalizationItem[stringBigList.Length];
            for (var i = 1; i < stringBigList.Length; i++)
            {
                string[] stringList = stringBigList[i].Split(',');
                for (var j = 0; j < stringList.Length; j++)
                {
                    localizationData.items[i - 1] = new LocalizationItem(stringList[1], stringList[2]);
                }
            }
        }
    }

    private void LoadGameData()
    {
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.dataPath, "txt");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.dataPath, "", "txt");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataAsJson);
        }
    }
}
