using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float smoothSpeed = 5f;

    [SerializeField] private Vector2 minBounds, maxBounds;

    private bool isCameraLock = true;

    void LateUpdate()
    {
        if (target == null) return;

        float speed = isCameraLock ? smoothSpeed : smoothSpeed * 3;

        Vector3 destination = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);

        smoothPosition.x = isCameraLock ? Mathf.Clamp(smoothPosition.x, minBounds.x, maxBounds.x) : smoothPosition.x;
        smoothPosition.y = isCameraLock ? Mathf.Clamp(smoothPosition.y, minBounds.y, maxBounds.y) : smoothPosition.y;

        transform.position = smoothPosition;
    }

    public void UnlockCameraPosition()
    {
        isCameraLock = false;
    }
}