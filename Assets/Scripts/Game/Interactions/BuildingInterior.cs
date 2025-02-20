using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BuildingInterior : InteractableController
{
    // Ask 22 if you don't know how this works
    [SerializeField]
    private InteractableBuilding _entrance;

    public BuildingInterior() : base(Personnality.Both) { }

    public override void Interact(CityPlayerController player)
    {
        base.Interact(player);
        _entrance.Exit(player);
    }

    public void Enter(InteractableBuilding entrance, CityPlayerController player)
    {
        _entrance = entrance;
        player.transform.position = transform.position;
    }
}
