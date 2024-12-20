using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ui_Manager : MonoBehaviour
{
    [Header("References")]
    public Text questionText;
    public Text correctCountText;
    public Text incorrectCountText;
    public Text roundText;
    public Text averageTimeText;
    public GameObject gameFinishPanel;
    public Animator anim;
    public Text[] optionArray = new Text[4];
    public Animator[] optionAnim = new Animator[4];
    public GameObject UI_Panel;

    [Header("Setttings Panel References")]
    public Animator animator;
    public GameObject setting;

    private string operatorString;
    private int randomOperator;
    private int numForMultiply;
    private int randomValueForMulitply;
    private int correctAnswer;
    private int correctCount = 0;
    private int incorrectCount = 0;
    private int currentQuestion = 1;
    private int totalQuestions = 10;
    private int round = 1;
    private float questionStartTime;
    private float questionEndTime;
    private List<float> questionTimes = new List<float>();
    void Start()
    {
        UI_Panel.SetActive(true);
        gameFinishPanel.SetActive(false);
        GenerateQuestionAndOptions();
        RoundSystem();
    }

    void GenerateQuestionAndOptions()
    {
        if (currentQuestion <= totalQuestions)
        {
            operatorString = string.Empty;
            anim.SetBool("BannerIncoming", true);

            numForMultiply = Random.Range(1, 20);
            randomValueForMulitply = Random.Range(1, 9);
            randomOperator = Random.Range(1, 4);
            if (randomOperator == 1)
            {
                correctAnswer = numForMultiply * randomValueForMulitply;
                operatorString += "  X  ";
            }
            else if (randomOperator == 2)
            {
                correctAnswer = numForMultiply - randomValueForMulitply;
                operatorString += "  -  ";
            }
            else if(randomOperator == 3) 
            {
                correctAnswer = numForMultiply + randomValueForMulitply;
                operatorString = "  +  ";
            }
            questionText.text = numForMultiply + operatorString + randomValueForMulitply + "  =  ?";
            //questionStartTime = Time.time;
            List<int> options = new List<int> { correctAnswer };
            Debug.Log(correctAnswer);

            int wrongValues;
            for (int i = 1; i <= 3; i++)
            {
                do
                {
                    wrongValues = Random.Range(correctAnswer - 10, correctAnswer + 10);
                } while (options.Contains(wrongValues));
                options.Add(wrongValues);
            }


            options = ShuffleList(options);


            for (int i = 0; i < optionArray.Length; i++)
            {
                optionArray[i].text = options[i].ToString();
                optionArray[i].GetComponentInParent<Button>().onClick.RemoveAllListeners();
                int optionIndex = i;
                optionArray[i].GetComponentInParent<Button>().onClick.AddListener(() => OnOptionSelected(optionArray[optionIndex], options[optionIndex]));
                optionAnim[i].SetTrigger("OptionIncoming");
            }
            StartCoroutine(BannerIncomingFalse());
        }
        else
        {
            UI_Panel.SetActive(false);
            ShowResults();

        }
    }

    void OnOptionSelected(Text selectedOptionText, int selectedOption)
    {
        //questionEndTime = Time.time;
        //float timeTaken = (questionEndTime - questionStartTime);
        //questionTimes.Add(timeTaken);

        if (selectedOption == correctAnswer)
        {
            correctCount++;
            correctCountText.text = "Correct - " + correctCount;
        }
        else
        {
            incorrectCount++;
            incorrectCountText.text = "Incorrect - " + incorrectCount;           
        }
        currentQuestion++;
        RoundSystem();
        GenerateQuestionAndOptions();
    }

    void ShowResults()
    {
        //averageTimeText.text = "";
        //for (int i = 0; i < questionTimes.Count; i++)
        //{
        //    float roundedTime = Mathf.Round(questionTimes[i] * 10f) / 10f;
        //    averageTimeText.text += "Time taken in question " + (i + 1) + " is " + roundedTime.ToString("F1") + "\n";
        //}
        gameFinishPanel.SetActive(true);
    }

    void RoundSystem()
    {
        if (round <= 10)
        {
            roundText.text = "Round - " + round + " / 10";
            round++;
        }
    }

    IEnumerator BannerIncomingFalse()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("BannerIncoming", false);
    }

    List<int> ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }


    public void SettingIncoming()
    {
        setting.SetActive(true);
        animator.SetBool("SettingIncoming", true);
        StartCoroutine(SettingIncomingIEnumerator());
    }

    public void SettingOutcoming()
    {
        animator.SetBool("SettingOutcoming", true);
        StartCoroutine(SettingOutcomingIEnumerator());
    }

    IEnumerator SettingIncomingIEnumerator()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("SettingIncoming", false);
    }

    IEnumerator SettingOutcomingIEnumerator()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("SettingOutcoming", false);
        setting.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void AppicationQuit()
    {
        Application.Quit();
    }

}

