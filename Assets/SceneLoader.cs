using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public AudioClip buttonClickSound;  // Buton týklama sesi
    private AudioSource audioSource;    // AudioSource referansý

    private void Start()
    {
        // AudioSource bileþenini al veya ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void LoadScene(string sceneName)
    {
        PlayButtonClickSound();  // Týklama sesi çal
        SceneManager.LoadScene(sceneName);  // Sahneyi yükle
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
