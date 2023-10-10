using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    private Dictionary<string, string> localizedText;
    private string missingTextString = "Localized text not found";
    private bool isReady = false;

    public void LoadLocalizedText(string fileName)
    {

    localizedText = new Dictionary<string, string>();
        TextAsset mytxtData = Resources.Load<TextAsset>("Texts/" + fileName);
        //���� ���������� �о������ Ȯ��
        //        Debug.Log(Resources.Load<TextAsset>("Texts/"+fileName));
        //        Debug.Log(fileName);
        //        Debug.Log(mytxtData);
        string txt = mytxtData.text;
        if (txt != "" && txt != null)
        {
            string dataAsJson = txt;
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                //�ҷ����� ������ Ȯ��
                //Debug.Log(loadedData.items[i].key + ":" + loadedData.items[i].value);
                //���鵥���Ͱ� ������ ���� �����߻�
                if (!localizedText.ContainsKey(loadedData.items[i].key))
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key].Replace("\\n", "\n");
        }

        return result;

    }

    public void ReloadTexts(string fileName)
    {
        LoadLocalizedText(fileName);
        foreach (LocalizedText text in FindObjectsOfType<LocalizedText>())
        {
            text.ReloadText();
        }
    }
}
