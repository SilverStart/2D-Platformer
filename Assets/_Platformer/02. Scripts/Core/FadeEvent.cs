using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class FadeEvent : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;
        
        public void Fade(float fadeTime, Color fadeColor, bool isFade)
        {
            StartCoroutine(FadeRoutine(fadeTime, fadeColor, isFade));
        }
        
        public IEnumerator FadeRoutine(float fadeTime, Color fadeColor, bool isFade)
        {
            fadeImage.gameObject.SetActive(true);
            
            float timer = 0f;
            float percent = 0f;

            while (percent < 1f)
            {
                timer += Time.deltaTime;
                percent = timer / fadeTime;

                if (isFade)
                    fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, percent); // Fade : 화면이 어두워지는 것
                else
                    fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1 - percent); // UnFade : 화면이 밝아지는 것

                // // 삼항 연산자
                // float value = isFade ? percent : 1 - percent;
                // fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, value);
                
                yield return null;
            }
            
            if (!isFade)
                fadeImage.gameObject.SetActive(false);
        }
    }
}