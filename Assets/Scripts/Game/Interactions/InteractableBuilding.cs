using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableBuilding : InteractableController
{
    [SerializeField]
    private BuildingInterior _interior;

    public InteractableBuilding() : base(Personnality.Both) { }

    public override void Interact(CityPlayerController player)
    {
        _interior.Enter(this, player);
    }

    public void Exit(CityPlayerController player)
    {
        player.transform.position = transform.position;
    }
}
