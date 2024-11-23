using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // UI Canvas'� referans al
    public AudioClip resumeSound;   // Devam etme sesi
    private AudioSource audioSource; // AudioSource referans�

    private bool isPaused = false;

    void Start()
    {
        // Oyun ba�lad���nda pause men�s�n� gizle
        pauseMenuUI.SetActive(false);

        // AudioSource bile�enini al veya ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // "Escape" tu�una bas�ld���nda oyunu duraklat veya devam ettir
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
            audioSource.PlayOneShot(resumeSound); // Sesi �al
        }

        pauseMenuUI.SetActive(false); // Pause men�s�n� gizle
        Time.timeScale = 1f; // Zaman� normale al (1 = normal h�z)
        isPaused = false;
    }

    // Oyunu duraklat
    void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Pause men�s�n� g�ster
        Time.timeScale = 0f; // Zaman� durdur (0 = duraklat�lm��)
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Zaman� normale d�nd�r
        SceneManager.LoadScene("rapor"); // Ana men� sahnesine d�n
    }
}
