using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationData : MonoBehaviour
{
   
   public LocalizationItem[] items;

}

[System.Serializable]
public class LocalizationItem
{
    public LocalizationItem(string key, string value) {
    this.key = key;
    this.value = value;
    }

    public string key;
    public string value;

}
