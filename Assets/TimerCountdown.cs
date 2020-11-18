using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 60;
    public bool timerStarted = false;
    
    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    void Update()
    {
        if (timerStarted == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        timerStarted = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
       
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        if (secondsLeft <= 0)
        {
            textDisplay.GetComponent<Text>().text = "Time's up!";
            
        }
        timerStarted = false;
    }
}
