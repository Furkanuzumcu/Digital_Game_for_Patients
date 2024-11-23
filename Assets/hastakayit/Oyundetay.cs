using UnityEngine;
using UnityEngine.UI;

public class Rapordetay : MonoBehaviour
{
    public Text oyunAdiText;
    public Text tarihText;
    public Text skorText;
    public Text maxKolAciText;
    public Text kullanilanKolText;
    public Text baslikText; // Yeni baþlýk Text referansý

    private void Start()
    {
        var secilenOyun = SecilenOyunVerisi.SecilenOyun; // Seçilen oyunun bilgisi

        if (secilenOyun != null)
        {
            // Oyun detaylarýný göster
            oyunAdiText.text = "Oyun Adý: " + secilenOyun.oyunAdi;
            tarihText.text = "Tarih: " + secilenOyun.oynamaTarihi;
            skorText.text = "Skor: " + secilenOyun.skor.ToString();
            maxKolAciText.text = "Max Kol Açýsý: " + secilenOyun.maxKolAci.ToString("F2") + " derece";
            kullanilanKolText.text = "Kullanýlan Kol: " + secilenOyun.kullanilanKol;

            // Oyun adýna göre baþlýðý ayarla
            if (secilenOyun.oyunAdi == "Ponk")
            {
                baslikText.text = "OMUZ ABDÜKSÝYON DEÐERLENDÝRMESÝ";
            }
            else 
            {
                baslikText.text = "OMUZ HORÝZONTAL DEÐERLENDÝRMESÝ";
            }
        }
        else
        {
            Debug.LogError("Seçilen oyun bilgisi bulunamadý.");
        }
    }
}
