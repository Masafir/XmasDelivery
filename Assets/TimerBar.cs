using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 
 public class TimerBar : MonoBehaviour
 {
     public static bool allowInputs;
 
     public Slider timerBar;
     private bool countDown = true;
 
     public float countDownTime = -60;
     public float refillTime = 1;
 
     private void Start()
     {
         timerBar.maxValue = refillTime;
     }
 
     private void Update()
     {
         if (timerBar.maxValue != refillTime)
             timerBar.maxValue = refillTime;
 
             if (countDown) 
             timerBar.value -= Time.deltaTime / countDownTime * refillTime;
         else
             timerBar.value += Time.deltaTime;
 
      
         if (timerBar.value <= 0)
         {
             countDown = false;
             allowInputs = false;
         }
         else if (timerBar.value >= refillTime)
         {
             countDown = true;
             allowInputs = true;
         }
     }
 }