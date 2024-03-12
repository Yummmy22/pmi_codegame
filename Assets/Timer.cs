using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float initialTime;
    private float remainingTime;
    private bool isTimerActive = false;

    void Update()
    {
        // Cek apakah timer aktif sebelum melakukan update
        if (isTimerActive)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                remainingTime = 0;
                // GameOver();
                timerText.color = Color.red;
                // Matikan timer setelah mencapai 0 (opsional)
                isTimerActive = false;
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // Cek apakah tombol "Enter" ditekan
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Aktifkan timer jika belum aktif
            if (!isTimerActive)
            {
                remainingTime = initialTime;
                isTimerActive = true;
            }
        }
    }
}
