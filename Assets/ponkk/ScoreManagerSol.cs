using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class ScoreManagerSol : MonoBehaviour
{
    public int playerScore = 0;
    public Text playerScoreText;
    public Text computerScoreText;
    public int winningScore = 10;  // Kazanma skoru
    public Text playerWinText;  // Oyuncu kazandýðýnda gösterilecek mesaj
    public topkontrol ballController;
    public AudioSource scorePositiveSound;  // Pozitif skor sesi
    public AudioSource scoreNegativeSound;  // Negatif skor sesi


    // Yeni eklenen deðiþkenler
    private float maxShoulderAngle = 0f;  // Maksimum kol açýsý
    public ShoulderAngleCalculatorSol angleCalculator;  // Sol kol için açý hesaplayýcý
    private string selectedArm = "Sol Kol"; // Hangi kol seçilmiþ

    void Start()
    {
        // Kol seçimini PlayerPrefs'ten al
        selectedArm = PlayerPrefs.GetString("selectedArm", "Sol Kol");

        UpdateScoreText();
        playerWinText.text = "";  // Oyuncu kazandýðýnda gösterilecek mesaj baþlangýçta boþ
        maxShoulderAngle = 0f;  // Baþlangýçta açýyý sýfýrla
    }

    void Update()
    {
        // Kol açýsýný sürekli kontrol et
        float currentAngle = angleCalculator.GetCurrentAngle();
        if (currentAngle > maxShoulderAngle)
        {
            maxShoulderAngle = currentAngle;  // Maksimum açýyý güncelle
        }
    }

    public void PlayerScores()
    {
        scorePositiveSound.Play();
        playerScore++;
        UpdateScoreText();
        if (playerScore >= winningScore)
        {
            EndGame(true);  // Oyuncu kazandý
        }

    }



    void UpdateScoreText()
    {
        playerScoreText.text = "" + playerScore.ToString();

    }

    void EndGame(bool isPlayerWinner)
    {
        string hastaAdi = PlayerPrefs.GetString("hastaAdi", "Bilinmeyen");
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi", "Kullanýcý");

        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        // Oyunun adýný kaydet
        string oyunAdi = "Ponk";  // Sol kol ile oynandýðýnda da doðru çalýþmasý için buraya ekleyin
        PlayerPrefs.SetString("lastPlayedGame", oyunAdi);

        SaveGameResult(hastaAdi, hastaSoyadi, playerScore, maxShoulderAngle, selectedArm, currentDate);

        Debug.Log($"Oyun bitti! Oyuncu: {hastaAdi} {hastaSoyadi}, Oyun: {oyunAdi}");

        ballController.rb.velocity = Vector2.zero;
        ballController.enabled = false;

        StartCoroutine(GoToRaporSceneAfterDelay(1f));
    }

    void SaveGameResult(string ad, string soyad, int score, float maxAngle, string kol, string tarih)
    {
        string filePath = Application.persistentDataPath + "/rapor_" + ad + "_" + soyad + ".json";

        GameResult result = new GameResult
        {
            oyuncuAdi = ad,
            oyuncuSoyadi = soyad,
            skor = score,
            maxKolAci = maxAngle,
            oynamaTarihi = tarih,
            kullanilanKol = kol,
            oyunAdi = PlayerPrefs.GetString("lastPlayedGame", "Bilinmeyen Oyun")  // Oyunun adýný buradan alýn
        };

        GameResultsList resultsList = new GameResultsList();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            resultsList = JsonUtility.FromJson<GameResultsList>(json);
        }

        resultsList.results.Add(result);

        string updatedJson = JsonUtility.ToJson(resultsList, true);
        File.WriteAllText(filePath, updatedJson);

        Debug.Log("Kayýt baþarýlý! Oyun sonucu kaydedildi.");
    }

    IEnumerator GoToRaporSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("rapor");
    }
}
