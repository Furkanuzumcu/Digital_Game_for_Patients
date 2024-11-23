using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SecilenOyunVerisi
{
    public static OyunRaporu SecilenOyun; // T�klanan oyunun bilgisi burada saklanacak.
}

public class OyunRaporuListele : MonoBehaviour
{
    public GameObject oyunButonPrefab;  // Prefab olarak olu�turdu�un buton
    public Transform oyunListContainer; // Butonlar�n yerle�tirilece�i alan (Scroll View'deki Content)

    private List<OyunRaporu> oyunRaporlari = new List<OyunRaporu>();

    private void Start()
    {
        // Dosya yolunu belirle
        string hastaAdi = PlayerPrefs.GetString("hastaAdi"); // Kullan�c� ad� bilgisi
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi"); // Kullan�c� soyad� bilgisi
        string filePath = Application.persistentDataPath + "/rapor_" + hastaAdi + "_" + hastaSoyadi + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            OyunRaporuListesi raporListesi = JsonUtility.FromJson<OyunRaporuListesi>(json);
            oyunRaporlari = raporListesi.results;

            // Debug ile y�klenen oyun say�s�n� kontrol et
            Debug.Log("Y�klenen oyun say�s�: " + oyunRaporlari.Count);

            // Oyunlar� listele
            ListeyiOlustur();
        }
        else
        {
            Debug.LogWarning("Oyun kay�t dosyas� bulunamad�.");
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
        SecilenOyunVerisi.SecilenOyun = oyun; // Se�ilen oyunun bilgilerini aktar.
        SceneManager.LoadScene("raporlama"); // Raporlama sahnesine y�nlendir.
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
