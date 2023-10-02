using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ�� Transform�� ������ ����
    public float moveSpeed = 1f;
    private float raycastDistance = 0.5f;
    MonsterManager MonsterManager;
    SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    float k;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        MonsterManager = GetComponent<MonsterManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        k = rb.velocity.y;
    }

    private void Update()
    {
        //�ڽ��� �� ĭ �� ������ Ž��
        Vector2 frontVec = new Vector2(rb.position.x + 0.4f, rb.position.y);
        //�ڽ��� �� ĭ �� ������ Ž��
        Vector2 frontVec2 = new Vector2(rb.position.x - 0.4f, rb.position.y);

        // �÷��̾�� ������ ��ġ�� ���Ͽ� �̵� ������ ����
        float horizontalInput = 0f;
        if (player != null)
        {
            if (player.position.x < transform.position.x)
            {
                horizontalInput = -1f;
                spriteRenderer.flipX = false;
            }
            else if (player.position.x > transform.position.x)
            {
                horizontalInput = 1f;
                spriteRenderer.flipX = true;
            }
                
        }
        // ���Ͱ� ����ĳ��Ʈ�� �׸����� ����׷� ǥ��
        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

        // �̵� ���⿡ ���� ���� �̵�
        Vector2 moveDirection = new Vector2(horizontalInput, k);

        // x���� �Ÿ��� ���
        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);

        // x���� �Ÿ��� �������� ������ ���ߴ� ���� �߰� (��: x�� �Ÿ��� ���� �� �̳��� ��)
        float stopDistanceX = 0.2f; // x�� �Ÿ��� �������� ������ ���ߴ� �Ÿ� �Ӱ谪 ����
        if (distanceToPlayerX < stopDistanceX)
        {
            // ���Ͱ� �÷��̾�� x�� �Ÿ��� ���� �� �̳��� ���� �� ���� ���߱�
            rb.velocity = Vector2.zero;
        }else {
            Debug.Log(moveDirection);
            rb.velocity = moveDirection * moveSpeed;
        }




        //��ĭ �� �κоƷ� ������ ray�� ��
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        //��ĭ �� �κоƷ� ������ ray�� ��
        Debug.DrawRay(frontVec2, Vector3.down, new Color(0, 1, 0));

       if (spriteRenderer.flipX)
        {
            //���̸� ���� ���� ������Ʈ�� Ž�� 
            RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("background"));
            //Ž���� ������Ʈ�� null : �� �տ� ������ ����
            if (raycast.collider == null)
            {
                // �̵� ������ 0.1���� ���� ��� ���� ���ߵ��� ����
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            //���̸� ���� ���� ������Ʈ�� Ž�� 
            RaycastHit2D raycast2 = Physics2D.Raycast(frontVec2, Vector3.down, 1, LayerMask.GetMask("background"));
                        //Ž���� ������Ʈ�� null : �� �տ� ������ ����
        if (raycast2.collider == null)
            {
                // �̵� ������ 0.1���� ���� ��� ���� ���ߵ��� ����
                rb.velocity = Vector2.zero;
            }
        }
    }
}

