using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; //스캔 범위
    public LayerMask targetLayer; //레이어
    public RaycastHit2D[] targets; //스캔 결과 배열
    public Transform nearestTarget; //가장 가까운 타겟

    void FixedUpdate()
    {        
        //1. 캐스팅 시작 위치, 2. 원의 반지름, 3. 캐스팅 방향, 4. 캐스팅 길이, 5. 대상 레이어
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
        //첫번째로 'CircleCastAll' 돌린 뒤 그 안에다가 'nearestTarget'도 검색함
    }

    Transform GetNearest()//가장 가까운 것 찾는 함수
    {
        Transform result = null;
        float diff = 100; //인식 거리 추가

        foreach (RaycastHit2D target in targets)//'targets'를 돌면서 'RaycastHit2D'를 하나씩 꺼낸 후 'target'으로
        {
            Vector3 myPos = transform.position;//내포지션
            Vector3 targetPos = target.transform.position;//타겟 포지션
            float curDiff = Vector3.Distance(myPos, targetPos);//내 포지션과 타겟 포지션 사이 거리 측정
            
            //가져온 거리가 저장된 거리보다 짧은 경우 교체
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
}
