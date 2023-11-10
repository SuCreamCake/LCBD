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

    //마우스 포인터가 현재 아이템 슬롯 영역 내부로 들어갈 때 1회 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        //아이템 슬롯의 색상을 회색으로 표시
        image.color = Color.gray;
    }

    //마우스 포인터가 현재 아이템 슬롯 영역을 빠져나갈때 1회 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }


    //마우스 포인터가 현재 아이템 슬롯 영역 내부에서 드롭할때 1회 호출
    public void OnDrop(PointerEventData eventData)
    {
        //pointerDrag는 드래그중인 대상인 아이템.
        if(eventData.pointerDrag!=null)
        {
            //드래그하고 있는 대상의 부모를 현재 오브젝트로 설정, 위치를 현재 오브젝트 위치와 동일하게 설정
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }
}
