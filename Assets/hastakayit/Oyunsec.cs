using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunSec : MonoBehaviour
{
    private string currentUser;

    public AudioClip buttonClickSound;  // Ses dosyasý için alan
    private AudioSource audioSource;    // AudioSource referansý

    private void Start()
    {
        // GameObject'e AudioSource bileþeni ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Kullanýcý adýný ayarla
    public void SetCurrentUser(string userName)
    {
        currentUser = userName;
        PlayerPrefs.SetString("currentUser", currentUser); // Kullanýcýyý kaydediyoruz
    }

    // Butona týklandýðýnda çaðrýlacak fonksiyon
    public void OyunSecButonunaTikla()
    {
        // Ses efekti çal
        PlayButtonClickSound();

        // Kullanýcýyý bir sonraki sahneye yönlendirmeden önce saklýyoruz
        PlayerPrefs.SetString("currentUser", currentUser);

        // "Oyunsec" sahnesine yönlendir
        SceneManager.LoadScene("Oyunsec");
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi çal
        }
    }
}
