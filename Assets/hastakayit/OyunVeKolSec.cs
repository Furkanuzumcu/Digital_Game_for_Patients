using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunVeKolSec : MonoBehaviour
{
    private string selectedGame;  // Se�ilen oyun (Pong veya Breaking Brick)

    // Kol se�imi butonlar�n� tutan GameObject'ler
    public GameObject sagKolButton;
    public GameObject solKolButton;

    // Oyun butonlar�
    public GameObject pongButton;
    public GameObject breakingBrickButton;

    public AudioClip buttonClickSound;  // Ses dosyas� i�in alan
    private AudioSource audioSource;    // AudioSource referans�

    private void Start()
    {
        // Ba�lang��ta kol se�im butonlar�n� gizle
        sagKolButton.SetActive(false);
        solKolButton.SetActive(false);

        // GameObject'e AudioSource bile�eni ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Pong Oyununu Se�ti�inde �a�r�lacak Fonksiyon
    public void SelectPong()
    {
        PlayButtonClickSound();  // Ses efekti �al
        selectedGame = "Pong";
        PlayerPrefs.SetString("selectedGame", selectedGame);  // Oyun ad�n� kaydet

        Debug.Log("Pong oyunu se�ildi, selectedGame: " + selectedGame);

        // Kol se�imi butonlar�n� g�ster ve Pong butonunu devre d��� b�rak
        pongButton.SetActive(false);
        breakingBrickButton.SetActive(false);
        ShowKolSecimi();
    }

    // Breaking Brick Oyununu Se�ti�inde �a�r�lacak Fonksiyon
    public void SelectBreakingBrick()
    {
        PlayButtonClickSound();  // Ses efekti �al
        selectedGame = "BreakingBrick";
        PlayerPrefs.SetString("selectedGame", selectedGame);

        Debug.Log("Breaking Brick oyunu se�ildi, selectedGame: " + selectedGame);

        pongButton.SetActive(false);
        breakingBrickButton.SetActive(false);
        ShowKolSecimi();
    }

    // Sa� kol se�ildi�inde �a�r�lacak fonksiyon
    public void RightArmSelected()
    {
        PlayButtonClickSound();  // Ses efekti �al
        PlayerPrefs.SetString("selectedArm", "Sa� Kol");

        Debug.Log("Sa� kol se�ildi, selectedGame: " + selectedGame);

        if (selectedGame == "Pong")
        {
            SceneManager.LoadScene("ponkypoz");
        }
        else if (selectedGame == "BreakingBrick")
        {
            SceneManager.LoadScene("breaksag");
        }
    }

    // Sol kol se�ildi�inde �a�r�lacak fonksiyon
    public void LeftArmSelected()
    {
        PlayButtonClickSound();  // Ses efekti �al
        PlayerPrefs.SetString("selectedArm", "Sol Kol");

        Debug.Log("Sol kol se�ildi, selectedGame: " + selectedGame);

        if (selectedGame == "Pong")
        {
            SceneManager.LoadScene("ponksolypoz");
        }
        else if (selectedGame == "BreakingBrick")
        {
            SceneManager.LoadScene("breaksol");
        }
    }

    // Kol se�im butonlar�n� aktif et
    private void ShowKolSecimi()
    {
        sagKolButton.SetActive(true);
        solKolButton.SetActive(true);
    }

    // Ses efekti �alma fonksiyonu
    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Ses efektini �al
        }
    }
}
