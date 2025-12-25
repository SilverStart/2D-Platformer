using System.Collections;
using Platformer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour, IInteractable
{
    public enum PortalType { ToTown, ToHuntingGround }
    public PortalType type;

    [SerializeField] private FadeEvent fade;
    [SerializeField] private GameObject loadUI;
    [SerializeField] private GameObject portalEffect;

    [SerializeField] private Image progressBar;

    public bool IsInteracting { get; }

    public void Interact(Transform interactor = null)
    {
        StartCoroutine(SceneLoadRoutine());
    }

    IEnumerator SceneLoadRoutine()
    {
        fade.Fade(3, Color.white, true);
        portalEffect.SetActive(true);
        yield return new WaitForSeconds(3f);

        fade.Fade(3, Color.white, false);
        loadUI.SetActive(true);
        progressBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);

        progressBar.gameObject.SetActive(true);
        progressBar.fillAmount = 0;

        // // 일반 Scene 로드
        // float timer = 0;
        // while (true)
        // {
        //     timer += Time.deltaTime;
        //     progressBar.fillAmount = timer / 3f;
        //     yield return null;
        //
        //     if (progressBar.fillAmount >= 1f)
        //         break;
        // }
        //
        // if (type == PortalType.ToTown)
        //     SceneManager.LoadScene(1);
        // else if (type == PortalType.ToHuntingGround)
        //     SceneManager.LoadScene(2);

        
        // 비동기 Scene 로드
        AsyncOperation operation = null;
        if (type == PortalType.ToTown)
            operation = SceneManager.LoadSceneAsync(1);
        else if (type == PortalType.ToHuntingGround)
            operation = SceneManager.LoadSceneAsync(2);
        
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            progressBar.fillAmount = progress;
        
            if (operation.progress >= 0.9f)
            {
                progressBar.fillAmount = 1f;
        
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }
        
            yield return null;
        }
    }

    public void UnInteract() { }
}