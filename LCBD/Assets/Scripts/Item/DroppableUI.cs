using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;

    void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    //���콺 �����Ͱ� ���� ������ ���� ���� ���η� �� �� 1ȸ ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        //������ ������ ������ ȸ������ ǥ��
        image.color = Color.gray;
    }

    //���콺 �����Ͱ� ���� ������ ���� ������ ���������� 1ȸ ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }


    //���콺 �����Ͱ� ���� ������ ���� ���� ���ο��� ����Ҷ� 1ȸ ȣ��
    public void OnDrop(PointerEventData eventData)
    {
        //pointerDrag�� �巡������ ����� ������.
        if(eventData.pointerDrag!=null)
        {
            //�巡���ϰ� �ִ� ����� �θ� ���� ������Ʈ�� ����, ��ġ�� ���� ������Ʈ ��ġ�� �����ϰ� ����
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }
}
