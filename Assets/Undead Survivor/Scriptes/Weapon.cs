using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;//타이머
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();//GetComponentInParent: 부모 컴포넌트 가져오기
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;//'deltaTime' 반복해서 더해줌

                if (timer > speed)
                {
                    timer = 0f;//'speed'보다 커질 시 초기화하면서 발사
                    Fire();
                }
                break;
        }
        // Test Code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
    }


    public void Init()
    {
        switch (id) {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                speed = 0.3f;//연사속도
                break;
        }
    }

    void Batch()
    {
        for (int index=0; index < count; index++) {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    
    void Fire()//발사 함수
    {
        if (!player.scanner.nearestTarget)//바로 스캐너로 접근해서 위치 파악 불가하므로 플레이어 스크립트에 추가해서 호출
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;//적의 위치
        Vector3 dir = targetPos - transform.position; //적의 방향
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//총알 회전
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
