using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip buttonClickSound; // Buton sesi

    public float soundDelay = 0.1f; // Sesin tamamlanmadan hemen fonksiyonu çalýþtýrmamasý için gecikme

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Ses çal ve ardýndan butonun iþlevini çalýþtýr
    public void PlaySoundAndExecute(System.Action functionToExecute)
    {
        StartCoroutine(PlaySoundAndRunFunction(functionToExecute));
    }

    private IEnumerator PlaySoundAndRunFunction(System.Action functionToExecute)
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
            yield return new WaitForSeconds(soundDelay);
        }
        functionToExecute?.Invoke(); // Fonksiyonu çalýþtýr
    }
}
