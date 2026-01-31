using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 위치 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime; //이동 속도 동일화
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        //속도에 따라 애니메이션 상태 변화
        anim.SetFloat("Speed", inputVec.magnitude);

        //이동시 입력 키에 맞춰서 방향 변경
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
