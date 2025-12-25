using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource[] bgmAudios;
        [SerializeField] private AudioSource[] eventAudios;
        [SerializeField] private AudioClip[] clips;

        // void Awake()
        // {
        //     bgmVolume.onValueChanged.AddListener(BgmVolume);
        //     eventVolume.onValueChanged.AddListener(EventVolume);
        //
        //     bgmMute.onValueChanged.AddListener(BgmMute);
        //     eventMute.onValueChanged.AddListener(EventMute);
        // }
        //
        // void Start()
        // {
        //     foreach (var audio in bgmAudios)
        //     {
        //         bgmVolume.value = audio.volume;
        //         bgmMute.isOn = audio.mute;
        //     }
        //
        //     foreach (var audio in eventAudios)
        //     {
        //         eventVolume.value = audio.volume;
        //         eventMute.isOn = audio.mute;
        //     }
        // }

        public void SoundPlay(string clipName) // 배경음악 용도
        {
            foreach (var audio in bgmAudios)
            {
                if (!audio.isPlaying)
                {
                    foreach (var clip in clips)
                    {
                        if (clip.name.Equals(clipName))
                        {
                            audio.clip = clip;
                            audio.loop = true;
                            audio.Play();
                            return;
                        }
                    }

                    Debug.Log($"{clipName} 파일을 찾을 수 없습니다.");
                    return;
                }
            }

            Debug.Log($"재생 가능한 AudioSource가 없습니다.");
        }

        public void SoundOneShot(string clipName) // 이벤트 사운드 용도
        {
            foreach (var audio in eventAudios)
            {
                if (!audio.isPlaying)
                {
                    foreach (var clip in clips)
                    {
                        if (clip.name.Equals(clipName))
                        {
                            audio.PlayOneShot(clip);
                            return;
                        }
                    }

                    Debug.Log($"{clipName} 파일을 찾을 수 없습니다.");
                    return;
                }
            }

            Debug.Log($"재생 가능한 AudioSource가 없습니다.");
        }
        
        private void BgmVolume(float volume)
        {
            foreach (var audio in bgmAudios)
                audio.volume = volume;
        }

        private void EventVolume(float volume)
        {
            foreach (var audio in eventAudios)
                audio.volume = volume;
        }

        private void BgmMute(bool isMute)
        {
            foreach (var audio in bgmAudios)
                audio.mute = isMute;
        }

        private void EventMute(bool isMute)
        {
            foreach (var audio in eventAudios)
                audio.mute = isMute;
        }
    }
}