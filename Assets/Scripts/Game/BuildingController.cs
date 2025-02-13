using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class BuildingController : MonoBehaviour
{
    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _sprite.enabled = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _sprite.enabled = true;
    }
}
