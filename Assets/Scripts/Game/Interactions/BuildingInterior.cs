using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BuildingInterior : InteractableController
{
    private InteractableBuilding _entrance;

    public BuildingInterior() : base(Personnality.Both) { }

    public override void Interact(CityPlayerController player)
    {
        _entrance.Exit(player);
    }

    public void Enter(InteractableBuilding entrance, CityPlayerController player)
    {
        _entrance = entrance;
        player.transform.position = transform.position;
    }
}
