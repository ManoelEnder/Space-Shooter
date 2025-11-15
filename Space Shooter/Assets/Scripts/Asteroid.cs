using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speedMin = 4f;
    public float speedMax = 7f;
    public float lifeTime = 10f;

    public AudioClip explodeSound;      
    AudioSource audioSource;            

    Rigidbody2D rb;
    int hp = 1;
    bool hasInit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (rb == null)
        {
            Debug.LogError("Asteroid: Rigidbody2D faltando!");
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);

        if (!hasInit)
        {
            Vector2 fallback = Vector2.down;
            float s = Random.Range(speedMin, speedMax);
            rb.linearVelocity = fallback * s;
        }
    }

    public void Initialize(Vector2 direction, float size = 1f, float speedOverride = -1f)
    {
        hasInit = true;
        transform.localScale = Vector3.one * size;
        hp = Mathf.Max(1, Mathf.RoundToInt(size));

        float s = speedOverride > 0 ? speedOverride : Random.Range(speedMin, speedMax);

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * s;
            rb.angularVelocity = Random.Range(-180f, 180f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(1);

            Die();
        }

        if (other.CompareTag("Bullet"))
        {
            hp--;
            Destroy(other.gameObject);

            if (hp <= 0)
            {
                GameManager.Instance?.AddScore(10);
                Die();
            }
        }
    }

    public void Die()
    {
        if (explodeSound != null)
        {
            AudioSource.PlayClipAtPoint(explodeSound, Camera.main != null ? Camera.main.transform.position : transform.position);
        }

        ScreenShake.Instance?.Shake(0.15f, 0.1f);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        if (rb != null) rb.linearVelocity = Vector2.zero;

        Destroy(gameObject, 0.1f); 
    }
}
