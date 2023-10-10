using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject playerObject;
    Player player;
    //동주 전투
    //공격속도를 체크하기 위한 변수
    public float attackTime = 0;
    //원거리 공격 오브젝트
    public GameObject bulletObject;
    //무기 객체를 담는 자료형
    public GameObject[] weapons;
    //맨손 공격, 근접공격, 원거리 공격 인덱스
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    //무기 인덱스
    public int weaponeIndex = -1;
    //공격위치
    public Vector3 attackPosition;

    GameObject equipWeapon;
    //쉴드 오브젝트
    public GameObject shieldObject;
    //사운드 웨이브 공격
    public GameObject soundWaveAttackObject;
    //적 레이어 마스크
    public LayerMask enemyLayers;
    //음파 공격 시간
    public float soundWaveAttackTime = 0;
    //총 공격량
    public int totalAttackPower;
    //총 방어량
    public int totalShield;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        attackPosition = transform.right + new Vector3(0.2f, 0.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        attackTime += Time.deltaTime;
        soundWaveAttackTime += Time.deltaTime;
        getInputBattleKeyKode();
        swapWeapon();
        battleLogic();
        getInputSoundWaveAttack();
    }

    //동주
    //맨손 공격
    public void punchAttack()
    {
        if (attackTime > (player.attackSpeed / 2) && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            //현재 캐릭터의 위치 가져오기
            Vector2 characterPoint = new(transform.position.x, transform.position.y);
            //startX, startY좌표 구하기 위한, 거리와 각도
            float rangeRadius = player.crossroads / 6.0f; //원의 반지름 1/3 1/2 == 1/6
            float rangeRadian = Mathf.Atan2(mousePoint.y - characterPoint.y, mousePoint.x - characterPoint.x);
            //원형레이캐스팅  시작점 (=중심점)
            float startX = characterPoint.x + rangeRadius * Mathf.Cos(rangeRadian);
            float startY = characterPoint.y + rangeRadius * Mathf.Sin(rangeRadian);
            //원형레이캐스팅
            Vector2 startAttackPoint = new(startX, startY);
            //공격 가능한 레이어를 추가하고 해당 레이어만 감지하도록 레이어 추가하고 레이어 감지 인자 수정 필요 (여러 레이어도 감지 가능) (일단 플레이어 제외한 모든 레이어 감지)
            //int layerMask = 1 << LayerMask.NameToLayer("monster");
            ////layerMask = ~layerMask;
            //RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, layerMask);
            //if (raycastHit.collider != null)    //대상 감지되면
            //{
            //    Debug.Log("맨손 공격에 감지된 대상 오브젝트: " + raycastHit.collider.gameObject.tag);
            //    //진짜 공격해서 감지한 대상 체력 깎아주기
            //}

            //Collider2D hitEnemys = Physics2D.OverlapCircle(startAttackPoint,rangeRadius,enemyLayers);

            RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, enemyLayers);
            if (raycastHit.collider != null)
            {
                CalDamage();
                raycastHit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                Debug.Log("몬스터 맞춤");

            }


            //수치 디버깅
            Debug.Log("mousePoint: " + mousePoint);
            Debug.Log("characterPoint: " + characterPoint);
            Debug.Log("rangeRadian: " + rangeRadian);
            Debug.Log("startAttackPoint: " + startAttackPoint);

            //레이캐스트 범위 그리기 디버그용 추후 삭제
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * player.crossroads, Color.white, 0.3f);      //캐릭터 중점 ~ 원래 사거리
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * player.crossroads / 3f, Color.green, 0.3f); //캐릭터 중점 ~ 맨손 사거리
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * rangeRadius, Color.black, 0.3f);     //캐릭터 중점 ~ 원 범위 중점까지 거리
            Debug.DrawRay(startAttackPoint, Vector2.up * rangeRadius, Color.red, 0.3f);                     //대충 원 위쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.down * rangeRadius, Color.red, 0.3f);                   //대충 원 아래쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.right * rangeRadius, Color.red, 0.3f);                  //대충 원 오른쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.left * rangeRadius, Color.red, 0.3f);                   //대충 원 왼쪽 범위
            Debug.DrawRay(startAttackPoint, Vector2.one.normalized * rangeRadius, Color.red, 0.3f);         //대충 원 우상향 대각선 범위
            Debug.DrawRay(startAttackPoint, new Vector2(1, -1).normalized * rangeRadius, Color.red, 0.3f);  //대충 원 우하향 대각선 범위
            Debug.DrawRay(startAttackPoint, new Vector2(-1, 1).normalized * rangeRadius, Color.red, 0.3f);  //대충 원 좌상향 대각선 범위
            Debug.DrawRay(startAttackPoint, -Vector2.one.normalized * rangeRadius, Color.red, 0.3f);        //대충 원 좌하향 대각선 범위
        }
    }
    //근접 공격
    private void meleeAttack()
    {
        if (attackTime > player.attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            //공격방향
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;

            //공격 범위
            float xRange = player.crossroads * 0.3f;
            float yRange = 0.5f;
            Vector2 boxSize = new Vector2(xRange, yRange);

            float angle = Mathf.Atan2(attackForce.y, attackForce.x) * Mathf.Rad2Deg;
            //공격 콜라이더 생성
            Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPosition, boxSize, angle, enemyLayers);
            Debug.Log(angle);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("몬스터 맞춤");
                    collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                }
            }
            Debug.Log("공격실행");
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(attackPosition, new Vector2(player.crossroads * 0.3f, 0.7f));
    }


    //원거리공격 메서드
    private void longDistanceAttack()
    {
        if (attackTime > player.attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            Vector3 playerPos = transform.position;

            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            GameObject tempObeject = Instantiate(bulletObject);
            tempObeject.transform.position = transform.position;
            tempObeject.transform.right = direVec;


        }
    }

    //전투관련 키 입력
    private void getInputBattleKeyKode()
    {
        sDown1 = Input.GetKeyDown(KeySetting.keys[KeyInput.sDown1]);
        sDown2 = Input.GetKeyDown(KeySetting.keys[KeyInput.sDown2]);
        sDown3 = Input.GetKeyDown(KeySetting.keys[KeyInput.sDown3]);
        sDown4 = Input.GetKeyDown(KeySetting.keys[KeyInput.sDown4]);
    }

    private void getInputSoundWaveAttack()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyInput.SoundWave]))
        {
            soundWaveAttack();
            Debug.Log("특수 능력 누름");
        }
    }
    //공격 타입 인덱스
    private void swapWeapon()
    {
        if (sDown1)
        {
            weaponeIndex = 0;
            Debug.Log("버튼 1활성화");
        }
        if (sDown2) weaponeIndex = 1;
        if (sDown3) weaponeIndex = 2;
        if (sDown4) weaponeIndex = 3;
        if (sDown1 || sDown2 || sDown3 || sDown4)
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = weapons[weaponeIndex];
            if (weaponeIndex != 3)
                equipWeapon.SetActive(true);
        }
    }
    private void battleLogic()
    {
        if (weaponeIndex == 0)
            meleeAttack();
        else if (weaponeIndex == 1)
            longDistanceAttack();
        else if (weaponeIndex == 2)
            punchAttack();
        else if (weaponeIndex == 3)
            shield();

    }

    //방어 방법 메서드
    private void shield()
    {
        if (Input.GetMouseButtonDown(0))
        {
            equipWeapon.SetActive(true);
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            Vector3 playerPos = transform.position;
            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            equipWeapon.transform.position = direVec + (Vector2)transform.position;

            //우측일 경우
            if (Vector3.Dot(transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("우측 방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //위쪽일 경우
            else if (Vector3.Dot(transform.up, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("위측방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }
            else if (Vector3.Dot(-transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("좌측 방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);


            }
            else
            {
                Debug.Log("하단 방패 생성");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {

            equipWeapon.SetActive(false);
        }
    }
    private void soundWaveAttack()
    {

        if (soundWaveAttackTime >= 3.0f)
        {
            soundWaveAttackTime = 0;
            RaycastHit2D[] raycastHit2Ds;
            soundWaveAttackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            //공격방향
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;
            Debug.Log(player.attackPower);
            float startAngle = -player.attackPower / 2;

            //일반 음파 공격
            for (float startAngleIndex = startAngle; startAngleIndex <= player.attackPower / 2; startAngleIndex += 0.5f)
            {
                attackForce = Quaternion.Euler(0, 0, startAngleIndex) * attackForce;
                Debug.Log(startAngleIndex);
                raycastHit2Ds = Physics2D.RaycastAll(transform.position, attackForce, player.crossroads, enemyLayers);
                for (int i = 0; i < raycastHit2Ds.Length; i++)
                {
                    RaycastHit2D hit = raycastHit2Ds[i];
                    if (hit.collider.tag == "monster")
                    {

                        hit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                    }
                }
            }

            //1/3지역 원형 공격 범위
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.crossroads / 3, enemyLayers);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("1/3지역 피격");
                    collider.GetComponent<EnemyHit>().IsCrossroadThird();
                    collider.GetComponent<EnemyHit>().TakeDamage(player.attackPower / 3);
                }
                string txt = "";
                if (player.health <= 0)
                {
                    player.health = 0;
                    txt = "Dead";
                }
                else
                {
                    if (player.health > player.maxHealth)
                        player.health = player.maxHealth;
                    txt = string.Format("{0}/{1}", player.health, player.maxHealth);
                }
            }
        }
    }
    //데미지 공식
    private void CalDamage()
    {
        totalAttackPower += 0/*여기 공격량 공식 들어갈 예정*/;
    }
    //방어 공식
    private void TotalShield()
    {
        totalShield += 0;/*여기도 마찬가지임*/;
    }
}
