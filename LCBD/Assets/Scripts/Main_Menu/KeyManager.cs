using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyInput {UP,
    DOWN,
    LEFT,
    RIGHT,
    JUMP,
    Run,
    Inventory,
    Item1,
    KEYCOUNT} //�������� Ű ������ ���� ����.

public static class KeySetting { public static Dictionary<KeyInput, KeyCode> keys = new Dictionary<KeyInput, KeyCode>(); }
public class KeyManager : MonoBehaviour
{
    //�ݵ�� enum�� ������� �������
    KeyCode[] defaultKeys = new KeyCode[] { 
        KeyCode.W, 
        KeyCode.S, 
        KeyCode.A, 
        KeyCode.D, 
        KeyCode.Space,
        KeyCode.LeftShift,
        KeyCode.I,
        KeyCode.Alpha1 };
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
    public void ChangeKey(int num) //Ű ����
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
        if (Input.GetKey(KeySetting.keys[KeyInput.UP])) //W�Է�
        {
            Debug.Log("Up");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.DOWN])) //S�Է�
        {
            Debug.Log("DOWN");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.LEFT])) //A�Է�
        {
            Debug.Log("LEFT");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.RIGHT])) //D�Է�
        {
            Debug.Log("RIGHT");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.JUMP])) //����
        {
            Debug.Log("JUMP");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Run])) //�޸���
        {
            Debug.Log("Run");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Inventory])) //�κ��丮
        {
            Debug.Log("Inventory");
        }
        if (Input.GetKey(KeySetting.keys[KeyInput.Item1])) //������1��Ű
        {
            Debug.Log("Item1");
        }
    }
}
