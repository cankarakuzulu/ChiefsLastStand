using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace nopact.ChefsLastStand
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        private float elapsedTime = 0f;
        private bool isTimerRunning = true;

        private void Update()
        {
            if (isTimerRunning)
            {
                elapsedTime += Time.deltaTime;

                int minutes = (int)elapsedTime / 60;
                int seconds = (int)elapsedTime % 60;

                timerText.text = $"{minutes:00}:{seconds:00}";
            }
        }

        public void PauseTimer()
        {
            isTimerRunning = false;
        }

        public void ResumeTimer()
        {
            isTimerRunning = true;
        }

        public float GetElapsedTime()
        {
            return elapsedTime;
        }
    }
}
