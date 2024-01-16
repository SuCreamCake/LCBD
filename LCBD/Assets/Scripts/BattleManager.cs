using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameObject playerObject;
    Player player;
    //�� ��
    public int totalShield;
    private GameObject soundwaveAttackOBJ;

    //Damage of Weapon or SoundWaveAttack;
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
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void LateUpdate()
    {
        
    }


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
   
    private void TotalShield()
    {
        totalShield += 0;
    }
    public void SoundwaveOff()
    {
        soundwaveAttackOBJ.SetActive(false);
    }

    //getInfomationOfMonster
     public float GetCurrentInfo(Collider2D monster)
    {
        monsterManager = monster.GetComponent<MonsterManager>();
        monsterDefense = monsterManager.defense_Ms;
        monsterTenacity = monsterManager.tenacity_Ms;
        playerAttackPower = player.attackPower;
        float damage = CalDamage(playerAttackPower, monsterDefense, monsterTenacity);
        return damage;
        
    }

    public void HandleAttack()
    {

    }
}
