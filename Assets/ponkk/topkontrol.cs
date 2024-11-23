using System.Collections;
using UnityEngine;

public class topkontrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float startingSpeed;
    private Vector3 initialPosition;
    public AudioSource paddleHitSound;  // Pedala �arpma sesi
    public AudioSource wallHitSound;    // Duvara �arpma sesi
    public ScoreManagerSol scoreManagerSol;  // Sol kol i�in ScoreManager
    public ScoreManager scoreManager;

    // KinectManager referans� (kullan�c�y� alg�lamak i�in)
    public KinectManager kinectManager;

    void Start()
    {
        initialPosition = new Vector3(transform.position.x, transform.position.y, 316f);  // Z de�erini sabitle
        StartCoroutine(WaitForKinectUser()); // Kinect kullan�c�s�n� bekleyerek ba�lat
    }

    IEnumerator WaitForKinectUser()
    {
        // Kinect ba�l� ve kullan�c� alg�lanana kadar bekle
        while (kinectManager == null || !kinectManager.IsUserDetected())
        {
            Debug.Log("Kinect kullan�c�s� alg�lanmad�, bekleniyor...");
            yield return null;  // Bir sonraki frame'i bekle
        }

        Debug.Log("Kinect kullan�c�s� alg�land�, oyun ba�l�yor!");
        StartCoroutine(ResetBallPositionWithDelay(1.5f)); // Oyun ba��nda 1.5 saniye gecikme
    }

    public void ResetBallPosition()
    {
        transform.position = initialPosition;  // Z de�erini 316 olarak sabitliyoruz
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
            // E�er ScoreManager atanm��sa, sa� kol i�in skoru art�r
            else if (scoreManager != null)
            {
                paddleHitSound.Play();
                scoreManager.PlayerScores();
            }
            
            
            // Pedala �arpt���nda sesi �al
            
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Duvara �arpt���nda sesi �al
            wallHitSound.Play();
        }
    }

    IEnumerator ResetBallPositionWithDelay(float delay)
    {
        rb.velocity = Vector2.zero;  // Topu durdur
        yield return new WaitForSeconds(delay);  // Belirtilen s�re kadar bekle
        ResetBallPosition();  // Topu yeniden ba�lat
    }

    public void ResetBallAfterScore()
    {
        StartCoroutine(ResetBallPositionWithDelay(1f)); // Skor sonras� 1 saniye bekleme
    }
}
