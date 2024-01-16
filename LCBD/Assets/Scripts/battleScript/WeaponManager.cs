using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private GameObject battleManager;
    private int attackPower;
   public enum Weapon
    {
        
        hammer,
        pillow,
        candy,
        sand
    }

    private void Start()
    {
        battleManager = GameObject.Find("BattleManager");
      
    }

    public Weapon weapon;
    private void Update()
    {
        if(Weapon.hammer==weapon)
        {
            battleManager.GetComponent<Battle>().weaponPower = 5;
        }
        else if(Weapon.pillow==weapon)
        {
            battleManager.GetComponent<Battle>().weaponPower = 7;
        }
        else if(Weapon.candy == weapon)
        {
            battleManager.GetComponent<Battle>().weaponPower = 4;
            battleManager.GetComponent<Battle>().addAttackSpeed += 8f;
        }
        else if (Weapon.sand == weapon)
        {
            battleManager.GetComponent<Battle>().weaponPower = 3;
            battleManager.GetComponent<Battle>().addAttackSpeed += 6f;
        }

    }
    public void Set(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
