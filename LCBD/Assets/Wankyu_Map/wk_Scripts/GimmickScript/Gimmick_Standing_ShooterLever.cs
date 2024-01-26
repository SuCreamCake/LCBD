using UnityEngine;

public class Gimmick_Standing_ShooterLever : MonoBehaviour, IControlGimmickObject
{
    private Transform shootersParent;          // 발사 오브젝트 부모.

    private bool isPulled = false;
    private bool isPulling = false;

    private void Awake()
    {
        // 발사 오브젝트 부모 가져옴.
        shootersParent = transform.parent.parent.GetChild(1);
        shootersParent.gameObject.SetActive(true);
    }

    public void ControlGimmickObject()
    {
        if (!isPulling)
        {
            if (!isPulled)
            {
                PullLever();
            }
        }
    }

    private void PullLever()
    {
        isPulling = true;

        isPulled = true;
        shootersParent.gameObject.SetActive(false);

        isPulling = false;
    }
}
