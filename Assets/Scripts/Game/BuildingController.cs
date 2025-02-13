using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private Collider2D _collider;

    [SerializeField]
    private Collider2D _entranceCollider;
    [SerializeField]
    private InteractableController buildingInteraction;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void OnPlayerColliderEnter(CityPlayerController player, Collider2D collision)
    {
        if (collision == _collider)
            _sprite.enabled = false;

        if (collision == _entranceCollider)
            player.Interactions.Add(buildingInteraction);
    }

    public void OnPlayerColliderExit(CityPlayerController player, Collider2D collider)
    {
        if (collider == _collider)
            _sprite.enabled = true;

        if (collider == _entranceCollider)
            player.Interactions.Remove(buildingInteraction);
    }
}
