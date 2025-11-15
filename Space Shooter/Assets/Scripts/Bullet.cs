using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float life = 3f;

    void Start()
    {
        Destroy(gameObject, life);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Asteroid a = other.GetComponent<Asteroid>();

            if (a != null)
            {
                GameManager.Instance.AddScore(10);
                a.Die();
            }

            Destroy(gameObject);
        }
    }
}
