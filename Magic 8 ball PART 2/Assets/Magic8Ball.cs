using UnityEngine;
using UnityEngine.UI;      
using TMPro;  
using System.Collections.Generic;             

public class Magic8Ball : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Text component that shows the answer")]
    public TextMeshProUGUI answerTMP;   
    public Text answerText;             

    [Header("Answer Pools")]
    [TextArea]
    public List<string> positiveAnswers = new List<string>()
    {
        "Definitely", "Yes", "Absolutely", "It is certain", "Count on it"
    };

    [TextArea]
    public List<string> negativeAnswers = new List<string>()
    {
        "MY SOURCES SAY NO, BUT THEY ALSO SAID HILLARY WOULD WIN",
        "IF COWS HAS WINGS THAN YES",
        "TRUMP USES ME WHEN DECIDING TO GO TO WAR",
        "YOU WOULD BE BETTER GOING TO MEDICAL COLLEGE",
        "Don't count on it.",
        "Very doubtful."


    };

    [Header("Optimism (0–100%)")]
    [Range(0, 100)]
    public int optimismPercent = 50; 

    [Header("Optional: Hook a Slider (0..1)")]
    public Slider optimismSlider; 

    System.Random rng;  
    void Awake() { rng = new System.Random(); }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowRandomAnswer();
        }

        
        if (optimismSlider)
        {
            optimismPercent = Mathf.RoundToInt(optimismSlider.value * 100f);
        }
    }

    public void ShowRandomAnswer()
    {
        
        int roll = rng.Next(0, 100);  
        bool choosePositive = roll < optimismPercent;

        string answer = choosePositive
            ? PickRandom(positiveAnswers)
            : PickRandom(negativeAnswers);

       
        if (answerTMP) answerTMP.text = answer;
        if (answerText) answerText.text = answer;
    }

    string PickRandom(List<string> list)
    {
        if (list == null || list.Count == 0) return "(no answers in list)";
        int i = rng.Next(0, list.Count);
        return list[i];
    }
}