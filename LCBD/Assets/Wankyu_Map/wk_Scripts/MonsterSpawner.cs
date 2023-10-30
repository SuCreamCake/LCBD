using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;  //스폰되는 몬스터
    private bool isWalk;    //스포너의 패턴으로 스폰되는 몬스터 패턴 지정 (배회/생존)  //true가 배회
    private int count;      //스폰되는 몬스터 수
    Transform trackingTarget;   //스폰되는 몬스터의 타겟(플레이어)

    //몬스터스포너 위치 설정
    public void SetPos(ObjectPoint point, int mapWidth, int mapHeight)
    {
        Vector3Int pos = new(point.FieldX * (mapWidth + 1) + point.MapX,
                          point.FieldY * (mapHeight + 1) + point.MapY);

        transform.position = pos;
        transform.Translate(0.5f, 0.5f, 0);
    }

    //몬스터스포너의 패턴(배회/생존) 설정 -> 스폰될 몬스터의 행동 패턴 설정
    public void SetPattern(bool isWalk)
    {
        this.isWalk = isWalk;
    }

    //몬스터스포너에서 스폰되는 몬스터의 마릿수 설정
    public void SetCount(int count)
    {
        this.count = count;
    }

    //몬스터스포너에서 스폰되는 몬스터의 종류 및 행동 패턴 설정
    public void SetMonsters(GameObject monster)
    {
        monsterPrefab = monster;

        if (isWalk == true)
        {
            //몬스터의 행동 패턴을 스포너에 맞게 지정//monsterPrefab.GetComponent<MonsterManager>().BehaviorPattern();
        }
    }

    //몬스터스포너에서 스폰되는 몬스터의 타겟 설정 (플레이어)
    public void SetTrackingTarget(Transform target)
    {
        trackingTarget = target;
    }

    //몬스터 스폰
    public void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject monster = Instantiate(monsterPrefab, transform.position, new(0, 0, 0, 0), transform);   //스포너를 부모로 몬스터 생성
            monster.GetComponent<PlayerTracking>().player = trackingTarget; //스폰된 몬스터에게 타겟(플레이어)을 넣어줌
        }
    }
}