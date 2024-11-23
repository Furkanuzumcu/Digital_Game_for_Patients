using UnityEngine;
using System.Collections;

public class WallBounce : MonoBehaviour
{
    public float minVerticalAngle = 0.2f;  // �ok yatay �arp��malarda dikey bile�en i�in minimum de�er
    private bool canBounce = true;         // �arp��may� engellemek i�in cooldown

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && canBounce)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Gelen a��n�n tersiyle yans�tma
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);

                // E�er �ok yatay bir h�z varsa, dikey bile�en ekle
                if (Mathf.Abs(rb.velocity.y) < minVerticalAngle)
                {
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * minVerticalAngle);
                }

                // Cooldown ba�lat, tekrar �arpmas�n� engelle
                canBounce = false;
                StartCoroutine(ResetBounceCooldown());
            }
        }
    }

    // Cooldown sonras� �arp��may� tekrar etkinle�tir
    private IEnumerator ResetBounceCooldown()
    {
        yield return new WaitForSeconds(0.1f); // Cooldown s�resi
        canBounce = true;
    }
}
