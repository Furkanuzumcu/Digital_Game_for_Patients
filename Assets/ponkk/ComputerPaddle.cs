using UnityEngine;

public class ComputerPaddle : MonoBehaviour
{
    public float speed = 5f; // Paddle'ýn hareket hýzý
    public Rigidbody2D ball; // Topun Rigidbody2D bileþeni
    public float followDistance = 5f; // Paddle'ýn topu takip edeceði maksimum mesafe

    private Vector2 startPosition; // Paddle'ýn baþlangýç pozisyonu

    void Start()
    {
        // Paddle'ýn baþlangýç pozisyonunu ayarlayýn
        transform.position = new Vector3(9.23f, -1.74f, 316f); // Paddle'ýn baþlangýç pozisyonu

        // Mevcut pozisyonu kaydedin
        startPosition = transform.position;
    }

    void Update()
    {
        // Top ile paddle arasýndaki mesafeyi hesaplayýn
        float distanceToBall = Vector2.Distance(ball.position, transform.position);

        // Top belirlenen mesafe içinde olduðunda paddle'ý hareket ettir
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

            // Paddle'ýn yukarý ve aþaðýya sýnýrsýz hareket etmemesi için pozisyonunu sýnýrlandýrýn
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y, -15.5f, 12.5f), // Y eksenindeki sýnýrlarý ayarlayýn
                transform.position.z
            );
        }
    }
}
