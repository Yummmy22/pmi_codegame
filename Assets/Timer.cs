using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject starPrefab; // Prefab UI Image untuk bintang
    [SerializeField] Transform starParent; // Parent untuk semua bintang
    [SerializeField] float initialTime;
    [SerializeField] int maxStars; // Jumlah maksimum bintang yang mungkin
    private float remainingTime;
    private bool isTimerActive = false;
    private int score;

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
                CalculateScore(); // Hitung skor saat permainan berakhir
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

    // Method untuk menghitung skor
    void CalculateScore()
    {
        float timePercentage = remainingTime / initialTime; // Hitung persentase waktu tersisa

        // Tentukan jumlah bintang berdasarkan rentang waktu
        if (timePercentage >= 0.67f) // 67% atau lebih, 3 bintang
        {
            score = 3;
        }
        else if (timePercentage >= 0.33f) // 33% atau lebih, 2 bintang
        {
            score = 2;
        }
        else // Kurang dari 33%, 1 bintang
        {
            score = 1;
        }

        DisplayStars(score); // Tampilkan bintang sesuai dengan skor
    }

    // Method untuk menampilkan bintang
    void DisplayStars(int numberOfStars)
    {
        // Hapus semua bintang sebelum menampilkan yang baru
        foreach (Transform child in starParent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate UI Image bintang sejumlah numberOfStars
        for (int i = 0; i < numberOfStars; i++)
        {
            Instantiate(starPrefab, starParent);
        }
    }
}
