using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SecilenOyunVerisi
{
    public static OyunRaporu SecilenOyun; // Týklanan oyunun bilgisi burada saklanacak.
}

public class OyunRaporuListele : MonoBehaviour
{
    public GameObject oyunButonPrefab;  // Prefab olarak oluþturduðun buton
    public Transform oyunListContainer; // Butonlarýn yerleþtirileceði alan (Scroll View'deki Content)

    private List<OyunRaporu> oyunRaporlari = new List<OyunRaporu>();

    private void Start()
    {
        // Dosya yolunu belirle
        string hastaAdi = PlayerPrefs.GetString("hastaAdi"); // Kullanýcý adý bilgisi
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi"); // Kullanýcý soyadý bilgisi
        string filePath = Application.persistentDataPath + "/rapor_" + hastaAdi + "_" + hastaSoyadi + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            OyunRaporuListesi raporListesi = JsonUtility.FromJson<OyunRaporuListesi>(json);
            oyunRaporlari = raporListesi.results;

            // Debug ile yüklenen oyun sayýsýný kontrol et
            Debug.Log("Yüklenen oyun sayýsý: " + oyunRaporlari.Count);

            // Oyunlarý listele
            ListeyiOlustur();
        }
        else
        {
            Debug.LogWarning("Oyun kayýt dosyasý bulunamadý.");
        }
    }

    private void ListeyiOlustur()
    {
        foreach (var oyun in oyunRaporlari)
        {
            GameObject yeniButon = Instantiate(oyunButonPrefab, oyunListContainer);
            Text buttonText = yeniButon.GetComponentInChildren<Text>();

            if (buttonText != null)
            {
                buttonText.text = oyun.oyunAdi + " - " + oyun.oynamaTarihi;
            }

            yeniButon.GetComponent<Button>().onClick.AddListener(() => OyunDetaylariniGoster(oyun));
            yeniButon.transform.SetParent(oyunListContainer, false);
        }
    }

    private void OyunDetaylariniGoster(OyunRaporu oyun)
    {
        SecilenOyunVerisi.SecilenOyun = oyun; // Seçilen oyunun bilgilerini aktar.
        SceneManager.LoadScene("raporlama"); // Raporlama sahnesine yönlendir.
    }
}

[System.Serializable]
public class OyunRaporu
{
    public string oyunAdi;
    public string oynamaTarihi;
    public int skor;
    public float maxKolAci;
    public string kullanilanKol;
}

[System.Serializable]
public class OyunRaporuListesi
{
    public List<OyunRaporu> results = new List<OyunRaporu>();
}
