using System.Collections;
using UnityEngine;

public class Gimmick_Standing_Bomb : MonoBehaviour
{
    public Sprite on_state;     // 활성화 이미지 (불 붙은)
    public Sprite off_state;    // 비활성화 이미지 (불 안붙은)

    private SpriteRenderer spriteRenderer;  // 오브젝트 스프라이트
    private Rigidbody2D rigid;

    private GameObject explosionArea; 

    bool isExploding = false;

    SoundsPlayer SFXPlayer;

    private void Start()
    {
        // 스프라이트 비활성화 이미지로.
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = off_state;

        // 리지드바디타입 kinematic으로.
        rigid = GetComponent<Rigidbody2D>();
        rigid.bodyType = RigidbodyType2D.Kinematic;

        // 폭발영역 오브젝트 비활성화.
        explosionArea = transform.GetChild(0).gameObject;
        explosionArea.SetActive(false);

        isExploding = false;
    }

    private void Awake()
    {
        SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
    }

    // 점화 -> 3초후 폭발.
    public void Ignite()
    {
        // 스프라이트 활성화 이미지, 리지드바디타입 dynamic으로.
        spriteRenderer.sprite = on_state;
        rigid.bodyType = RigidbodyType2D.Dynamic;

        if (!isExploding)
        {
            StartCoroutine(ExplodeCoroutine());
        }
    }

    // 폭발 -> 즉시 폭발.
    public void Explode()
    {
        isExploding = true;

        // 사운드 재생. 폭탄 터지는 소리. (폭탄이 거의 동시에 여러 개가 터질 예정이니 작게 부탁드립니다.)
        SFXPlayer.Gimmick01Sound(0);
        spriteRenderer.sprite = null;
        rigid.bodyType = RigidbodyType2D.Dynamic;

        explosionArea.SetActive(true);  // 폭발영역on. (폭발영역이 몬스터 감지하면, 몬스터에게 데미지 입힘.)
        Destroy(gameObject, 0.1f);      // 파괴.
    }
    
    // 3초 후 폭발시키는 코루틴.
    private IEnumerator ExplodeCoroutine()
    {
        isExploding = true;
        yield return new WaitForSeconds(3f);    // 3초 대기.

        Explode();

        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌체가 폭탄이 아니면 폭발.
        if (!collision.collider.TryGetComponent<Gimmick_Standing_Bomb>(out var gimmick_Standing_Bomb))
        {
            Explode();
        }
    }
}
