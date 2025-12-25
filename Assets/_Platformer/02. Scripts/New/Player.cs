using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.New;
using UnityEngine;

public class Player : MonoBehaviour, IMoveable, IJumpable, IDashable, IGrappleable
{
    [SerializeField] private MoveController moveController;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float jumpForce = 20f;

    [Header("Trail Settings")] [SerializeField]
    private GameObject ghostPrefab; // 단계 1에서 만든 프리팹 연결

    [SerializeField] private float trailSpeedThreshold = 10f; // 이 속도보다 빠르면 잔상 생성
    [SerializeField] private float ghostSpawnInterval = 0.05f; // 잔상 생성 간격 (짧을수록 촘촘함)
    [SerializeField] private float ghostFadeSpeed = 4f; // 잔상이 사라지는 속도
    [SerializeField] private Color trailColor = new Color(1f, 1f, 1f, 0.7f); // 잔상 색상 (약간 반투명)

    private float lastGhostSpawnTime; // 마지막 잔상 생성 시간 기록용

    private SpriteRenderer mySpriteRenderer; // 내 현재 스프라이트를 가져오기 위함

    // 간단한 오브젝트 풀 (비활성화된 잔상들을 보관하는 큐)
    private readonly Queue<GhostFade> ghostPool = new();

    private bool isDashing;

    public bool IsDashing => isDashing;

    private bool canDash = true;

    private Rigidbody2D rb;

    private LineRenderer lineRenderer; // 줄을 그릴 렌더러
    private DistanceJoint2D distanceJoint; // 물리 관절 컴포넌트

    [Header("Settings")] public LayerMask grappleLayer; // 갈고리가 박힐 수 있는 레이어 (벽/천장)
    public float maxDistance = 20f; // 갈고리 최대 사거리

    // 갈고리가 박힌 위치를 저장할 변수
    private Vector2 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        moveController.SetMoveable(this);
        moveController.SetJumpable(this);
        moveController.SetDashable(this);
        moveController.SetGrappleable(this);

        GrowPool(10);

        StartCoroutine(SpawnTrailMonitorRoutine());
    }

    private void GrowPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject ghostObj = Instantiate(ghostPrefab);
            ghostObj.SetActive(false);
            ghostPool.Enqueue(ghostObj.GetComponent<GhostFade>());
        }
    }

    private IEnumerator SpawnTrailMonitorRoutine()
    {
        // 게임 내내 무한 반복
        while (true)
        {
            // 1. 현재 속도(magnitude: 벡터의 길이/총 속력)가 임계값보다 높은지 확인
            // 만약 Y축(낙하) 속도는 무시하고 싶다면 Mathf.Abs(rb.linearVelocity.x) > trailSpeedThreshold 사용
            bool isFastEnough = moveSpeed < Mathf.Abs(rb.linearVelocity.x);

            // 2. 속도가 충분히 빠르고 && 마지막 생성 후 일정 시간이 지났다면
            if (isFastEnough && lastGhostSpawnTime + ghostSpawnInterval <= Time.time)
            {
                SpawnGhost();
                lastGhostSpawnTime = Time.time; // 시간 기록 갱신
            }

            // 다음 프레임까지 대기 (매 프레임 속도를 감시하기 위함)
            yield return null;
        }
    }

    // 잔상 하나를 풀에서 꺼내 초기화하는 함수 (코드 분리)
    private void SpawnGhost()
    {
        GhostFade ghostScript = GetGhostFromPool();
        ghostScript.Init(
            mySpriteRenderer.sprite,
            transform.position,
            transform.rotation,
            transform.localScale,
            trailColor,
            ghostFadeSpeed
        );
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private GhostFade GetGhostFromPool()
    {
        if (ghostPool.Count == 0)
        {
            GrowPool(5); // 풀이 비었으면 5개 더 만듦
        }

        GhostFade ghost = ghostPool.Dequeue();
        ghostPool.Enqueue(ghost);

        return ghost;
    }

    private void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        if (!distanceJoint.enabled) return;
        lineRenderer.SetPosition(0, transform.position); // 줄의 시작점 (플레이어)
        lineRenderer.SetPosition(1, targetPos); // 줄의 끝점 (벽)   
    }


    public void Move(float h, float v)
    {
        float targetVelocityX = h * moveSpeed;
        float currentVelocityX = MathF.Abs(rb.linearVelocity.x);
        if (MathF.Abs(targetVelocityX) < currentVelocityX) return;
        rb.linearVelocity = new Vector2(targetVelocityX, rb.linearVelocity.y);
        // transform.Translate(new Vector3(h * moveSpeed * Time.deltaTime, 0, 0));
    }

    public void Jump()
    {
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    public void Dash(float h, float v)
    {
        if (canDash) StartCoroutine(DashRoutine(h, v));
    }

    private IEnumerator DashRoutine(float h, float v)
    {
        canDash = false;
        isDashing = true;

        // 대시 시작: 기존 중력 영향 제거 및 속도 고정
        float originalGravity = rb.gravityScale;
        // rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(h, v) * dashForce;

        yield return new WaitForSeconds(dashDuration);

        // 대시 종료: 상태 복구
        rb.gravityScale = originalGravity;
        isDashing = false;

        // 쿨타임 대기
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void ShootGrapple(Vector2 grapplePosition)
    {
        // 플레이어 위치에서 마우스 방향 계산
        Vector2 direction = (grapplePosition - (Vector2)transform.position).normalized;

        // 레이캐스트 발사 (플레이어 위치 -> 마우스 방향, 최대 거리, 충돌 레이어)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleLayer);

        // 무언가에 맞았다면 (hit.collider가 null이 아님)
        if (hit.collider != null)
        {
            IsGrappling = true;
            // 1. 박힌 위치 저장
            targetPos = hit.point;

            // 2. 조인트 설정
            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = targetPos; // 조인트의 고정점을 벽에 박힌 위치로 설정

            // 3. 거리 유지 설정
            // 현재 플레이어와 박힌 지점 사이의 거리를 유지하도록 설정
            distanceJoint.distance = Vector2.Distance(transform.position, targetPos);

            // 4. 라인 렌더러 활성화
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2; // 점 2개 (시작, 끝)
        }
    }

    public void CutGrapple()
    {
        IsGrappling = false;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    public bool IsGrappling { get; private set; }
}