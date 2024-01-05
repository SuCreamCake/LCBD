using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject playerObject, gunArm, hammer1, hammer2;
    Player player;
    public bool gunBool, hammer1bool, hammer2bool;

    //���� ����
    //���ݼӵ��� üũ�ϱ� ���� ����

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
   
    public LayerMask enemyLayers;
    //���� ���� �ð�
    public float soundWaveAttackTime = 0;
    //�� ���ݷ�
    public float totalAttackPower;
    //�� ��
    public int totalShield;
    private GameObject soundwaveAttackOBJ;
    public int weaponIndex = -1;

    public GameObject animationEffectMeeleAttack;

    public GameObject soundWakeAttack;
    public float attackTimeDelay;
    public float attackTime = 0;

    public bool iSmeleeAttackEnd = false;
    public bool iSmeleeAttack = false;
    public float setKey;

    //Damage of Weapon or SoundWaveAttack;
    public int weaponPower;
    public float addAttackSpeed = 0;
    SoundsPlayer SFXPlayer;
    public int playerAttackPower;

    //monster defensed
    public int monsterDefense;

    //monster tenacity
    public int monsterTenacity;
    //monsterManager
    MonsterManager monsterManager;


    // Start is called before the first frame update
    void Start()
    {
        //playerObject = GameObject.Find("Player");
        //SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<SoundsPlayer>();
        //player = playerObject.GetComponent<Player>();
        //attackPosition = playerObject.transform.position + new Vector3(0.2f, 0.2f, 0);
        //soundwaveAttackOBJ = GameObject.Find("SoundWaveOBJ");
        //attackTimeDelay = player.attackSpeed;
        //gunBool = true;
        //hammer1bool = true;

    }

    // Update is called once per frame
    void Update()
    {
        //soundWaveAttackTime += Time.deltaTime;
        //attackTimeDelay = player.attackSpeed + addAttackSpeed;
        //attackTimeDelay = 1f / attackTimeDelay;
        //attackTime += Time.deltaTime;
        //getInputBattleKeyKode();
        //battleLogic();
        //getInputSoundWaveAttack();
        ////공격방향
        //attackPosition = playerObject.transform.position + new Vector3(0.2f, 0.2f, 0);

        //iSmeleeAttackEnd = player.ani.GetCurrentAnimatorStateInfo(0).IsName("isMeleeAttack");
        //SetTrrigerMeleeAtack();
    }

    private void LateUpdate()
    {
        //if (player.stage == 2 && gunBool)
        //{
        //    gunArm = GameObject.Find("gunArm");
        //    gunArm.SetActive(false);
        //    gunBool = false;
        //}
        //if (player.stage == 2 && hammer1bool)
        //{
        //    hammer1 = GameObject.Find("hammer1");
        //    hammer1.SetActive(false);
        //    hammer1bool = false;
        //}

        //if (player.stage == 2 && weaponIndex == 0 &&
        //    player.ani.GetCurrentAnimatorStateInfo(0).IsName("childhoodStay"))
        //{
        //    hammer1.SetActive(true);
        //}

    }


    //����
    //�Ǽ� ����
    //public void punchAttack()
    //{
    //    if (attackTime > (attackTimeDelay / 2) && Input.GetMouseButtonDown(0))
    //    {
    //        attackTime = 0;
    //        //���콺�� ��ġ ��������
    //        Vector2 mousePoint = Input.mousePosition;
    //        mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
    //        //���� ĳ������ ��ġ ��������
    //        Vector2 characterPoint = new(transform.position.x, transform.position.y);
    //        //startX, startY��ǥ ���ϱ� ����, �Ÿ��� ����
    //        float rangeRadius = player.crossroads / 6.0f; //���� ������ 1/3 1/2 == 1/6
    //        float rangeRadian = Mathf.Atan2(mousePoint.y - characterPoint.y, mousePoint.x - characterPoint.x);
    //        //��������ĳ����  ������ (=�߽���)
    //        float startX = characterPoint.x + rangeRadius * Mathf.Cos(rangeRadian);
    //        float startY = characterPoint.y + rangeRadius * Mathf.Sin(rangeRadian);
    //        //��������ĳ����
    //        Vector2 startAttackPoint = new(startX, startY);
    //        //���� ������ ���̾ �߰��ϰ� �ش� ���̾ �����ϵ��� ���̾� �߰��ϰ� ���̾� ���� ���� ���� �ʿ� (���� ���̾ ���� ����) (�ϴ� �÷��̾� ������ ��� ���̾� ����)
    //        //int layerMask = 1 << LayerMask.NameToLayer("monster");
    //        ////layerMask = ~layerMask;
    //        //RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, layerMask);
    //        //if (raycastHit.collider != null)    //��� �����Ǹ�
    //        //{

    //        //}

    //        //Collider2D hitEnemys = Physics2D.OverlapCircle(startAttackPoint,rangeRadius,enemyLayers);

    //        RaycastHit2D raycastHit = Physics2D.CircleCast(startAttackPoint, rangeRadius, Vector2.right, 0f, enemyLayers);
    //        if (raycastHit.collider != null)
    //        {
    //            GetCurrentInfo(raycastHit.collider);
    //            raycastHit.collider.GetComponent<MonsterManager>().TakeDamage(CalDamage(playerAttackPower, monsterDefense, monsterTenacity));
    //            raycastHit.collider.GetComponent<MonsterManager>().TakeDamage(totalAttackPower);
    //        }

    //        //��ġ �����
    //        Debug.Log("mousePoint: " + mousePoint);
    //        Debug.Log("characterPoint: " + characterPoint);
    //        Debug.Log("rangeRadian: " + rangeRadian);
    //        Debug.Log("startAttackPoint: " + startAttackPoint);

    //        //����ĳ��Ʈ ���� �׸��� ����׿� ���� ����
    //        Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * player.crossroads, Color.white, 0.3f);      //ĳ���� ���� ~ ���� ��Ÿ�
    //        Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * player.crossroads / 3f, Color.green, 0.3f); //ĳ���� ���� ~ �Ǽ� ��Ÿ�
    //        Debug.DrawRay(characterPoint, new Vector2(rangeRadius * Mathf.Cos(rangeRadian), rangeRadius * Mathf.Sin(rangeRadian)).normalized * rangeRadius, Color.black, 0.3f);     //ĳ���� ���� ~ �� ���� �������� �Ÿ�
    //        Debug.DrawRay(startAttackPoint, Vector2.up * rangeRadius, Color.red, 0.3f);                     //���� �� ���� ����
    //        Debug.DrawRay(startAttackPoint, Vector2.down * rangeRadius, Color.red, 0.3f);                   //���� �� �Ʒ��� ����
    //        Debug.DrawRay(startAttackPoint, Vector2.right * rangeRadius, Color.red, 0.3f);                  //���� �� ������ ����
    //        Debug.DrawRay(startAttackPoint, Vector2.left * rangeRadius, Color.red, 0.3f);                   //���� �� ���� ����
    //        Debug.DrawRay(startAttackPoint, Vector2.one.normalized * rangeRadius, Color.red, 0.3f);         //���� �� ����� �밢�� ����
    //        Debug.DrawRay(startAttackPoint, new Vector2(1, -1).normalized * rangeRadius, Color.red, 0.3f);  //���� �� ������ �밢�� ����
    //        Debug.DrawRay(startAttackPoint, new Vector2(-1, 1).normalized * rangeRadius, Color.red, 0.3f);  //���� �� �»��� �밢�� ����
    //        Debug.DrawRay(startAttackPoint, -Vector2.one.normalized * rangeRadius, Color.red, 0.3f);        //���� �� ������ �밢�� ����
    //    }
    //}
    ////���� ����
    //private void meleeAttack()
    //{
        
    //    if (attackTime > attackTimeDelay && Input.GetMouseButtonDown(0))
    //    {
    //        player.enduranceOnOff = 0;
    //        player.endurance -= player.maxEndurance / 15;
    //        this.GetComponent<WeaponManager>().Set(WeaponManager.Weapon.hammer);
    //        addAttackSpeed = 0;
    //        attackTime = 0;
    //        //���콺�� ��ġ ��������
    //        //Vector2 mousePoint = Input.mousePosition;
    //        //mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
    //        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
    //            Input.mousePosition.y, -Camera.main.transform.position.z));

    //        //애니메이션
    //        player.ani.SetTrigger("isMeleeAttack");
    //        float key = mousePoint.x - playerObject.transform.position.x;
    //        if (key > 0)
    //        {
    //            key = 1;
    //            playerObject.transform.localScale = new Vector3(key * 1.5f, 1.5f, 0);
    //        }
    //        else if (key < 0)
    //        {
    //            key = -1;
    //            playerObject.transform.localScale = new Vector3(key * 1.5f, 1.5f, 0);
    //        }
            
    //        iSmeleeAttack = true;
    //        setKey = key;

    //        //���ݹ���
    //        Vector2 attackForce = mousePoint - (Vector3)playerObject.transform.position;
    //        attackForce = attackForce.normalized;

    //        //���� ����
    //        float xRange = player.crossroads * 0.3f;
    //        float yRange = 0.5f;
    //        Vector2 boxSize = new Vector2(xRange, yRange);

    //        float angle = 0;
    //        Vector3 attackPositionForMel = attackPosition;
    //        Debug.Log("키값" + key);
    //        //���� �ݶ��̴� ����
    //        if (key < 0)
    //        {
    //            attackPositionForMel = attackPositionForMel - 2 * (attackPosition - playerObject.transform.position);
    //            angle = 180f;
    //        }

    //        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPositionForMel, boxSize, angle, enemyLayers);

    //        foreach (Collider2D collider in colliders)
    //        {
    //            if (collider.tag == "monster")
    //            {
    //                //getInfoOfMonster
    //                GetCurrentInfo(collider);
    //                collider.GetComponent<MonsterManager>().TakeDamage(CalDamage(playerAttackPower,monsterDefense, monsterTenacity));
    //            }
    //        }
    //    }
    //}

    ////근접 공격
    //private void SetTrrigerMeleeAtack()
    //{
    //    float key = setKey;
    //    if (iSmeleeAttack && !iSmeleeAttackEnd)
    //    {
    //        SFXPlayer.AttackSound(2);
    //        if (key > 0)
    //        {
    //            Instantiate(animationEffectMeeleAttack, playerObject.transform.position + new Vector3(0.8f, 0, 0), new(0, 0, 0, 0));
    //            anim = animationEffectMeeleAttack.GetComponent<Animator>();
    //            anim.SetTrigger("isMeelAttackEffect");
    //        }
    //        else
    //        {
    //            Instantiate(animationEffectMeeleAttack, playerObject.transform.position + new Vector3(-0.8f, 0, 0), new(0, 0, 0, 0));
    //            anim = animationEffectMeeleAttack.GetComponent<Animator>();
    //            anim.SetTrigger("isMeelAttackEffect");
    //        }
    //        iSmeleeAttack = false;
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawCube(attackPosition, new Vector2(player.crossroads * 0.3f, 0.7f));
    //}


    ////원거리 공격
    //public void longDistanceAttack()
    //{
    //    if (attackTime > attackTimeDelay && Input.GetMouseButtonDown(0))
    //    {
    //        this.GetComponent<WeaponManager>().Set(WeaponManager.Weapon.candy);
    //        player.enduranceOnOff = 0;
    //        player.endurance -= weaponPower;
    //        SFXPlayer.AttackSound(0);
    //        attackTime = 0;
    //        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
    //        Input.mousePosition.y, -Camera.main.transform.position.z));

    //        player.ani.SetTrigger("isGunAttack");
    //        float key = mousePoint.x - playerObject.transform.position.x;
    //        if (key > 0)
    //        {
    //            key = 1;
    //            playerObject.transform.localScale = new Vector3(-key * 1.5f, 1.5f, 0);
    //        }
    //        else if (key < 0)
    //        {
    //            key = -1;
    //            playerObject.transform.localScale = new Vector3(-key * 1.5f, 1.5f, 0);
    //        }

    //        gunArm.SetActive(true);

    //        Vector3 playerPos = playerObject.transform.position;

    //        Vector2 direVec = mousePoint - (Vector3)playerPos;
    //        direVec = direVec.normalized;
    //        GameObject tempObeject = Instantiate(bulletObject);
    //        tempObeject.transform.position = playerObject.transform.position;
    //        tempObeject.transform.right = direVec;

    //    }
    //}

    //private void getInputBattleKeyKode()
    //{
    //    sDown1 = Input.GetKeyDown(KeyCode.F1);
    //    sDown2 = Input.GetKeyDown(KeyCode.F2);
    //    sDown3 = Input.GetKeyDown(KeyCode.F3);
    //    sDown4 = Input.GetKeyDown(KeyCode.F4);
    //    if(player.stage==2)
    //        swapWeapon();
    //}

    //private void getInputSoundWaveAttack()
    //{
    //    if (Input.GetKeyDown(KeyCode.F) && player.stage == 1)
    //    {
    //        soundWaveAttack();
    //    }
    //}
    ////���� Ÿ�� �ε���
    //private void swapWeapon()
    //{
    //    if (sDown1)
    //    {
    //        weaponIndex = 0;
    //        player.ani.SetTrigger("isChildhood");
    //    }
    //    if (sDown2)
    //    {
    //        weaponIndex = 1;
    //        player.ani.SetTrigger("isGun");
    //    }
    //    if (sDown3) weaponIndex = 2;
    //    if (sDown4)
    //    {
    //        weaponIndex = 3;
    //        player.ani.SetTrigger("isChildhood");
    //    }

    //    //if (sDown1 || sDown2 || sDown3 || sDown4)
    //    //{
    //    //    if (shieldObject != null)
    //    //        shieldObject.SetActive(false);
    //    //    shieldObject = weapons[weaponIndex];
    //    //    shieldObject.SetActive(true);
    //    //}
    //}
    //public void battleLogic()
    //{

    //    if (weaponIndex == 0)
    //        meleeAttack();
    //    else if (weaponIndex == 1)
    //        longDistanceAttack();
    //    else if (weaponIndex == 2)
    //        punchAttack();
    //    else if (weaponIndex == 3)
    //        shield();
    //}

    ////��� ��� �޼���
    //private void shield()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        shieldObject.SetActive(true);
    //        //���콺�� ��ġ ��������
    //        Vector2 mousePoint = Input.mousePosition;
    //        mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
    //        Vector3 playerPos = transform.position;
    //        Vector2 direVec = mousePoint - (Vector2)playerPos;
    //        direVec = direVec.normalized;
    //        shieldObject.transform.position = direVec + (Vector2)transform.position;

    //        //������ ���
    //        if (Vector3.Dot(transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
    //        {
    //            shieldObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    //        }
    //        //������ ���
    //        else if (Vector3.Dot(transform.up, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
    //        {
    //            shieldObject.transform.rotation = Quaternion.Euler(0, 0, 90f);
    //        }
    //        else if (Vector3.Dot(-transform.right, direVec) > Mathf.Cos(45f * Mathf.Deg2Rad))
    //        {
    //            shieldObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    //        }
    //        else
    //        {
    //            shieldObject.transform.rotation = Quaternion.Euler(0, 0, 90f);
    //        }
    //    }
    //    else if (Input.GetMouseButtonUp(0))
    //    {
    //        shieldObject.SetActive(false);
    //    }
    //}
    //private void soundWaveAttack()
    //{
    //    if (soundWaveAttackTime >= 3.0f)
    //    {
    //        player.ani.SetTrigger("isSkill");
    //        soundWaveAttackTime = 0;
    //        RaycastHit2D[] raycastHit2Ds;
    //        soundWaveAttackTime = 0;
    //        //���콺�� ��ġ ��������
    //        Vector2 mousePoint = Input.mousePosition;
    //        mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

    //        //���ݹ���
    //        Vector2 attackForce = mousePoint - (Vector2)transform.position;
    //        attackForce = attackForce.normalized;

    //        float startAngle = -player.attackPower / 2;

    //        Instantiate(soundWakeAttack, playerObject.transform.position, new(0, 0, 0, 0));
    //        anim = animationEffectMeeleAttack.GetComponent<Animator>();
    //        anim.SetTrigger("IsTrigger");
    //        //1/3���� ���� ���� ����
    //        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.crossroads / 3, enemyLayers);
    //        foreach (Collider2D collider in colliders)
    //        {
    //            if (collider.tag == "monster")
    //            {
    //                GetCurrentInfo(collider);
    //                collider.GetComponent<MonsterManager>().TakeDamage(CalDamage(playerAttackPower, monsterDefense, monsterTenacity));
    //            }
    //            string txt = "";
    //            if (player.health <= 0)
    //            {
    //                player.health = 0;
    //                txt = "Dead";
    //            }
    //            else
    //            {
    //                if (player.health > player.maxHealth)
    //                    player.health = player.maxHealth;
    //                txt = string.Format("{0}/{1}", player.health, player.maxHealth);
    //            }
    //        }
    //    }
    //}
   
    public float CalDamage(int atk, int defense, int tenacity)
    {
        float sum = 0;
        sum = atk - defense;
        if(tenacity < 0)
        {
            sum  = sum * 1.3f;
        }
        return sum;
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

    //getInfomationOfMonster
     public void GetCurrentInfo(Collider2D monster)
    {
        monsterManager = monster.GetComponent<MonsterManager>();
        monsterDefense = monsterManager.defense_Ms;
        monsterTenacity = monsterManager.tenacity_Ms;
        playerAttackPower = player.attackPower;
        
    }

    public void HandleAttack()
    {

    }

}
