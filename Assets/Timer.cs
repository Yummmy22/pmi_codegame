using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject starPrefab1; // Prefab UI Image untuk bintang
    [SerializeField] GameObject starPrefab2; // Prefab UI Image untuk bintang
    [SerializeField] GameObject starPrefab3; // Prefab UI Image untuk bintang
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject inputField;
    [SerializeField] float initialTime;
    [SerializeField] int maxStars; // Jumlah maksimum bintang yang mungkin
    private float remainingTime;
    private bool isTimerActive = false;
    void Start() {
        starPrefab1.SetActive(false);
        starPrefab2.SetActive(false);
        starPrefab3.SetActive(false);
        gameOver.SetActive(false);
        inputField.SetActive(false);
    }
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
                gameOver.SetActive(true);
                timerText.color = Color.red;
                CalculateScore(); // Hitung skor saat permainan berakhir
                isTimerActive = false;
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            // timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = Mathf.FloorToInt(remainingTime).ToString("00");
        }

        // Cek apakah tombol "Enter" ditekan
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Aktifkan timer jika belum aktif
            if (!isTimerActive)
            {
                remainingTime = initialTime;
                isTimerActive = true;
                inputField.SetActive(true);
            }
        }
    }

    // Method untuk menghitung skor
    public void CalculateScore()
    {
        float timePercentage = remainingTime / initialTime; // Hitung persentase waktu tersisa
        Debug.Log("Time Percentage: " + timePercentage);
        // Tentukan jumlah bintang berdasarkan rentang waktu
        if (timePercentage >= 0.67f) // 67% atau lebih, 3 bintang
        {
            starPrefab1.SetActive(true);
            starPrefab2.SetActive(true);
            starPrefab3.SetActive(true);
        }
        else if (timePercentage >= 0.33f) // 33% atau lebih, 2 bintang
        {
            starPrefab1.SetActive(true);
            starPrefab2.SetActive(true);
            starPrefab3.SetActive(false);
        }
        else if (timePercentage >= 0.01f) // Kurang dari 33%, 1 bintang
        {
            starPrefab1.SetActive(false);
            starPrefab2.SetActive(true);
            starPrefab3.SetActive(false);
        }

        isTimerActive = false; // Hentikan timer
        inputField.SetActive(false); // Hentikan input field
    }
}
