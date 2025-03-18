using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")] //스크립터블 오브젝트를 사용하기 위해 속성 추가
public class QuestionSO : ScriptableObject // -->스크립터블 오브젝트 
{   
    [TextArea(2,6)] // 2줄 최소 6줄 최대
    [SerializeField] string question = "Which of the following is not a Hoyoverse game"; 
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex = 0;



    public string GetQuestion() 
    {


        return question;
    }
    public int GetCorretAnswerIndex()
    {   
        return correctAnswerIndex;
    }

    public string GetAnswer(int index) 
    {
        return answers[index];
    }

}

