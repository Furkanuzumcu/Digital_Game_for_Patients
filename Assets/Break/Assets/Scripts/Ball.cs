using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public float speed = 7f;
    public float minVerticalVelocity = 1f;  // Minimum dikey hız bileşenini artırdık
    public float minHorizontalVelocity = 1f; // Minimum yatay hız bileşeni de ekledik

    // Ses efektleri
    public AudioClip paddleHitSound;
    public AudioClip wallHitSound;
    public AudioClip brickHitSound;

    private bool isUserDetected = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitForUserDetection());
    }

    private IEnumerator WaitForUserDetection()
    {
        while (!isUserDetected)
        {
            isUserDetected = KinectManager.Instance && KinectManager.Instance.IsUserDetected();
            yield return null;
        }
        ResetBall();
    }

    public void ResetBall()
    {
        
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        CancelInvoke();
        Invoke(nameof(SetRandomTrajectory), 2f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = new Vector2(Random.Range(-1f, 1f), -1f);
        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (!isUserDetected)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Brick"))
        {
            PlaySound(collision.gameObject.CompareTag("Paddle") ? paddleHitSound :
                      collision.gameObject.CompareTag("Wall") ? wallHitSound : brickHitSound);

            // Yatay ve dikey hız bileşenlerini kontrol et
            if (Mathf.Abs(rb.velocity.y) < minVerticalVelocity)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) *minVerticalVelocity+1f);
            }

            if (Mathf.Abs(rb.velocity.x) < minHorizontalVelocity)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * minHorizontalVelocity, rb.velocity.y+1f);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
