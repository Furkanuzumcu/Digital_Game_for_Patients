using UnityEngine;

public class RaporlamaAvatarKontrol : MonoBehaviour
{
    public Transform avatarSagKol;  // Sa� kol referans�
    public Transform avatarSolKol;  // Sol kol referans�

    private void Start()
    {
        var secilenOyun = SecilenOyunVerisi.SecilenOyun; // Se�ilen oyunun bilgisi

        if (secilenOyun != null)
        {
            int maxKolAci = Mathf.RoundToInt(secilenOyun.maxKolAci); // A��y� int'e d�n��t�r
            string kolSecimi = secilenOyun.kullanilanKol;

            Debug.Log($"Max Kol A��s�: {maxKolAci}, Kullan�lan Kol: {kolSecimi}");

            if (kolSecimi == "Sa� Kol")
            {
                avatarSagKol.localRotation = Quaternion.Euler(0, -maxKolAci+110, 0);
                avatarSolKol.localRotation = Quaternion.Euler(0, -90, 0);
                Debug.Log("Sa� Kol hareket ettirildi.");
            }
            else if (kolSecimi == "Sol Kol")
            {
                avatarSolKol.localRotation = Quaternion.Euler(0, maxKolAci-110, 0);
                avatarSagKol.localRotation = Quaternion.Euler(0, +90, 0);
                Debug.Log("Sol Kol hareket ettirildi.");
            }
            else
            {
                Debug.LogWarning("Ge�ersiz kol se�imi: " + kolSecimi);
            }
        }
        else
        {
            Debug.LogError("Se�ilen oyun bilgisi bulunamad�.");
        }
    }
}
