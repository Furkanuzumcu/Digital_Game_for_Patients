using UnityEngine;

public class ComputerPaddle : MonoBehaviour
{
    public float speed = 5f; // Paddle'�n hareket h�z�
    public Rigidbody2D ball; // Topun Rigidbody2D bile�eni
    public float followDistance = 5f; // Paddle'�n topu takip edece�i maksimum mesafe

    private Vector2 startPosition; // Paddle'�n ba�lang�� pozisyonu

    void Start()
    {
        // Paddle'�n ba�lang�� pozisyonunu ayarlay�n
        transform.position = new Vector3(9.23f, -1.74f, 316f); // Paddle'�n ba�lang�� pozisyonu

        // Mevcut pozisyonu kaydedin
        startPosition = transform.position;
    }

    void Update()
    {
        // Top ile paddle aras�ndaki mesafeyi hesaplay�n
        float distanceToBall = Vector2.Distance(ball.position, transform.position);

        // Top belirlenen mesafe i�inde oldu�unda paddle'� hareket ettir
        if (distanceToBall <= followDistance)
        {
            if (ball.position.y > transform.position.y)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else if (ball.position.y < transform.position.y)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }

            // Paddle'�n yukar� ve a�a��ya s�n�rs�z hareket etmemesi i�in pozisyonunu s�n�rland�r�n
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y, -15.5f, 12.5f), // Y eksenindeki s�n�rlar� ayarlay�n
                transform.position.z
            );
        }
    }
}
