using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{   
    [SerializeField] float timeToCompleteQuestion = 30f; //문제푸는 시간
    [SerializeField] float timeToShowCorrectAnswer = 10f; // 정답 보여주는 시간

    public bool loadNextQuestion = false;
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime; // 게임의 프레임마다 타이머 값이 조금씩 줄어듬
        if (isAnsweringQuestion)
        {
            if (timerValue > 0) 
            {
                fillFraction = timerValue / timeToCompleteQuestion; // 분수  0 ~ 1 까지 

            }
            else
            {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
        }
        else 
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer; // 분수  0 ~ 1 까지 
            }
            else
            {
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
        }
    }

    public void CancelTimer() // 타이머 강제종료
    {
        timerValue = 0;
    }
}
