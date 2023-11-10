using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //�巡�� ����� ���� �̺�Ʈ�ý���

//������ �巡�� ��ũ��Ʈ
public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas; //UI�� �ҼӵǾ� �ִ� �ֻ���� Canvas Transform
    Transform previousParent; //�ش� ������Ʈ�� ������ �ҼӵǾ� �ִ� �θ� Transform
    RectTransform rect; //UI ��ġ ��� ���� RecTransform
    CanvasGroup canvasGroup; //UI���İ��� ��ȣ�ۿ� ��� ���� ĵ�����׷�

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) //�巡�� ���۽� ȣ��
    {
        //�巡�� ������ �ҼӵǾ� �ִ� �θ� transform ���� ����
        previousParent = transform.parent;

        //���� �巡������ UI�� ȭ���� �ֻ�ܿ� ��µǵ��� �ϱ� ����
        transform.SetParent(canvas); //�θ� ������Ʈ�� canvas�� ����
        transform.SetAsLastSibling(); //���� �տ� ���̵��� ������ �ڽ����� ����

        //�巡�� ������ ������Ʈ�� �ϳ��� �ƴ� �ڽĵ��� ������ ���� �� �־ CanVasGroup���� ����
        //���İ��� 0.6���� �����ϰ�, ���� �浹 ó���� ���� �ʵ���
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) //���콺 �巡���� ȣ��
    {
        Debug.Log("�巡�� ��");
        //�����۵巡�� ��ġ�� ���콺 ��������ġ���ؼ� �̵�
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) //�巡�� ������ ȣ��
    {
        Debug.Log("�巡�� ��");
        if(transform.parent == canvas)
        {
            //�������� �ҼӵǾ��ִ� previousParent�� �ڽ����� �����ϰ�, �ش� ��ġ�� ����
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }
        //���İ��� 1�� �����ϰ�, ���� �浹 ó�� �ȵǰ�
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

}
