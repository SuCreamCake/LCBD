using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class BodySlot : MonoBehaviour
{
    public string itemName; //�������� �̸�
    public Sprite itemSprite; //���Կ� ǥ�õ� �������� �̹���
    public bool isUse; //��������� ����
    public Image BodyParts_image;
    public Image Item_image;
    public Item item; // ���Կ� �ִ� ������

    public void UseItem() //������ ���
    {
        if (item != null)
        {
            item.Use_Effect(); // �������� ��� ȿ���� �ߵ�
            // ������ ��� �� �߰����� ����, ���� ��� ������ ���ų� ������ ���� ������Ʈ
            //ClearSlot(); // ������ �ʱ�ȭ
        }
    }
    private void Awake()
    {
        Transform child = transform.Find("Image"); // �ڽ� ������Ʈ�� �̸����� ã��
        if (child != null)
            BodyParts_image = child.GetComponent<Image>();
    }
    public BodySlot()
    {
        itemName = "";
        itemSprite = null;
        isUse = false;
    }
    public void ClearSlot() //������ ����� �ʱ�ȭ�� �������� ������������
    {
        itemName = "";
        itemSprite = null;
        BodyParts_image.sprite = null;
        BodyParts_image.enabled = false;
        isUse = false;
        item = null;
    }
}
