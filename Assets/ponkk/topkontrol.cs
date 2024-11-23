using System.Collections;
using UnityEngine;

public class topkontrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float startingSpeed;
    private Vector3 initialPosition;
    public AudioSource paddleHitSound;  // Pedala çarpma sesi
    public AudioSource wallHitSound;    // Duvara çarpma sesi
    public ScoreManagerSol scoreManagerSol;  // Sol kol için ScoreManager
    public ScoreManager scoreManager;

    // KinectManager referansý (kullanýcýyý algýlamak için)
    public KinectManager kinectManager;

    void Start()
    {
        initialPosition = new Vector3(transform.position.x, transform.position.y, 316f);  // Z deðerini sabitle
        StartCoroutine(WaitForKinectUser()); // Kinect kullanýcýsýný bekleyerek baþlat
    }

    IEnumerator WaitForKinectUser()
    {
        // Kinect baðlý ve kullanýcý algýlanana kadar bekle
        while (kinectManager == null || !kinectManager.IsUserDetected())
        {
            Debug.Log("Kinect kullanýcýsý algýlanmadý, bekleniyor...");
            yield return null;  // Bir sonraki frame'i bekle
        }

        Debug.Log("Kinect kullanýcýsý algýlandý, oyun baþlýyor!");
        StartCoroutine(ResetBallPositionWithDelay(1.5f)); // Oyun baþýnda 1.5 saniye gecikme
    }

    public void ResetBallPosition()
    {
        transform.position = initialPosition;  // Z deðerini 316 olarak sabitliyoruz
        bool isRight = UnityEngine.Random.value >= 0.5f;

        float xVelocity = isRight ? 1f : -1f;
        float yVelocity = UnityEngine.Random.Range(-1f, 1f);

        rb.velocity = new Vector2(xVelocity * startingSpeed, yVelocity * startingSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (scoreManagerSol != null)
            {
                paddleHitSound.Play();
                scoreManagerSol.PlayerScores();
            }
            // Eðer ScoreManager atanmýþsa, sað kol için skoru artýr
            else if (scoreManager != null)
            {
                paddleHitSound.Play();
                scoreManager.PlayerScores();
            }
            
            
            // Pedala çarptýðýnda sesi çal
            
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Duvara çarptýðýnda sesi çal
            wallHitSound.Play();
        }
    }

    IEnumerator ResetBallPositionWithDelay(float delay)
    {
        rb.velocity = Vector2.zero;  // Topu durdur
        yield return new WaitForSeconds(delay);  // Belirtilen süre kadar bekle
        ResetBallPosition();  // Topu yeniden baþlat
    }

    public void ResetBallAfterScore()
    {
        StartCoroutine(ResetBallPositionWithDelay(1f)); // Skor sonrasý 1 saniye bekleme
    }
}
