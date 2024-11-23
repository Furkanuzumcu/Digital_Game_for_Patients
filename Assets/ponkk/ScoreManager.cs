using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public int computerScore = 0;
    public Text playerScoreText;
    public int winningScore = 20;
    public Text playerWinText;
    public Text computerWinText;
    public topkontrol ballController;
    public AudioSource scorePositiveSound;
    public AudioSource scoreNegativeSound;

    private int maxShoulderAngle = 0; // Artýk doðrudan int
    public ShoulderAngleCalculator angleCalculator;
    private string selectedArm = "Sað Kol";

    void Start()
    {
        selectedArm = PlayerPrefs.GetString("selectedArm", "Sað Kol");
        UpdateScoreText();
        playerWinText.text = "";
        computerWinText.text = "";
        maxShoulderAngle = 0;
    }

    void Update()
    {
        float currentAngle = angleCalculator.GetCurrentAngle();
        int roundedAngle = Mathf.RoundToInt(currentAngle);

        if (roundedAngle > maxShoulderAngle)
        {
            maxShoulderAngle = roundedAngle;
        }
    }

    public void PlayerScores()
    {
        scorePositiveSound.Play();
        playerScore++;
        UpdateScoreText();

        if (playerScore >= winningScore)
        {
            EndGame(true);
        }

    }



    void UpdateScoreText()
    {
        playerScoreText.text = playerScore.ToString();
       
    }

    void EndGame(bool isPlayerWinner)
    {
        string hastaAdi = PlayerPrefs.GetString("hastaAdi", "Bilinmeyen");
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi", "Kullanýcý");
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        try
        {
            SaveGameResult(hastaAdi, hastaSoyadi, playerScore, maxShoulderAngle, selectedArm, currentDate);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Kayýt sýrasýnda hata oluþtu: {ex.Message}");
        }

        Debug.Log($"Oyun bitti! Kazanan: {(isPlayerWinner ? "Oyuncu" : "Bilgisayar")}");
        Debug.Log($"Oynayan Kullanýcý: {hastaAdi} {hastaSoyadi}");
        Debug.Log($"Oyun sýrasýndaki maksimum kol açýsý: {maxShoulderAngle} derece");
        Debug.Log($"Oyuncunun Skoru: {playerScore}");
        Debug.Log($"Oyun tarihi: {currentDate}");
        Debug.Log($"Kullanýlan Kol: {selectedArm}");

        ballController.rb.velocity = Vector2.zero;
        ballController.enabled = false;

        StartCoroutine(GoToRaporSceneAfterDelay(1f));
    }

    void SaveGameResult(string ad, string soyad, int score, int maxAngle, string kol, string tarih)
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
            oyunAdi = "Ponk"
        };

        GameResultsList resultsList = new GameResultsList();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            try
            {
                resultsList = JsonUtility.FromJson<GameResultsList>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON okuma hatasý: {ex.Message}");
                resultsList = new GameResultsList(); // Boþ bir liste baþlat
            }
        }

        resultsList.results.Add(result);

        string updatedJson = JsonUtility.ToJson(resultsList, true);
        File.WriteAllText(filePath, updatedJson);

        Debug.Log("Kayýt baþarýlý! Oyun sonucu " + filePath + " dosyasýna kaydedildi.");
    }

    IEnumerator GoToRaporSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("rapor");
    }
}
