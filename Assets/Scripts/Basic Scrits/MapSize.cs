
using UnityEngine;

public class MapSize : MonoBehaviour
{
    [SerializeField] public Vector2 mapSize = new Vector2(30, 30);
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = mapSize;
    }

}
