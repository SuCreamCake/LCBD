using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyInput {
    UP, //1
    DOWN, //2
    LEFT, //3
    RIGHT, //4
    JUMP, //5
    Inventory, //6
    Item1,  //7
    Item2, //8
    Item3, //9
    KEYCOUNT } //마지막은 키 갯수를 위해 넣음.

public static class KeySetting { public static Dictionary<KeyInput, KeyCode> keys = new Dictionary<KeyInput, KeyCode>(); }
public class KeyManager : MonoBehaviour
{
    //반드시 enum과 순서대로 맞춰야함
    KeyCode[] defaultKeys = new KeyCode[] { 
        KeyCode.W, //UP
        KeyCode.S, //Down
        KeyCode.A,  //Left
        KeyCode.D,  //Right
        KeyCode.Space, //Jump
        KeyCode.I, //Inventory
        KeyCode.Alpha1, //Item1
        KeyCode.Alpha2, //Item2
        KeyCode.Alpha3 //Item3
    };
    void Awake()
    {
        for(int i=0; i < (int)KeyInput.KEYCOUNT; i++) //0부터 10까지 키셋팅키에 추가.
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

    private void TestInput() {
    //UP,
    //DOWN,
    //LEFT,
    //RIGHT,
    //JUMP,
    //Run,
    //Inventory,
    //Item1,
    //
    //
    //
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
        if (Input.GetKey(KeySetting.keys[KeyInput.JUMP])) //점프
        {
            Debug.Log("JUMP");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Inventory])) //인벤토리
        {
            Debug.Log("Inventory");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Item1])) //아이템1번키
        {
            Debug.Log("Item1");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Item2])) //아이템1번키
        {
            Debug.Log("Item2");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Item3])) //아이템1번키
        {
            Debug.Log("Item3");
        }
    }
}
