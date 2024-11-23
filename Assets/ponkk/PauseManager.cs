using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // UI Canvas'ý referans al
    public AudioClip resumeSound;   // Devam etme sesi
    private AudioSource audioSource; // AudioSource referansý

    private bool isPaused = false;

    void Start()
    {
        // Oyun baþladýðýnda pause menüsünü gizle
        pauseMenuUI.SetActive(false);

        // AudioSource bileþenini al veya ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // "Escape" tuþuna basýldýðýnda oyunu duraklat veya devam ettir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Oyunu devam ettir
    public void ResumeGame()
    {
        if (resumeSound != null)
        {
            audioSource.PlayOneShot(resumeSound); // Sesi çal
        }

        pauseMenuUI.SetActive(false); // Pause menüsünü gizle
        Time.timeScale = 1f; // Zamaný normale al (1 = normal hýz)
        isPaused = false;
    }

    // Oyunu duraklat
    void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Pause menüsünü göster
        Time.timeScale = 0f; // Zamaný durdur (0 = duraklatýlmýþ)
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Zamaný normale döndür
        SceneManager.LoadScene("rapor"); // Ana menü sahnesine dön
    }
}
