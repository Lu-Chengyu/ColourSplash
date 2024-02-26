using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color spriteColor = spriteRenderer.color;
            GameManager.Instance.UpdateCounter(spriteColor);
        }
        Destroy(gameObject);
    }
}
