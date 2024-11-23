using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class HastaKayit : MonoBehaviour
{
    public InputField hastaAdiField;
    public InputField hastaSoyadiField;
    public InputField telNoField;
    public InputField tcNoField;
    public InputField gunField;  // Gün InputField
    public InputField ayField;   // Ay InputField
    public InputField yilField;  // Yýl InputField
    public Dropdown hastalikTuruDropdown;
    public InputField hastalikAciklamasiField;
    public InputField boyField;
    public InputField kiloField;
    public InputField teknisyenField;

    public AudioClip buttonClickSound;  // Ses dosyasý için alan
    private AudioSource audioSource;    // AudioSource referansý

    private void Start()
    {
        // GameObject'e AudioSource bileþeni ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Kaydet()
    {
        // Ses efekti çal
        PlayButtonClickSound();

        // Doðum tarihini oluþtur
        string dogumTarihi = gunField.text + "/" + ayField.text + "/" + yilField.text;

        // Hasta bilgilerini doldur
        HastaBilgi yeniHasta = new HastaBilgi
        {
            hastaAdi = hastaAdiField.text,
            hastaSoyadi = hastaSoyadiField.text,
            telNo = telNoField.text,
            tcNo = tcNoField.text,
            dogumTarihi = dogumTarihi,
            hastalikTuru = hastalikTuruDropdown.options[hastalikTuruDropdown.value].text,
            hastalikAciklamasi = hastalikAciklamasiField.text,
            boy = boyField.text,
            kilo = kiloField.text,
            teknisyen = teknisyenField.text
        };

        // JSON formatýnda dosya oluþtur
        string json = JsonUtility.ToJson(yeniHasta);
        string filePath = Application.persistentDataPath + "/hasta_" + yeniHasta.tcNo + ".json";
        File.WriteAllText(filePath, json);

        Debug.Log("Hasta bilgileri kaydedildi: " + filePath);

        // Kayýt iþleminden sonra "grs" sahnesine yönlendirme
        SceneManager.LoadScene("grs");
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi çal
        }
    }
}
