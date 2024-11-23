using UnityEngine;

public class RaporlamaAvatarKontrol : MonoBehaviour
{
    public Transform avatarSagKol;  // Sað kol referansý
    public Transform avatarSolKol;  // Sol kol referansý

    private void Start()
    {
        var secilenOyun = SecilenOyunVerisi.SecilenOyun; // Seçilen oyunun bilgisi

        if (secilenOyun != null)
        {
            int maxKolAci = Mathf.RoundToInt(secilenOyun.maxKolAci); // Açýyý int'e dönüþtür
            string kolSecimi = secilenOyun.kullanilanKol;

            Debug.Log($"Max Kol Açýsý: {maxKolAci}, Kullanýlan Kol: {kolSecimi}");

            if (kolSecimi == "Sað Kol")
            {
                avatarSagKol.localRotation = Quaternion.Euler(0, -maxKolAci+110, 0);
                avatarSolKol.localRotation = Quaternion.Euler(0, -90, 0);
                Debug.Log("Sað Kol hareket ettirildi.");
            }
            else if (kolSecimi == "Sol Kol")
            {
                avatarSolKol.localRotation = Quaternion.Euler(0, maxKolAci-110, 0);
                avatarSagKol.localRotation = Quaternion.Euler(0, +90, 0);
                Debug.Log("Sol Kol hareket ettirildi.");
            }
            else
            {
                Debug.LogWarning("Geçersiz kol seçimi: " + kolSecimi);
            }
        }
        else
        {
            Debug.LogError("Seçilen oyun bilgisi bulunamadý.");
        }
    }
}
