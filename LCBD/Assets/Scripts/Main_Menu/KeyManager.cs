using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyInput {
    UP, //1
    DOWN, //2
    LEFT, //3
    RIGHT, //4
    JUMP, //5
    Item1,  //7
    Item2, //8
    Item3, //9
    SoundWave, //10
    sDown1, //11
    sDown2, //12
    sDown3, //13
    sDown4, //14
    TouchNPC, //15
    KEYCOUNT } //키 카운트를 재기위해 keycount설정

public static class KeySetting { public static Dictionary<KeyInput, KeyCode> keys = new Dictionary<KeyInput, KeyCode>(); }
public class KeyManager : MonoBehaviour
{
    //key Code EnumType
    KeyCode[] defaultKeys = new KeyCode[] {
        KeyCode.W, //UP
        KeyCode.S, //Down
        KeyCode.A,  //Left
        KeyCode.D,  //Right
        KeyCode.Space, //Jump
        KeyCode.Alpha1, //Item1
        KeyCode.Alpha2, //Item2
        KeyCode.Alpha3, //Item3
        KeyCode.F, //SoundWave
        KeyCode.F1, //sDown1
        KeyCode.F2, //sDown2
        KeyCode.F3, //sDown3
        KeyCode.F4, //sDown3
        KeyCode.G, //NPC 상호작용 대화
    };
    void Awake()
    {
        KeySetting.keys.Clear(); //Key SetClear
        for(int i=0; i < (int)KeyInput.KEYCOUNT; i++) //Key Change
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
    public void ChangeKey(int num) //키 값 바꾸는 함수
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
        //SoundWave
        //sDown1
        //sDown2
        //sDown3
        //sDown3
        //
        //
        if (Input.GetKey(KeySetting.keys[KeyInput.UP])) //UP
        {
            Debug.Log("Up");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.DOWN])) //Down
        {
            Debug.Log("DOWN");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.LEFT])) //Left
        {
            Debug.Log("LEFT");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.RIGHT])) //Right
        {
            Debug.Log("RIGHT");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.JUMP])) //Jump
        {
            Debug.Log("JUMP");
        }
        if (Input.GetKeyDown(KeySetting.keys[KeyInput.Item1])) //item1
        {
            ItemInventory.instance.selectedItemIndex = 0; // 첫 번째 아이템 슬롯 선택
            ItemInventory.instance.UseSelectedItem(); // 선택된 아이템 사용
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Item2])) //Item2
        {
            ItemInventory.instance.selectedItemIndex = 1; // 첫 번째 아이템 슬롯 선택
            ItemInventory.instance.UseSelectedItem(); // 선택된 아이템 사용
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Item3])) //Item3
        {
            ItemInventory.instance.selectedItemIndex = 2; // 첫 번째 아이템 슬롯 선택
            ItemInventory.instance.UseSelectedItem(); // 선택된 아이템 사용
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.TouchNPC])) //TouchNPC
        {
            Debug.Log("NPC G Input");
        }
    }
}
