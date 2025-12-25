using System.Collections;
using TMPro;
using UnityEngine;

public class ClearScript : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private GameObject text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraFollow.UnlockCameraPosition();
            StartCoroutine(StartClearRoutine(other.gameObject));
        }
    }

    private IEnumerator StartClearRoutine(GameObject player)
    {
        Vector3 initScale = player.transform.localScale;
        float scaleTime = .5f;
        float currentTime = 0f;

        yield return new WaitForSeconds(2f);
        while (currentTime < scaleTime)
        {
            currentTime += Time.deltaTime;
            player.transform.localScale = initScale + (Vector3.one * (100f * (currentTime / scaleTime)));
            yield return null;
        }

        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        text.SetActive(true);
    }
}