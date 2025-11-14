using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public float minX = -8f;
    public float maxX = 8f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hor = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) hor = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) hor = 1f;
        }

        if (Gamepad.current != null)
        {
            float g = Gamepad.current.leftStick.ReadValue().x;
            if (Mathf.Abs(g) > 0.1f) hor = g;
        }

        Vector2 vel = new Vector2(hor, 0f).normalized * speed;
        rb.linearVelocity = vel;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}
