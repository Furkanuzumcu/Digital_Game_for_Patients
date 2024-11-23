using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HastaGiris : MonoBehaviour
{
    public InputField tcNoField;
    public AudioClip buttonClickSound;  // Ses dosyas� i�in alan
    private AudioSource audioSource;    // AudioSource referans�

    public GameObject suggestionPanel;  // TC �nerilerinin g�sterilece�i panel
    public GameObject suggestionTextPrefab;   // Her �neri i�in Text prefab

    private List<string> hastaTcListesi = new List<string>();  // T�m TC numaralar�n�n listesi

    private void Start()
    {
        suggestionPanel.SetActive(false);
        // GameObject'e AudioSource bile�eni ekle
        audioSource = gameObject.AddComponent<AudioSource>();

        // Kay�tl� hasta dosyalar�n� kontrol et
        LoadHastaTcList();

        // InputField de�i�ikliklerini dinle
        tcNoField.onValueChanged.AddListener(delegate { ShowSuggestions(tcNoField.text); });
    }

    private void LoadHastaTcList()
    {
        // Kay�tl� hasta dosyalar�n� bul ve TC numaralar�n� listeye ekle
        string[] files = Directory.GetFiles(Application.persistentDataPath, "hasta_*.json");
        foreach (string file in files)
        {
            string tcNo = Path.GetFileNameWithoutExtension(file).Replace("hasta_", "");
            hastaTcListesi.Add(tcNo);
        }

        Debug.Log("Toplam kay�tl� hasta: " + hastaTcListesi.Count);
    }

    private void ShowSuggestions(string input)
    {
        // �neri panelini temizle
        foreach (Transform child in suggestionPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // E�er girdi bo�sa �neri panelini kapat
        if (string.IsNullOrEmpty(input))
        {
            suggestionPanel.SetActive(false);
            return;
        }

        // E�le�en TC numaralar�n� �neri olarak g�ster
        List<string> filteredTcList = hastaTcListesi.FindAll(tc => tc.StartsWith(input));

        if (filteredTcList.Count > 0)
        {
            suggestionPanel.SetActive(true);

            foreach (string tc in filteredTcList)
            {
                GameObject suggestionText = Instantiate(suggestionTextPrefab, suggestionPanel.transform);
                suggestionText.GetComponentInChildren<Text>().text = tc;

                // �neriye t�kland���nda InputField'e TC numaras�n� yaz
                suggestionText.GetComponent<Button>().onClick.AddListener(() => SelectTcFromSuggestion(tc));
            }
        }
        else
        {
            suggestionPanel.SetActive(false);
        }
    }

    private void SelectTcFromSuggestion(string tc)
    {
        tcNoField.text = tc;  // �nerilen TC numaras�n� InputField'e yaz
        suggestionPanel.SetActive(false);  // �neri panelini kapat
    }

    public void GirisYap()
    {
        // Ses efekti �al
        PlayButtonClickSound();

        string tcNo = tcNoField.text;
        string filePath = Application.persistentDataPath + "/hasta_" + tcNo + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            HastaBilgi hasta = JsonUtility.FromJson<HastaBilgi>(json);

            // Debug mesaj� ile giri�in ba�ar�l� oldu�unu bildirin
            Debug.Log("Giri� ba�ar�l�! Ho�geldin " + hasta.hastaAdi + " " + hasta.hastaSoyadi);

            // Kullan�c� ad�n� PlayerPrefs ile sakla
            PlayerPrefs.SetString("hastaAdi", hasta.hastaAdi);
            PlayerPrefs.SetString("hastaSoyadi", hasta.hastaSoyadi);

            // "Rapor" sahnesine y�nlendir
            SceneManager.LoadScene("rapor");
        }
        else
        {
            // Debug mesaj� ile kullan�c�n�n bulunamad���n� bildirin
            Debug.Log("Kay�t bulunamad�. L�tfen TC numaras�n� kontrol edin.");
        }
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi �al
        }
    }
}
