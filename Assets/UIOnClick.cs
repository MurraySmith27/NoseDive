using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIOnClick : MonoBehaviour
{
    public float timerVal;
    public TextMeshProUGUI timerText;

    public GameObject timerTextCanvas;

    public bool done = false;

    public void OnTryAgainClick()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        timerVal = 0;
        done = true;
        timerTextCanvas.SetActive(true);
    }

    public void FixedUpdate()
    {
        if (!done)
        {
            timerVal += Time.deltaTime;
            timerText.text = timerVal.ToString("n2");
        }
    }
}
