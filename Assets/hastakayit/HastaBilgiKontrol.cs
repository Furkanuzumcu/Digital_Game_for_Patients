using UnityEngine;
using UnityEngine.UI;

public class HastaBilgiKontrol : MonoBehaviour
{
    public Text hastaBilgiText; // Kullanýcý adýný yazdýracaðýmýz Text component

    void Start()
    {
        // PlayerPrefs'ten hasta adýný ve soyadýný al
        string hastaAdi = PlayerPrefs.GetString("hastaAdi", "Bilinmiyor");
        string hastaSoyadi = PlayerPrefs.GetString("hastaSoyadi", "Bilinmiyor");

        // Hasta ismini Text componentine yazdýr
        hastaBilgiText.text = "Hoþgeldin " + hastaAdi + " " + hastaSoyadi;
    }
}
