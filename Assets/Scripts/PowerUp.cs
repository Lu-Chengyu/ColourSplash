using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        RedCoin,
        BlueCoin,
        GreenCoin,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.RedCoin:
                GameManager.Instance.UpdateCounter("Red", 1); // Increase counter
                break;

            case Type.GreenCoin:
                GameManager.Instance.UpdateCounter("Green", 1); // Increase counter
                break;

            case Type.BlueCoin:
                GameManager.Instance.UpdateCounter("Blue", 1); // Increase counter
                break;
        }

        Destroy(gameObject);
    }

}
