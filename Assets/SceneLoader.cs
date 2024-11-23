using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public AudioClip buttonClickSound;  // Buton t�klama sesi
    private AudioSource audioSource;    // AudioSource referans�

    private void Start()
    {
        // AudioSource bile�enini al veya ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void LoadScene(string sceneName)
    {
        PlayButtonClickSound();  // T�klama sesi �al
        SceneManager.LoadScene(sceneName);  // Sahneyi y�kle
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
