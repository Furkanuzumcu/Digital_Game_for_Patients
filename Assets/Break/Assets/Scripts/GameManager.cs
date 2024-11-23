using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // UI bileşenlerini kullanmak için

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Ball ball;
    private Paddlesol paddlesol;
    private Brick[] bricks;
    private string selectedArm = "Sol Kol";
    private float maxHorizontalAngle = 0f; // Maksimum açı için değişken
    public HorizontalAngleCalculator angleCalculator;

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 5;  // Oyuncunun başlangıçtaki canı
    public string raporScene = "rapor";  // Oyun sonunda yönlendirilmek istenen rapor sahnesi

    // Yeni eklenen Text bileşenleri
    public Text scoreText;  // Skoru gösterecek Text
    public Text livesText;  // Canları gösterecek Text

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);  // Eğer zaten bir GameManager varsa, yenisini yok et
            return;
        }
        else
        {
            Instance = this;
            StartGame();  // Oyunu başlat
            FindSceneReferences();
        }
    }

    private void StartGame()
    {
        // Oyunun başında canları sıfırla
        lives = 10;
        score = 0;  // Skoru sıfırla
        UpdateUI();  // Can ve skor metinlerini güncelle
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddlesol = FindObjectOfType<Paddlesol>();
        bricks = FindObjectsOfType<Brick>();
        Debug.Log("Ball: " + ball);
        Debug.Log("Paddle: " + paddlesol);

        // Başlangıçta maksimum açıyı 0'a ayarla
        maxHorizontalAngle = 0f;

        // Referansların doğru atanıp atanmadığını kontrol et
        if (angleCalculator == null)
        {
            Debug.LogWarning("AngleCalculator referansı atanmadı!");
        }
    }

    void Update()
    {
        if (angleCalculator != null)
        {
            float currentAngle = angleCalculator.GetCurrentHorizontalAngle();

            // Açıya 180 ekleyerek düzeltiyoruz
            float adjustedAngle = currentAngle + 180;
            if (adjustedAngle < 94)
            {
                if (adjustedAngle > maxHorizontalAngle)
                {
                    maxHorizontalAngle = adjustedAngle; // Maksimum açıyı tut
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
            SceneManager.LoadScene(raporScene);
        }
    }

    private void ResetLevel()
    {
        paddlesol.ResetPaddle();
        ball.ResetBall();
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points; // Skoru artır
        UpdateUI();  // Skor metnini güncelle
        Debug.Log("Güncel skor: " + score);

        if (Cleared())
        {
            SaveGameResult();
            SceneManager.LoadScene(raporScene);
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
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi", "Kullanıcı");
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        string filePath = Application.persistentDataPath + "/rapor_" + hastaAdi + "_" + hastaSoyadi + ".json";

        GameResult result = new GameResult
        {
            oyuncuAdi = hastaAdi,
            oyuncuSoyadi = hastaSoyadi,
            skor = score,
            maxKolAci = maxHorizontalAngle, // Maksimum açıyı kaydediyoruz
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

    // Can ve skoru güncelleyerek ekrana yazdıran fonksiyon
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
