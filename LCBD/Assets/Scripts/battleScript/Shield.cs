using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //��� ��� ����
    public int stackShield;
    //ĳ���� ����
    public int characterDefense;
    public GameObject player;
    //�ΰ�
    public int personality;
    //��
    public float compatibility;
    //�� ����
    public int totalShield;
    //���ݶ�
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
