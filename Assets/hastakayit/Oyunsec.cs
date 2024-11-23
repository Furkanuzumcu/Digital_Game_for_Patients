using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunSec : MonoBehaviour
{
    private string currentUser;

    public AudioClip buttonClickSound;  // Ses dosyas� i�in alan
    private AudioSource audioSource;    // AudioSource referans�

    private void Start()
    {
        // GameObject'e AudioSource bile�eni ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Kullan�c� ad�n� ayarla
    public void SetCurrentUser(string userName)
    {
        currentUser = userName;
        PlayerPrefs.SetString("currentUser", currentUser); // Kullan�c�y� kaydediyoruz
    }

    // Butona t�kland���nda �a�r�lacak fonksiyon
    public void OyunSecButonunaTikla()
    {
        // Ses efekti �al
        PlayButtonClickSound();

        // Kullan�c�y� bir sonraki sahneye y�nlendirmeden �nce sakl�yoruz
        PlayerPrefs.SetString("currentUser", currentUser);

        // "Oyunsec" sahnesine y�nlendir
        SceneManager.LoadScene("Oyunsec");
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi �al
        }
    }
}
