using UnityEngine;
using System.Collections;

public class KullaniciKontrol : MonoBehaviour
{
    public GameObject statusText;  // "Kullan�c� Bekleniyor..." mesaj� i�in Text
    private bool isUserDetected = false;

    public Ball ball;
    private Paddle paddle;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();

        StartCoroutine(CheckUserStatus());  // Kullan�c� durumunu s�rekli kontrol eden coroutine ba�lat
    }

    private IEnumerator CheckUserStatus()
    {
        while (true)  // Sonsuz d�ng�
        {
            // Kullan�c� alg�lanma durumunu kontrol et
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
            statusText.SetActive(true);  // "Kullan�c� Bekleniyor..." mesaj�n� g�ster
        }
    }

    private void ResumeGame()
    {
        if (ball != null) ball.enabled = true;  // Top hareketini ba�lat
        if (paddle != null) paddle.enabled = true;  // Paddle hareketini ba�lat

        if (statusText != null)
        {
            statusText.SetActive(false);  // "Kullan�c� Bekleniyor..." mesaj�n� gizle
        }
    }
}
