using UnityEngine;
using System.Collections;

public class WallBounce : MonoBehaviour
{
    public float minVerticalAngle = 0.2f;  // Çok yatay çarpýþmalarda dikey bileþen için minimum deðer
    private bool canBounce = true;         // Çarpýþmayý engellemek için cooldown

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && canBounce)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Gelen açýnýn tersiyle yansýtma
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);

                // Eðer çok yatay bir hýz varsa, dikey bileþen ekle
                if (Mathf.Abs(rb.velocity.y) < minVerticalAngle)
                {
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * minVerticalAngle);
                }

                // Cooldown baþlat, tekrar çarpmasýný engelle
                canBounce = false;
                StartCoroutine(ResetBounceCooldown());
            }
        }
    }

    // Cooldown sonrasý çarpýþmayý tekrar etkinleþtir
    private IEnumerator ResetBounceCooldown()
    {
        yield return new WaitForSeconds(0.1f); // Cooldown süresi
        canBounce = true;
    }
}
