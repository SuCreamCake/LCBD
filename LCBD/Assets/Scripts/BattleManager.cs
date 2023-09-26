using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject playerObject;
    Player player;
    //���� ����
    //���ݼӵ��� üũ�ϱ� ���� ����
    public float attackTime = 0;
    //���Ÿ� ���� ������Ʈ
    public GameObject bulletObject;
    //���� ��ü�� ��� �ڷ���
    public GameObject[] weapons;
    //�Ǽ� ����, ��������, ���Ÿ� ���� �ε���
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    //���� �ε���
    public int weaponeIndex = -1;
    //������ġ
    public Vector3 attackPosition;

    GameObject equipWeapon;
    //���� ������Ʈ
    public GameObject shieldObject;
    //���� ���̺� ����
    public GameObject soundWaveAttackObject;
    //�� ���̾� ����ũ
    public LayerMask enemyLayers;
    //���� ���� �ð�
    public float soundWaveAttackTime = 0;
    //�� ���ݷ�
    public int totalAttackPower;
    //�� ��
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
            attackTime = 0;
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            //���ݹ���
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;

            //���� ����
            float xRange = player.crossroads * 0.3f;
            float yRange = 0.5f;
            Vector2 boxSize = new Vector2(xRange, yRange);

            float angle = Mathf.Atan2(attackForce.y, attackForce.x) * Mathf.Rad2Deg;
            //���� �ݶ��̴� ����
            Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPosition, boxSize, angle, enemyLayers);
            Debug.Log(angle);
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "monster")
                {
                    Debug.Log("���� ����");
                    collider.GetComponent<EnemyHit>().TakeDamage(totalAttackPower);
                }
            }
            Debug.Log("���ݽ���");
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(attackPosition, new Vector2(player.crossroads * 0.3f, 0.7f));
    }


    //���Ÿ����� �޼���
    private void longDistanceAttack()
    {
        if (attackTime > player.attackSpeed && Input.GetMouseButtonDown(0))
        {
            attackTime = 0;
            //���콺�� ��ġ ��������
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

    //�������� Ű �Է�
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
            Debug.Log("F����");
        }
    }
    //���� Ÿ�� �ε���
    private void swapWeapon()
    {
        if (sDown1)
        {
            weaponeIndex = 0;
            Debug.Log("��ư 1Ȱ��ȭ");
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

    //��� ��� �޼���
    private void shield()
    {
        if (Input.GetMouseButtonDown(0))
        {
            equipWeapon.SetActive(true);
            //���콺�� ��ġ ��������
            Vector2 mousePoint = Input.mousePosition;
            mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
            Vector3 playerPos = transform.position;
            Vector2 direVec = mousePoint - (Vector2)playerPos;
            direVec = direVec.normalized;
            equipWeapon.transform.position = direVec + (Vector2)transform.position;

            //������ ���
            if (Vector3.Dot(transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("���� ���� ����");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //������ ���
            else if (Vector3.Dot(transform.up, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("�������� ����");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }
            else if (Vector3.Dot(-transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
            {
                Debug.Log("���� ���� ����");
                equipWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);


            }
            else
            {
                Debug.Log("�ϴ� ���� ����");
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

            //���ݹ���
            Vector2 attackForce = mousePoint - (Vector2)transform.position;
            attackForce = attackForce.normalized;
            Debug.Log(player.attackPower);
            float startAngle = -player.attackPower / 2;

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
                    Debug.Log("1/3���� �ǰ�");
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
    //������ ����
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
