using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{

    public string key;
    private Text Text;
    private TextMeshProUGUI TextPro;
    // Use this for initialization
    void Start()
    {

    }

    private void OnEnable()
    {
        Text = GetComponent<Text>();
        TextPro = GetComponent<TextMeshProUGUI>();
        ReloadText();
    }

    public void ReloadText()
    {
        if (Text != null)
        {
            Text.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }
        if (TextPro != null)
        {
            TextPro.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }
    }
}