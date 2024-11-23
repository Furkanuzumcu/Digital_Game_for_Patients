using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HastaGiris : MonoBehaviour
{
    public InputField tcNoField;
    public AudioClip buttonClickSound;  // Ses dosyasý için alan
    private AudioSource audioSource;    // AudioSource referansý

    public GameObject suggestionPanel;  // TC önerilerinin gösterileceði panel
    public GameObject suggestionTextPrefab;   // Her öneri için Text prefab

    private List<string> hastaTcListesi = new List<string>();  // Tüm TC numaralarýnýn listesi

    private void Start()
    {
        suggestionPanel.SetActive(false);
        // GameObject'e AudioSource bileþeni ekle
        audioSource = gameObject.AddComponent<AudioSource>();

        // Kayýtlý hasta dosyalarýný kontrol et
        LoadHastaTcList();

        // InputField deðiþikliklerini dinle
        tcNoField.onValueChanged.AddListener(delegate { ShowSuggestions(tcNoField.text); });
    }

    private void LoadHastaTcList()
    {
        // Kayýtlý hasta dosyalarýný bul ve TC numaralarýný listeye ekle
        string[] files = Directory.GetFiles(Application.persistentDataPath, "hasta_*.json");
        foreach (string file in files)
        {
            string tcNo = Path.GetFileNameWithoutExtension(file).Replace("hasta_", "");
            hastaTcListesi.Add(tcNo);
        }

        Debug.Log("Toplam kayýtlý hasta: " + hastaTcListesi.Count);
    }

    private void ShowSuggestions(string input)
    {
        // Öneri panelini temizle
        foreach (Transform child in suggestionPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Eðer girdi boþsa öneri panelini kapat
        if (string.IsNullOrEmpty(input))
        {
            suggestionPanel.SetActive(false);
            return;
        }

        // Eþleþen TC numaralarýný öneri olarak göster
        List<string> filteredTcList = hastaTcListesi.FindAll(tc => tc.StartsWith(input));

        if (filteredTcList.Count > 0)
        {
            suggestionPanel.SetActive(true);

            foreach (string tc in filteredTcList)
            {
                GameObject suggestionText = Instantiate(suggestionTextPrefab, suggestionPanel.transform);
                suggestionText.GetComponentInChildren<Text>().text = tc;

                // Öneriye týklandýðýnda InputField'e TC numarasýný yaz
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
        tcNoField.text = tc;  // Önerilen TC numarasýný InputField'e yaz
        suggestionPanel.SetActive(false);  // Öneri panelini kapat
    }

    public void GirisYap()
    {
        // Ses efekti çal
        PlayButtonClickSound();

        string tcNo = tcNoField.text;
        string filePath = Application.persistentDataPath + "/hasta_" + tcNo + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            HastaBilgi hasta = JsonUtility.FromJson<HastaBilgi>(json);

            // Debug mesajý ile giriþin baþarýlý olduðunu bildirin
            Debug.Log("Giriþ baþarýlý! Hoþgeldin " + hasta.hastaAdi + " " + hasta.hastaSoyadi);

            // Kullanýcý adýný PlayerPrefs ile sakla
            PlayerPrefs.SetString("hastaAdi", hasta.hastaAdi);
            PlayerPrefs.SetString("hastaSoyadi", hasta.hastaSoyadi);

            // "Rapor" sahnesine yönlendir
            SceneManager.LoadScene("rapor");
        }
        else
        {
            // Debug mesajý ile kullanýcýnýn bulunamadýðýný bildirin
            Debug.Log("Kayýt bulunamadý. Lütfen TC numarasýný kontrol edin.");
        }
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi çal
        }
    }
}
