using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyInput {UP,DOWN,LEFT,RIGHT,JUMP,KEYCOUNT}

public static class KeySetting { public static Dictionary<KeyInput, KeyCode> keys = new Dictionary<KeyInput, KeyCode>(); }
public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space };
    void Awake()
    {
        for(int i=0; i < (int)KeyInput.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyInput)i,defaultKeys[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TestInput();
    }
    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey)
        {
            KeySetting.keys[(KeyInput)key] = keyEvent.keyCode;
            key = -1;
        }
    }
    int key = -1;
    public void ChangeKey(int num) //키 변경
    {
        key = num;
    }

    private void TestInput()
    {
        if (Input.GetKey(KeySetting.keys[KeyInput.UP])) //W입력
        {
            Debug.Log("Up");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.DOWN])) //S입력
        {
            Debug.Log("DOWN");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.LEFT])) //A입력
        {
            Debug.Log("LEFT");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.RIGHT])) //D입력
        {
            Debug.Log("RIGHT");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.JUMP]))
        {
            Debug.Log("JUMP");
        }
    }
}
