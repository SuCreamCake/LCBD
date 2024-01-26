using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCatAbility : MonoBehaviour
{
    //카피캣 모드
    public bool isOnBuff = false;
    public bool isMaintain = false;
    public bool isOnDelay = false;

    //지속 시간 버프, 복사된 공격 유지 시간
    public float buffTime = 5f;
    public float maintainTime = 10f;
    
    //딜레이타임
    public float delayTime = 10f;
    
    //지속 기간, 복사된 공격 유지 시간, 딜레이 시간
    public float duringBuffTime = 0f;
    public float duringMaintainTime = 0f;
    public float duringDelayTime = 0f;

    //피격 여부
    public bool isHit = false;

    //카피캣 파티클 시스템
    public ParticleSystem particleSystem;
    //
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //만약 딜레이 시간이 아니라면
        if(!isOnDelay){

            //만약 카피 캣이 켜졌다면
            if(isOnBuff){
                Debug.Log("isOnBuff" + isOnBuff);
                duringBuffTime += Time.deltaTime;
                bool isBuff;
                isBuff = duringBuffTime <= buffTime;

                //만약 피격이 되었을 경우
                if(isHit){
                    Debug.Log("버프 시간 지속중..");
                    //현재 공격복사 유지하기 시간
                    duringMaintainTime += Time.deltaTime;
                }
                //만약 버프 시간이 초과되었는데, 피격상태가 아닌경우 또는 복사된 공격 유지 시간이 다된 경우
                isMaintain = duringMaintainTime <= maintainTime;
                if((!isBuff && !isHit) || (!isMaintain)){
                     Debug.Log("버프 시간 끝남....");
                    isOnDelay = true;
                }
                
                //피격 되었고 제한 시간안에 스킬을 쓴다면
                //즉 피격되었고 F키 누를 시
                
                
            }   
        }
        //딜레이 중이라면
        else{
            duringDelayTime += Time.deltaTime;
            //만약 딜레이 시간이 오버된다면 다시 카피캣 가능하게 만들기
            bool isDelayOver = duringDelayTime >= delayTime;
            if(isDelayOver){
                //딜레이 타임, 지속시간 버프 , 복사된 공격시간 유지하기 초기화하기
                duringBuffTime = 0f;
                duringMaintainTime = 0f;
                duringDelayTime = 0f;
                //카피캣 모드 초기화
                isOnBuff = false;
                isMaintain = false;
                isOnDelay = false;
                isHit = false;
            }
        }
    }

    //맞았을 대 알리는 함수이다. 즉 피격상태를 알리는 함수
    //Buff시간내에서만 유효해야하 함
    public void AlertHit(Collision2D collider){
        Debug.Log("87번째 AlertHit함수");
        if(isOnBuff){
            isHit = true;
        }
        particleSystem = collider.gameObject.GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    //카피캣 쓰기
    public void UseCopy(bool isHit ,bool isDuring){
        //현재 피격 상태이고 제한시간안에 사용한다면
        if(isHit && isDuring){
            //복사하는 로직 가져오기
            if(particleSystem != null){
                //파티클 시스템 실행 후
                particleSystem.Play();
                //3초 뒤 멈추기
                Invoke("StopCopy",2f);
            }
        }
    }

    //효과를 멈추기 위한 함수
    public void StopCopy(){
        if(particleSystem != null){
            particleSystem.Stop();
        }
    }


    //카피캣 실행 여부에 따른 실행할 함수들 모음
    //다른 함수에서 F만 딸깍 누르면 여기서 처리하기 위한 함수
    public void HandleEvent(){
        //딜레이 상태가 아니고 버프상태도 꺼져있다면 카피캣 활성화하기
        if(!isOnDelay && ! isOnBuff){
            //버프 상태 활성화
            Debug.Log("버프가 활성화되었습니다.");
            isOnBuff = true;
        }
        //지속시간 동안 해당 키를 누르면 즉 피격상태일 때
        else if(isMaintain && isHit){
            Debug.Log("You Active CopyCat! Great!");
            UseCopy(isHit, isMaintain);
            isOnDelay = true;
        }
    }
}
