using System.Collections;
using UnityEngine;

public class GhostFade : MonoBehaviour
{
    private SpriteRenderer sr;
    private float fadeSpeed;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // 외부에서 이 함수를 호출해서 잔상을 초기화합니다.
    public void Init(Sprite sprite, Vector3 position, Quaternion rotation, Vector3 scale, Color color, float speed)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
        sr.sprite = sprite;
        sr.color = color; // 시작 색상(약간 투명하게 시작할 수도 있음)
        fadeSpeed = speed;

        gameObject.SetActive(true);
        StopAllCoroutines(); // 혹시 실행 중이던 코루틴이 있다면 정지
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        // 알파값이 0보다 큰 동안 계속 줄입니다.
        while (sr.color.a > 0f)
        {
            Color currentColor = sr.color;
            // fadeSpeed 만큼 알파값을 뺍니다. (Time.deltaTime을 곱해 프레임 독립적으로)
            currentColor.a -= fadeSpeed * Time.deltaTime;
            sr.color = currentColor;
            yield return null; // 다음 프레임까지 대기
        }

        // 완전히 투명해지면 비활성화하여 풀(Pool)로 돌려보냅니다.
        gameObject.SetActive(false);
    }
}
