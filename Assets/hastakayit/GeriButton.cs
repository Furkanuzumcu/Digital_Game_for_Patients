using UnityEngine;
using UnityEngine.SceneManagement;

public class GeriButton : MonoBehaviour
{
    public AudioClip buttonClickSound;  // Buton týklama sesi
    private AudioSource audioSource;    // AudioSource referansý

    private void Start()
    {
        // AudioSource bileþenini al veya ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void GoToGirisScene()
    {
        PlayButtonClickSound();  // Týklama sesi çal
        Invoke(nameof(LoadGirisScene), 0.1f);  // Sahneler arasý geçiþ için kýsa bir bekleme
    }

    public void GoToRaporScene()
    {
        PlayButtonClickSound();  // Týklama sesi çal
        Invoke(nameof(LoadRaporScene), 0.1f);  // Sahneler arasý geçiþ için kýsa bir bekleme
    }

    // "grs" sahnesini yükleme fonksiyonu
    private void LoadGirisScene()
    {
        SceneManager.LoadScene("grs");
    }

    // "rapor" sahnesini yükleme fonksiyonu
    private void LoadRaporScene()
    {
        SceneManager.LoadScene("rapor");
    }

    // Buton týklama sesini çalma fonksiyonu
    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi çal
        }
    }
}
