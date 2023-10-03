using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    Rigidbody2D rigid;
    public int nextMove;//���� �ൿ��ǥ�� ������ ����
    Animator animator;
    SpriteRenderer spriteRenderer;
    MonsterManager MonsterManager;

    bool searching = false;

    public float waitingtime = 5.0f;

    int monsterID; //�޾ƿ��� ���� id

    // Start is called before the first frame update
    private void Awake()
    {
        MonsterManager = GetComponent<MonsterManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5); // �ʱ�ȭ �Լ� �ȿ� �־ ����� �� ����(���� 1ȸ) nextMove������ �ʱ�ȭ �ǵ����� 

    }

    private void Start()
    {
        monsterID = MonsterManager.MonsterID;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!searching)
        {
            //Move
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y); //nextMove �� 0:���� -1:���� 1:������ ���� �̵� 

            //�ڽ��� �� ĭ �� ������ Ž���ؾ��ϹǷ� position.x + nextMove(-1,1,0�̹Ƿ� ������)
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

            //��ĭ �� �κоƷ� ������ ray�� ��
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

            //���̸� ���� ���� ������Ʈ�� Ž�� 
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("background"));

            //Ž���� ������Ʈ�� null : �� �տ� ������ ����
            if (raycast.collider == null)
            {
                Turn();
            }
        } else
        {
            // ���� ���ߵ��� ����
            rigid.velocity = Vector2.zero;
        }




    }


    public void Think()
    {
        float time = 3.0f;
        //���Ͱ� ������ �����ؼ� �Ǵ� (-1:�����̵� ,1:������ �̵� ,0:����  ���� 3���� �ൿ�� �Ǵ�)

        //Random.Range : �ּ�<= ���� <�ִ� /������ ���� ���� ����(�ִ�� �����̹Ƿ� �����ؾ���)
        nextMove = Random.Range(-1, 2);

        ////Sprite Animation
        ////WalkSpeed������ nextMove�� �ʱ�ȭ
        //animator.SetInteger("WalkSpeed", nextMove);


        //Flip Sprite
        if (nextMove != 0) //������ �� ���� ������ �ٲ� �ʿ䰡 ���� 
            spriteRenderer.flipX = nextMove == 1; //nextmove �� 1�̸� ������ �ݴ�� ����

        if(nextMove != 0)
        {
            time = 3.0f; //�����ϴ� �ð�
        } else
        {
            time = waitingtime; // ������ �̵��ӵ� �ɷ�ġ +2 + (2*�������� ��) / ������ �̵��ӵ� �ɷ�ġ
        }
        //Think(); : ����Լ� : �����̸� ���� ������ CPU����ȭ �ǹǷ� ����Լ��� ���� �׻� ���� ->Think()�� ���� ȣ���ϴ� ��� Invoke()���
        Invoke("Think", time); //�Ű������� ���� �Լ��� time���� �����̸� �ο��Ͽ� �����
        
        if(nextMove == 0 && monsterID >= 1000)
        {
            SearchPalyer();
        }
    }

    void Turn()
    {

        nextMove = nextMove * (-1); //�츮�� ���� ������ �ٲپ� �־����� Think�� ��� ���߾����
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke(); //think�� ��� ���� �� �����
        Invoke("Think", 2);//

    }

    public void StopThink()
    {
        CancelInvoke();
    }

    public void SearchPalyer()
    {
        searching = true;
        CancelInvoke(); //think�� ��� ���� �� �����

        // 2�ʸ��� MyFunction �޼��带 ȣ���մϴ�.
        InvokeRepeating("Serch", 0.0f, 2.0f);
        Invoke("StopSerch", waitingtime);
    }

    void Serch()
    {
        Debug.Log(monsterID);
        if (spriteRenderer.flipX)
            nextMove = 1;
            else
            nextMove = -1;
        nextMove = nextMove * (-1); //�츮�� ���� ������ �ٲپ� �־����� Think�� ��� ���߾����
        spriteRenderer.flipX = nextMove == 1;
    }
    
    void StopSerch()
    {
        CancelInvoke("Serch");
        Invoke("Think", 3);
        searching = false;
    }


}