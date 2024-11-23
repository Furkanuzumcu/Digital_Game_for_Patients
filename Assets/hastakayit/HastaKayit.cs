using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahne y�netimi i�in gerekli

public class HastaKayit : MonoBehaviour
{
    public InputField hastaAdiField;
    public InputField hastaSoyadiField;
    public InputField telNoField;
    public InputField tcNoField;
    public InputField gunField;  // G�n InputField
    public InputField ayField;   // Ay InputField
    public InputField yilField;  // Y�l InputField
    public Dropdown hastalikTuruDropdown;
    public InputField hastalikAciklamasiField;
    public InputField boyField;
    public InputField kiloField;
    public InputField teknisyenField;

    public AudioClip buttonClickSound;  // Ses dosyas� i�in alan
    private AudioSource audioSource;    // AudioSource referans�

    private void Start()
    {
        // GameObject'e AudioSource bile�eni ekle
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Kaydet()
    {
        // Ses efekti �al
        PlayButtonClickSound();

        // Do�um tarihini olu�tur
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

        // JSON format�nda dosya olu�tur
        string json = JsonUtility.ToJson(yeniHasta);
        string filePath = Application.persistentDataPath + "/hasta_" + yeniHasta.tcNo + ".json";
        File.WriteAllText(filePath, json);

        Debug.Log("Hasta bilgileri kaydedildi: " + filePath);

        // Kay�t i�leminden sonra "grs" sahnesine y�nlendirme
        SceneManager.LoadScene("grs");
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Sesi �al
        }
    }
}
