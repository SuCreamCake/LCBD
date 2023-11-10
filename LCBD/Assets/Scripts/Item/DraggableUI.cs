using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //드래그 기능을 위한 이벤트시스템

//아이템 드래그 스크립트
public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas; //UI가 소속되어 있는 최상단의 Canvas Transform
    Transform previousParent; //해당 오브젝트가 직전에 소속되어 있던 부모 Transform
    RectTransform rect; //UI 위치 제어를 위한 RecTransform
    CanvasGroup canvasGroup; //UI알파값과 상호작용 제어를 위한 캔버스그룹

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) //드래그 시작시 호출
    {
        //드래그 직전에 소속되어 있던 부모 transform 정보 저장
        previousParent = transform.parent;

        //현재 드래그중인 UI가 화면의 최상단에 출력되도록 하기 위해
        transform.SetParent(canvas); //부모 오브젝트를 canvas로 설정
        transform.SetAsLastSibling(); //가장 앞에 보이도록 마지막 자식으로 설정

        //드래그 가능한 오브젝트가 하나가 아닌 자식들을 가지고 있을 수 있어서 CanVasGroup으로 통제
        //알파값을 0.6으로 설정하고, 광선 충돌 처리가 되지 않도록
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) //마우스 드래그중 호출
    {
        Debug.Log("드래그 중");
        //아이템드래그 위치를 마우스 포인터위치로해서 이동
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) //드래그 끝날때 호출
    {
        Debug.Log("드래그 끝");
        if(transform.parent == canvas)
        {
            //마지막에 소속되어있던 previousParent의 자식으로 설정하고, 해당 위치로 설정
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }
        //알파값을 1로 설정하고, 광선 충돌 처리 안되게
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

}
