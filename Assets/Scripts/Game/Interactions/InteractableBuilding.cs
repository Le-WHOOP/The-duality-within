using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableBuilding : InteractableController
{
    [SerializeField]
    private BuildingInterior _interior;

    public event EventHandler<CityPlayerController> OnEntered;
    public event EventHandler<CityPlayerController> OnExited;

    public InteractableBuilding() : base(Personnality.Both) { }

    public override void Interact(CityPlayerController player)
    {
        _interior.Enter(this, player);
        OnEntered?.Invoke(this, player);
    }

    public void Exit(CityPlayerController player)
    {
        player.transform.position = transform.position;
        OnExited?.Invoke(this, player);
    }
}
