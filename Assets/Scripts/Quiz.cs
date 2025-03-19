using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
public class Quiz : MonoBehaviour
{   
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion; 

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons; 
    int correctAnswerIndex; 
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    float fillAmount;
    void Awake()
    {   
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {   
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion) // 프레임마다 새질문 받아오지 않음
        {   
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion) // 답을 일찍 선택하지 않았음 그리고 질문에 답변중이지 않을때
        {
            //정답 표시 
            DisplayAnswer(-1); // 정답과는 항상 다른 답을 index로 넣기
            SetButtonState(false); // 버튼 비활성화
        }
    }

    public void OnAnswerSelected(int index) //버튼에 onclick 
    {   
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score : "+ scoreKeeper.CalculateScore() + "%";


    }

    void DisplayAnswer(int index) // 정답 출력 화면
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorretAnswerIndex()) // 정답 
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else 
        {
            questionText.text = "Sorry, the corret answer was\n" + currentQuestion.GetAnswer(currentQuestion.GetCorretAnswerIndex());
            buttonImage = answerButtons[currentQuestion.GetCorretAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion() 
    {   
        if(questions.Count > 0)
        {
            SetButtonState(true); // 버튼 클릭 가능하게 활성화
            SetDefaultButtonSprites(); // 파란색 버튼 
            GetRandomQuestion();
            DisplayQuestion(); //
            progressBar.value++;
            scoreKeeper.IncrementquestionsSeen();
        }
    }
    void SetButtonState(bool state) 
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state; // 버튼 활성화 및 비활성화
        }
    }
    void SetDefaultButtonSprites() 
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>(); //버튼의 이미지 컴포넌트 가져오기
            buttonImage.sprite = defaultAnswerSprite; //기본 이미지 적용
        }
    }

    void DisplayQuestion() // 퀴즈 질문 텍스트 출력
    {
        questionText.text = currentQuestion.GetQuestion();
        for(int i=0; i<answerButtons.Length; i++) 
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>(); // 첫번째 textmeshpro 컴포넌트를 찾음
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void GetRandomQuestion()
    {   
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }



}
