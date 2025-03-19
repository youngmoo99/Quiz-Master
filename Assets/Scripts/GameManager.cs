using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true); //유니티 인스펙터에서 오브젝트 이름 옆의 체크박스를 키는것과 같음
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if( quiz.isComplete) 
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
