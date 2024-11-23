using UnityEngine;
using UnityEngine.UI;

public class Rapordetay : MonoBehaviour
{
    public Text oyunAdiText;
    public Text tarihText;
    public Text skorText;
    public Text maxKolAciText;
    public Text kullanilanKolText;
    public Text baslikText; // Yeni ba�l�k Text referans�

    private void Start()
    {
        var secilenOyun = SecilenOyunVerisi.SecilenOyun; // Se�ilen oyunun bilgisi

        if (secilenOyun != null)
        {
            // Oyun detaylar�n� g�ster
            oyunAdiText.text = "Oyun Ad�: " + secilenOyun.oyunAdi;
            tarihText.text = "Tarih: " + secilenOyun.oynamaTarihi;
            skorText.text = "Skor: " + secilenOyun.skor.ToString();
            maxKolAciText.text = "Max Kol A��s�: " + secilenOyun.maxKolAci.ToString("F2") + " derece";
            kullanilanKolText.text = "Kullan�lan Kol: " + secilenOyun.kullanilanKol;

            // Oyun ad�na g�re ba�l��� ayarla
            if (secilenOyun.oyunAdi == "Ponk")
            {
                baslikText.text = "OMUZ ABD�KS�YON DE�ERLEND�RMES�";
            }
            else 
            {
                baslikText.text = "OMUZ HOR�ZONTAL DE�ERLEND�RMES�";
            }
        }
        else
        {
            Debug.LogError("Se�ilen oyun bilgisi bulunamad�.");
        }
    }
}
