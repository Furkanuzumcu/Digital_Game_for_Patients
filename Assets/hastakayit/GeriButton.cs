using UnityEngine;
using UnityEngine.SceneManagement;

public class GeriButton : MonoBehaviour
{
    public AudioClip buttonClickSound;  // Buton t�klama sesi
    private AudioSource audioSource;    // AudioSource referans�

    private void Start()
    {
        // AudioSource bile�enini al veya ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void GoToGirisScene()
    {
        PlayButtonClickSound();  // T�klama sesi �al
        Invoke(nameof(LoadGirisScene), 0.1f);  // Sahneler aras� ge�i� i�in k�sa bir bekleme
    }

    public void GoToRaporScene()
    {
        PlayButtonClickSound();  // T�klama sesi �al
        Invoke(nameof(LoadRaporScene), 0.1f);  // Sahneler aras� ge�i� i�in k�sa bir bekleme
    }

    // "grs" sahnesini y�kleme fonksiyonu
    private void LoadGirisScene()
    {
        SceneManager.LoadScene("grs");
    }

    // "rapor" sahnesini y�kleme fonksiyonu
    private void LoadRaporScene()
    {
        SceneManager.LoadScene("rapor");
    }

    // Buton t�klama sesini �alma fonksiyonu
    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi �al
        }
    }
}
