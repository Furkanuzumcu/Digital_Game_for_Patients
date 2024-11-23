using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManagerSag : MonoBehaviour
{
    public static GameManagerSag Instance { get; private set; }
    private Ball ball;
    private Paddle paddle;
    private Brick[] bricks;
    private string selectedArm = "Sa� Kol";
    private float minHorizontalAngle = 0f; // Minimum a�� i�in de�i�ken
    public HorizontalAngleCalculator angleCalculator;

    // Yeni eklenen Text bile�enleri
    public Text scoreText;  // Skoru g�sterecek Text
    public Text livesText;  // Canlar� g�sterecek Text

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 5;  // Oyuncunun ba�lang��taki can�

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);  // E�er ba�ka bir GameManagerSag varsa, bu nesneyi yok et
            return;
        }
        else
        {
            Instance = this;
            StartGame();  // Oyunu ba�lat
            FindSceneReferences();
        }
    }

    private void StartGame()
    {
        // Oyun ba�lang�c�nda canlar� ve skoru s�f�rla
        lives = 10;
        score = 0;
        UpdateUI();  // Can ve skor metinlerini g�ncelle
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
        Debug.Log("Ball: " + ball);
        Debug.Log("Paddle: " + paddle);

        // Ba�lang��ta minimum a��y� maksimuma ayarla
        minHorizontalAngle = 0f;

        // Referanslar�n do�ru atan�p atanmad���n� kontrol et
        if (angleCalculator == null)
        {
            Debug.LogWarning("AngleCalculator referans� atanmad�!");
        }
    }

    void Update()
    {
        if (angleCalculator != null)
        {
            float currentAngle = angleCalculator.GetCurrentHorizontalAngle();
            if (currentAngle > -94)
            {
                // E�er yeni a�� daha d���kse (daha negatif) minimum a��y� g�ncelle
                if (currentAngle < minHorizontalAngle)
                {
                    minHorizontalAngle = currentAngle; // Minimum a��y� tut
                }
            }
        }
    }

    public void OnBallMiss()
    {
        lives--;
        UpdateUI();  // Can metnini g�ncelle

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            SaveGameResult();
            SceneManager.LoadScene("rapor");
        }
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points; // Skoru art�r
        UpdateUI();  // Skor metnini g�ncelle

        Debug.Log("G�ncel skor: " + score);

        if (Cleared())
        {
            SaveGameResult();
            SceneManager.LoadScene("rapor");
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }

    void SaveGameResult()
    {
        string hastaAdi = PlayerPrefs.GetString("hastaAdi", "Bilinmeyen");
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi", "Kullan�c�");
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        string filePath = Application.persistentDataPath + "/rapor_" + hastaAdi + "_" + hastaSoyadi + ".json";

        GameResult result = new GameResult
        {
            oyuncuAdi = hastaAdi,
            oyuncuSoyadi = hastaSoyadi,
            skor = score,
            maxKolAci = -minHorizontalAngle, // Minimum a��y� kaydediyoruz
            oynamaTarihi = currentDate,
            kullanilanKol = selectedArm,
            oyunAdi = "Breaking Brick"
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

        Debug.Log("Oyun sonucu kaydedildi: " + filePath);
    }

    // Can ve skoru g�ncelleyerek ekrana yazd�ran fonksiyon
    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skor: " + score.ToString();
        }

        if (livesText != null)
        {
            livesText.text = "Can: " + lives.ToString();
        }
    }
}
