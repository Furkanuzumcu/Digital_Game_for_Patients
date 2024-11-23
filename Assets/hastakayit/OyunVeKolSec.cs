using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunVeKolSec : MonoBehaviour
{
    private string selectedGame;  // Seçilen oyun (Pong veya Breaking Brick)

    // Kol seçimi butonlarýný tutan GameObject'ler
    public GameObject sagKolButton;
    public GameObject solKolButton;

    // Oyun butonlarý
    public GameObject pongButton;
    public GameObject breakingBrickButton;

    public AudioClip buttonClickSound;  // Ses dosyasý için alan
    private AudioSource audioSource;    // AudioSource referansý

    private void Start()
    {
        // Baþlangýçta kol seçim butonlarýný gizle
        sagKolButton.SetActive(false);
        solKolButton.SetActive(false);

        // GameObject'e AudioSource bileþeni ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Pong Oyununu Seçtiðinde Çaðrýlacak Fonksiyon
    public void SelectPong()
    {
        PlayButtonClickSound();  // Ses efekti çal
        selectedGame = "Pong";
        PlayerPrefs.SetString("selectedGame", selectedGame);  // Oyun adýný kaydet

        Debug.Log("Pong oyunu seçildi, selectedGame: " + selectedGame);

        // Kol seçimi butonlarýný göster ve Pong butonunu devre dýþý býrak
        pongButton.SetActive(false);
        breakingBrickButton.SetActive(false);
        ShowKolSecimi();
    }

    // Breaking Brick Oyununu Seçtiðinde Çaðrýlacak Fonksiyon
    public void SelectBreakingBrick()
    {
        PlayButtonClickSound();  // Ses efekti çal
        selectedGame = "BreakingBrick";
        PlayerPrefs.SetString("selectedGame", selectedGame);

        Debug.Log("Breaking Brick oyunu seçildi, selectedGame: " + selectedGame);

        pongButton.SetActive(false);
        breakingBrickButton.SetActive(false);
        ShowKolSecimi();
    }

    // Sað kol seçildiðinde çaðrýlacak fonksiyon
    public void RightArmSelected()
    {
        PlayButtonClickSound();  // Ses efekti çal
        PlayerPrefs.SetString("selectedArm", "Sað Kol");

        Debug.Log("Sað kol seçildi, selectedGame: " + selectedGame);

        if (selectedGame == "Pong")
        {
            SceneManager.LoadScene("ponkypoz");
        }
        else if (selectedGame == "BreakingBrick")
        {
            SceneManager.LoadScene("breaksag");
        }
    }

    // Sol kol seçildiðinde çaðrýlacak fonksiyon
    public void LeftArmSelected()
    {
        PlayButtonClickSound();  // Ses efekti çal
        PlayerPrefs.SetString("selectedArm", "Sol Kol");

        Debug.Log("Sol kol seçildi, selectedGame: " + selectedGame);

        if (selectedGame == "Pong")
        {
            SceneManager.LoadScene("ponksolypoz");
        }
        else if (selectedGame == "BreakingBrick")
        {
            SceneManager.LoadScene("breaksol");
        }
    }

    // Kol seçim butonlarýný aktif et
    private void ShowKolSecimi()
    {
        sagKolButton.SetActive(true);
        solKolButton.SetActive(true);
    }

    // Ses efekti çalma fonksiyonu
    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Ses efektini çal
        }
    }
}
