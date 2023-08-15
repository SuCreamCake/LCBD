using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //방어 장비 스택
    public int stackShield;
    //캐릭터 방어력
    public int characterDefense;
    public GameObject player;
    //인격
    public int personality;
    //상성
    public float compatibility;
    //총 방어력
    public int totalShield;
    //공격랑
    public int attack;
    // Start is called before the first frame update
    void Start()
    {
        characterDefense = player.GetComponent<Player>().defense;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        player.GetComponent<Player>().health -= (attack - totalShield);


    }


}
