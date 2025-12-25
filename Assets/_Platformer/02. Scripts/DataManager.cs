using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField idInput;
        [SerializeField] private TMP_InputField passwordInput;

        [SerializeField] private Button createButton;
        [SerializeField] private Button loginButton;

        void Start()
        {
            createButton.onClick.AddListener(CreateAccount);
            loginButton.onClick.AddListener(Login);
        }
        
        private void CreateAccount() // 계정 생성
        {
            if (idInput.text != String.Empty && passwordInput.text != String.Empty)
            {
                PlayerPrefs.SetString("ID", idInput.text);
                PlayerPrefs.SetString("Password", passwordInput.text);
                PlayerPrefs.Save();
                
                ClearText("계정이 생성되었습니다.", "white");
            }
            else
                Debug.Log("<color=yellow>아이디 또는 비밀번호를 입력하세요</color>");
        }

        private void Login()
        {
            if (idInput.text != String.Empty && passwordInput.text != String.Empty)
            {
                if (idInput.text == PlayerPrefs.GetString("ID"))
                {
                    if (passwordInput.text == PlayerPrefs.GetString("Password"))
                    {
                        Debug.Log("<color=green>로그인 되었습니다.</color>");
                        
                        Invoke(nameof(DelayEvent), 1f);
                    }
                    else // 비밀번호가 다를 때
                        ClearText("비밀번호를 잘못 입력하였습니다.", "red");
                }
                else // 아이디가 다를 때
                    ClearText("아이디를 잘못 입력하였습니다.", "red");
            }
        }

        private void ClearText(string msg, string color)
        {
            idInput.text = String.Empty;
            passwordInput.text = String.Empty;
            Debug.Log($"<color={color}>{msg}</color>");
        }

        private void DelayEvent()
        {
            SceneManager.LoadScene(1);
        }
    }
}