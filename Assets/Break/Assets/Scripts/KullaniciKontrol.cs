using UnityEngine;
using System.Collections;

public class KullaniciKontrol : MonoBehaviour
{
    public GameObject statusText;  // "Kullanýcý Bekleniyor..." mesajý için Text
    private bool isUserDetected = false;

    public Ball ball;
    private Paddle paddle;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();

        StartCoroutine(CheckUserStatus());  // Kullanýcý durumunu sürekli kontrol eden coroutine baþlat
    }

    private IEnumerator CheckUserStatus()
    {
        while (true)  // Sonsuz döngü
        {
            // Kullanýcý algýlanma durumunu kontrol et
            isUserDetected = KinectManager.Instance && KinectManager.Instance.IsUserDetected();

            if (!isUserDetected)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }

            yield return new WaitForSeconds(0.5f);  // 0.5 saniyede bir kontrol et
        }
    }

    private void PauseGame()
    {
        if (ball != null) ball.enabled = false;  // Top hareketini durdur
        if (paddle != null) paddle.enabled = false;  // Paddle hareketini durdur

        if (statusText != null)
        {
            statusText.SetActive(true);  // "Kullanýcý Bekleniyor..." mesajýný göster
        }
    }

    private void ResumeGame()
    {
        if (ball != null) ball.enabled = true;  // Top hareketini baþlat
        if (paddle != null) paddle.enabled = true;  // Paddle hareketini baþlat

        if (statusText != null)
        {
            statusText.SetActive(false);  // "Kullanýcý Bekleniyor..." mesajýný gizle
        }
    }
}
