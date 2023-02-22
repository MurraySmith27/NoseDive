using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalDetector : MonoBehaviour
{
    public TextMeshProUGUI GoalText;
    public GameObject TimerText;
    public GameObject GoalScreen;
    public UIOnClick uiOnClick;
    public GameObject GameOverCanvas;

    public void OnCollisionEnter()
    {
        GoalScreen.SetActive(true);
        GoalText.text = "You Win! Time: " + uiOnClick.timerVal.ToString("n2");
        TimerText.SetActive(false);
        GameOverCanvas.SetActive(false);
    }
}
