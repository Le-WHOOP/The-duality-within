using System;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public event EventHandler<Collider2D> OnTriggerEntered;
    public event EventHandler<Collider2D> OnTriggerExited;

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerEntered?.Invoke(this,collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        OnTriggerExited?.Invoke(this, collider);
    }
}
