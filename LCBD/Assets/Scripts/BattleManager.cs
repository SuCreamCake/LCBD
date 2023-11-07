using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject playerObject, gunArm;
    Player player;
    public bool val;


    //���� ����
    //���ݼӵ��� üũ�ϱ� ���� ����
    public float attackTime = 0;
    //���Ÿ� ���� ������Ʈ
    public GameObject bulletObject;
    //���� ��ü�� ��� �ڷ���

    //�Ǽ� ����, ��������, ���Ÿ� ���� �ε���
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    //���� �ε���

    //������ġ
    public Vector3 attackPosition;
    private Animator anim;


    //���� ������Ʈ
    public GameObject shieldObject;
    //���� ���̺� ����
    //public GameObject soundWaveAttackObject;
    //�� ���̾� ����ũ
    public LayerMask enemyLayers;
    //���� ���� �ð�
    public float soundWaveAttackTime = 0;
    //�� ���ݷ�
    public int totalAttackPower;
    //�� ��
    public int totalShield;
    public GameObject soundwaveAttackOBJ;
    public int weaponIndex = -1;

    public GameObject animationEffectMeeleAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        attackPosition = transform.right + new Vector3(0.2f, 0.2f, 0);
        soundwaveAttackOBJ = GameObject.Find("SoundWaveOBJ");
        soundwaveAttackOBJ.SetActive(false);
        val = true;
    }

    // Update is called once per frame
    void Update()
    {
        attackTime += Time.deltaTime;
        soundWaveAttackTime += Time.deltaTime;
        getInputBattleKeyKode();
        //battleLogic();
        getInputSoundWaveAttack();
        //공격방향
        attackPosition = transform.right + new Vector3(0.2f, 0.2f, 0);

        if (player.stage == 2 && val)
        {
            gunArm = GameObject.Find("gunArm");
            gunArm.SetActive(false);
            val = false;
        }

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
                raycastHit.collider.GetComponent<MonsterManager>().TakeDamage(totalAttackPower);
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




            attackTime = 0;
            //���콺�� ��ġ ��������
            //Vector2 mousePoint = Input.mousePosition;
            //mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

            //애니메이션
            player.ani.SetTrigger("isMeleeAttack");
            float key = mousePoint.x - playerObject.transform.position.x;
            if (key > 0)
            {
                key = 1;
                playerObject.transform.localScale = new Vector3(key * 1.5f, 1.5f, 0);
            }
            else if (key < 0)
            {
                key = -1;
                playerObject.transform.localScale = new Vector3(key * 1.5f, 1.5f, 0);
            }

            if (key > 0)
            {
                Instantiate(animationEffectMeeleAttack, playerObject.transform.position + new Vector3(0.8f, 0, 0), new(0, 0, 0, 0));
                Debug.Log("근접공격 실행!!");
                anim = animationEffectMeeleAttack.GetComponent<Animator>();
                anim.SetTrigger("isMeelAttackEffect");
            }
            else
            {
                Instantiate(animationEffectMeeleAttack, playerObject.transform.position + new Vector3(-0.8f, 0, 0), new(0, 0, 0, 0));
                Debug.Log("근접공격 실행!!");
                anim = animationEffectMeeleAttack.GetComponent<Animator>();
                anim.SetTrigger("isMeelAttackEffect");
            }


            //���ݹ���
            Vector2 attackForce = mousePoint - (Vector3)transform.position;
            attackForce = attackForce.normalized;

            //���� ����
            float xRange = player.crossroads * 0.3f;
            float yRange = 0.5f;
            Vector2 boxSize = new Vector2(xRange, yRange);

            float angle = Mathf.Atan2(attackForce.y, attackForce.x) * Mathf.Rad2Deg;
            //���� �ݶ��̴� ����
            Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPosition, boxSize, 0, enemyLayers);
            Debug.Log(angle);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("몬스터를 맞춤");
                    collider.GetComponent<MonsterManager>().TakeDamage(totalAttackPower);
                }
            }
            Debug.Log("���ݽ���");
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(player.transform.position, new Vector2(player.crossroads * 0.3f, 0.7f));
    }


    //원거리 공격
    public bool longDistanceAttack()
    {
        if (attackTime > player.attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //���콺�� ��ġ ��������
            //Vector2 mousePoint = Input.mousePosition;
            //mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

            player.ani.SetTrigger("isGunAttack");
            float key = mousePoint.x - playerObject.transform.position.x;
            if (key > 0)
            {
                key = 1;
                playerObject.transform.localScale = new Vector3(-key * 1.5f, 1.5f, 0);
            }
            else if (key < 0)
            {
                key = -1;
                playerObject.transform.localScale = new Vector3(-key * 1.5f, 1.5f, 0);
            }


            gunArm.SetActive(true);

            Vector3 playerPos = playerObject.transform.position;
            Vector2 direVec = mousePoint - (Vector3)playerPos;
            direVec = direVec.normalized;
            GameObject tempObeject = Instantiate(bulletObject);
            tempObeject.GetComponent<Bullet>().SetDamage(totalAttackPower);
            tempObeject.transform.position = playerObject.transform.position;
            tempObeject.transform.right = direVec;
            return true;
        }
        return false;
    }

    //�������� Ű �Է�
    private void getInputBattleKeyKode()
    {
        sDown1 = Input.GetKeyDown(KeyCode.F1);
        sDown2 = Input.GetKeyDown(KeyCode.F2);
        sDown3 = Input.GetKeyDown(KeyCode.F3);
        sDown4 = Input.GetKeyDown(KeyCode.F4);
        swapWeapon();
    }

    private void getInputSoundWaveAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            soundWaveAttack();
            Debug.Log("F����");
            player.ani.SetTrigger("isSkill");
        }
    }
    //���� Ÿ�� �ε���
    private void swapWeapon()
    {
        if (sDown1)
        {
            weaponIndex = 0;
            Debug.Log("��ư 1Ȱ��ȭ");
        }
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;
        if (sDown4) weaponIndex = 3;
        //if (sDown1 || sDown2 || sDown3 || sDown4)
        //{
        //    if (shieldObject != null)
        //        shieldObject.SetActive(false);
        //    shieldObject = weapons[weaponIndex];
        //    shieldObject.SetActive(true);
        //}
    }
    public void battleLogic()
    {
        Debug.Log("여기 실행됨 배틀로직 함수");
        Debug.Log("weaponIndex");
        if (weaponIndex == 0)
            meleeAttack();
        else if (weaponIndex == 1)
            longDistanceAttack();
        else if (weaponIndex == 2)
            punchAttack();
        else if (weaponIndex == 3)
            shield();

    }

    //��� ��� �޼���
    private void shield()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shieldObject.SetActive(true);
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            Vector3 playerPos = transform.position;
            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            shieldObject.transform.position = direVec + (Vector2)transform.position;

            //������ ���
            if (Vector3.Dot(transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("���� ���� ����");
                shieldObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //������ ���
            else if (Vector3.Dot(transform.up, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("�������� ����");
                shieldObject.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }
            else if (Vector3.Dot(-transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("���� ���� ����");
                shieldObject.transform.rotation = Quaternion.Euler(0, 0, 0);


            }
            else
            {
                Debug.Log("�ϴ� ���� ����");
                shieldObject.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {

            shieldObject.SetActive(false);
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

            //���ݹ���
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;
            Debug.Log(player.attackPower);
            float startAngle = -player.attackPower / 2;

            //�Ϲ� ���� ����
            //for (float startAngleIndex = startAngle; startAngleIndex <= player.attackPower / 2; startAngleIndex += 0.5f)
            //{
            //    attackForce = Quaternion.Euler(0, 0, startAngleIndex) * attackForce;
            //    Debug.Log(startAngleIndex);
            //    raycastHit2Ds = Physics2D.RaycastAll(transform.position, attackForce, player.crossroads, enemyLayers);
            //    for (int i = 0; i < raycastHit2Ds.Length; i++)
            //    {
            //        RaycastHit2D hit = raycastHit2Ds[i];
            //        if (hit.collider.tag == "monster")
            //        {

            //            hit.collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
            //        }
            //    }
            //}
            //if (soundwaveAttackOBJ != null)
            //    soundwaveAttackOBJ.SetActive(true);
            //float angle = Mathf.Atan2(attackForce.y, attackForce.x) * Mathf.Rad2Deg;
            //soundwaveAttackOBJ.transform.rotation = Quaternion.Euler(0, 0, angle);
            //Invoke("SoundwaveOff", 2f);



            Collider2D[] colliders = Physics2D.OverlapCircleAll(playerObject.transform.position, player.crossroads / 3, enemyLayers);
            Instantiate(soundwaveAttackOBJ, playerObject.transform.position, Quaternion.identity);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("1/3���� �ǰ�");
                    collider.GetComponent<MonsterManager>().TakeDamage(player.attackPower / 3);
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
    //������ ����
    private void CalDamage()
    {
        totalAttackPower += 0;
    }
    //��� ����
    private void TotalShield()
    {
        totalShield += 0;
    }
    public void SoundwaveOff()
    {
        soundwaveAttackOBJ.SetActive(false);
    }

}
