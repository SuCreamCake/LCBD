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
    private Vector3 attackPosition;

    GameObject equipWeapon;
    //쉴드 오브젝트
    public GameObject shieldObject;

    //적 레이어 마스크
    public LayerMask enemyLayers;
    //음파 공격 시간
    public float soundWaveAttackTime = 0;
    //총 공격량
    public int totalAttackPower;
    //총 방어량
    public int totalShield;

    private GameObject mealAttackOBJ;
    private GameObject soundwaveAttackOBJ;
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        attackPosition = player.transform.right + new Vector3(0.2f, 0.2f, 0);
        soundwaveAttackOBJ = GameObject.Find("SoundWaveOBJ");
        soundwaveAttackOBJ.SetActive(false);
        
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

    //����
    //�Ǽ� ����
    public void punchAttack()
    {
        if (attackTime > (player.attackSpeed / 2) && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            //���� ĳ������ ��ġ ��������
            Vector2 characterPoint = new(transform.position.x, transform.position.y);
            //startX, startY��ǥ ���ϱ� ����, �Ÿ��� ����
            float rangeRadius = player.crossroads / 6.0f; //���� ������ 1/3 1/2 == 1/6
            float rangeRadian = Mathf.Atan2(mousePoint.y - characterPoint.y, mousePoint.x - characterPoint.x);
            //��������ĳ����  ������ (=�߽���)
            float startX = characterPoint.x + rangeRadius * Mathf.Cos(rangeRadian);
            float startY = characterPoint.y + rangeRadius * Mathf.Sin(rangeRadian);
            //��������ĳ����
            Vector2 startAttackPoint = new(startX, startY);
            //���� ������ ���̾ �߰��ϰ� �ش� ���̾ �����ϵ��� ���̾� �߰��ϰ� ���̾� ���� ���� ���� �ʿ� (���� ���̾ ���� ����) (�ϴ� �÷��̾� ������ ��� ���̾� ����)
            //int layerMask = 1 << LayerMask.NameToLayer("monster");
            ////layerMask = ~layerMask;
            //RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, layerMask);
            //if (raycastHit.collider != null)    //��� �����Ǹ�
            //{
            //    Debug.Log("�Ǽ� ���ݿ� ������ ��� ������Ʈ: " + raycastHit.collider.gameObject.tag);
            //    //��¥ �����ؼ� ������ ��� ü�� ����ֱ�
            //}

            //Collider2D hitEnemys = Physics2D.OverlapCircle(startAttackPoint,rangeRadius,enemyLayers);

            RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, enemyLayers);
            if (raycastHit.collider != null)
            {
                CalDamage();
                raycastHit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                Debug.Log("���� ����");

            }


            //��ġ �����
            Debug.Log("mousePoint: " + mousePoint);
            Debug.Log("characterPoint: " + characterPoint);
            Debug.Log("rangeRadian: " + rangeRadian);
            Debug.Log("startAttackPoint: " + startAttackPoint);

            //����ĳ��Ʈ ���� �׸��� ����׿� ���� ����
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * player.crossroads, Color.white, 0.3f);      //ĳ���� ���� ~ ���� ��Ÿ�
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * player.crossroads / 3f, Color.green, 0.3f); //ĳ���� ���� ~ �Ǽ� ��Ÿ�
            Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * rangeRadius, Color.black, 0.3f);     //ĳ���� ���� ~ �� ���� �������� �Ÿ�
            Debug.DrawRay(startAttackPoint, Vector2.up * rangeRadius, Color.red, 0.3f);                     //���� �� ���� ����
            Debug.DrawRay(startAttackPoint, Vector2.down * rangeRadius, Color.red, 0.3f);                   //���� �� �Ʒ��� ����
            Debug.DrawRay(startAttackPoint, Vector2.right * rangeRadius, Color.red, 0.3f);                  //���� �� ������ ����
            Debug.DrawRay(startAttackPoint, Vector2.left * rangeRadius, Color.red, 0.3f);                   //���� �� ���� ����
            Debug.DrawRay(startAttackPoint, Vector2.one.normalized * rangeRadius, Color.red, 0.3f);         //���� �� ����� �밢�� ����
            Debug.DrawRay(startAttackPoint, new Vector2(1, -1).normalized * rangeRadius, Color.red, 0.3f);  //���� �� ������ �밢�� ����
            Debug.DrawRay(startAttackPoint, new Vector2(-1, 1).normalized * rangeRadius, Color.red, 0.3f);  //���� �� �»��� �밢�� ����
            Debug.DrawRay(startAttackPoint, -Vector2.one.normalized * rangeRadius, Color.red, 0.3f);        //���� �� ������ �밢�� ����
        }
    }
    //���� ����
    private void meleeAttack()
    {
        if (attackTime > player.attackSpeed && Input.GetMouseButtonDown(0))
        {
            equipWeapon.GetComponent<animationAttack>().SetAnimMealAttack();
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
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawCube(attackPosition, new Vector2(player.crossroads * 0.3f, 0.7f));
    //}

    //원거리 공격
    private void longDistanceAttack()
    {
        if (attackTime > player.attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //마우스의 위치 가져오기
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            Vector3 playerPos = player.transform.position;

            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            GameObject tempObeject = Instantiate(bulletObject);
            tempObeject.transform.position = attackPosition;
            tempObeject.transform.right = direVec;


        }
    }
    //전투관련 키 입력
    private void getInputBattleKeyKode()
    {
        sDown1 = Input.GetKeyDown(KeyCode.F1);
        sDown2 = Input.GetKeyDown(KeyCode.F2);
        sDown3 = Input.GetKeyDown(KeyCode.F3);
        sDown4 = Input.GetKeyDown(KeyCode.F4);
    }

    private void getInputSoundWaveAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            soundWaveAttack();
            Debug.Log("F누름");
        }
        else if (Input.GetButtonDown("CopyCat"))
        {
            Debug.Log("카피캣 실행");

        }
        else if (Input.GetButtonDown("AlterEgo"))
        {
       
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
        if (sDown2)
        {
            weaponeIndex = 1;
         

        }
        if (sDown3)
        {
            weaponeIndex = 2;
      
        }
        if (sDown4)
        {
            weaponeIndex = 3;
            
        }
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

    //��� ��� �޼���
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
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            if (soundwaveAttackOBJ != null)
                soundwaveAttackOBJ.SetActive(true);
            //���ݹ���
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;
            float angle = Mathf.Atan2(attackForce.y, attackForce.x) * Mathf.Rad2Deg;
            soundwaveAttackOBJ.transform.rotation = Quaternion.Euler(0,0,angle);
            Debug.Log(player.attackPower);
            float startAngle = -player.attackPower / 2;
            Invoke("SoundwaveOff", 2f);
            //�Ϲ� ���� ����
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


           

            //1/3���� ���� ���� ����
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.crossroads / 3, enemyLayers);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("1/3피격 됨");
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
                player.img.fillAmount = player.health / player.maxHealth;

                player.text_hp.text = txt;

            }
        }
    }
    //사운드 웨이브 비활성화
    public void SoundwaveOff()
    {
        soundwaveAttackOBJ.SetActive(false);
    }
    //데미지 공식
    private void CalDamage()
    {
        totalAttackPower += 0/*���� ���ݷ� ���� �� ����*/;
    }
    //��� ����
    private void TotalShield()
    {
        totalShield += 0;/*���⵵ ����������*/;
    }
}
