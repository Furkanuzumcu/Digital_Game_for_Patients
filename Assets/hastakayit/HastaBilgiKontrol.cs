using UnityEngine;
using UnityEngine.UI;

public class HastaBilgiKontrol : MonoBehaviour
{
    public Text hastaBilgiText; // Kullan�c� ad�n� yazd�raca��m�z Text component

    void Start()
    {
        // PlayerPrefs'ten hasta ad�n� ve soyad�n� al
        string hastaAdi = PlayerPrefs.GetString("hastaAdi", "Bilinmiyor");
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi", "Bilinmiyor");

        // Hasta ismini Text componentine yazd�r
        hastaBilgiText.text = "Ho�geldin " + hastaAdi + " " + hastaSoyadi;
    }
}
