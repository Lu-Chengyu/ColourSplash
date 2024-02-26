using UnityEngine;

public class Box : MonoBehaviour
{
    public Color color;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool CanInteract(Color playerColor)
    {
        return color == playerColor;
    }

    public void Move(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }
}