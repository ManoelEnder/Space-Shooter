using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speedMin = 4f;
    public float speedMax = 7f;
    public float lifeTime = 10f;

    Rigidbody2D rb;
    int hp = 1;
    bool hasInit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Asteroid: Rigidbody2D faltando no prefab! Adicione Rigidbody2D.");
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
        float s = speedOverride > 0f ? speedOverride : Random.Range(speedMin, speedMax);
        if (rb == null) rb = GetComponent<Rigidbody2D>();
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
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Bullet"))
        {
            hp--;
            Destroy(other.gameObject);
            if (hp <= 0)
            {
                if (GameManager.Instance != null) GameManager.Instance.AddScore(10);
                Destroy(gameObject);
            }
        }
    }
}
