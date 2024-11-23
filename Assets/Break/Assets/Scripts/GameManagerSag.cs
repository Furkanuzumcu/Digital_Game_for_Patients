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
    private string selectedArm = "Sað Kol";
    private float minHorizontalAngle = 0f; // Minimum açý için deðiþken
    public HorizontalAngleCalculator angleCalculator;

    // Yeni eklenen Text bileþenleri
    public Text scoreText;  // Skoru gösterecek Text
    public Text livesText;  // Canlarý gösterecek Text

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 5;  // Oyuncunun baþlangýçtaki caný

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);  // Eðer baþka bir GameManagerSag varsa, bu nesneyi yok et
            return;
        }
        else
        {
            Instance = this;
            StartGame();  // Oyunu baþlat
            FindSceneReferences();
        }
    }

    private void StartGame()
    {
        // Oyun baþlangýcýnda canlarý ve skoru sýfýrla
        lives = 10;
        score = 0;
        UpdateUI();  // Can ve skor metinlerini güncelle
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
        Debug.Log("Ball: " + ball);
        Debug.Log("Paddle: " + paddle);

        // Baþlangýçta minimum açýyý maksimuma ayarla
        minHorizontalAngle = 0f;

        // Referanslarýn doðru atanýp atanmadýðýný kontrol et
        if (angleCalculator == null)
        {
            Debug.LogWarning("AngleCalculator referansý atanmadý!");
        }
    }

    void Update()
    {
        if (angleCalculator != null)
        {
            float currentAngle = angleCalculator.GetCurrentHorizontalAngle();
            if (currentAngle > -94)
            {
                // Eðer yeni açý daha düþükse (daha negatif) minimum açýyý güncelle
                if (currentAngle < minHorizontalAngle)
                {
                    minHorizontalAngle = currentAngle; // Minimum açýyý tut
                }
            }
        }
    }

    public void OnBallMiss()
    {
        lives--;
        UpdateUI();  // Can metnini güncelle

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
        score += brick.points; // Skoru artýr
        UpdateUI();  // Skor metnini güncelle

        Debug.Log("Güncel skor: " + score);

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
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi", "Kullanýcý");
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        string filePath = Application.persistentDataPath + "/rapor_" + hastaAdi + "_" + hastaSoyadi + ".json";

        GameResult result = new GameResult
        {
            oyuncuAdi = hastaAdi,
            oyuncuSoyadi = hastaSoyadi,
            skor = score,
            maxKolAci = -minHorizontalAngle, // Minimum açýyý kaydediyoruz
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

    // Can ve skoru güncelleyerek ekrana yazdýran fonksiyon
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
