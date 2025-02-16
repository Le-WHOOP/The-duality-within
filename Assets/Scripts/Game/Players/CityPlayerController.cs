using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityPlayerController : PlayerController
{
    private Personnality _personnality;

    private GameObject _cityJekyll;
    private Animator _animatorJekyll;
    private GameObject _cityHyde;
    private Animator _animatorHyde;

    private Animator _animator;

    public List<InteractableController> Interactions { get; } = new List<InteractableController>();

    protected override void Start()
    {
        base.Start();

        _cityJekyll = transform.Find("CityJekyll").gameObject;
        _animatorJekyll = _cityJekyll.GetComponent<Animator>();

        _cityHyde = transform.Find("CityHyde").gameObject;
        _animatorHyde = _cityHyde.GetComponent<Animator>();

        DisplayActivePlayer();
    }

    void DisplayActivePlayer()
    {
        if (_personnality == Personnality.Hyde)
        {
            _cityHyde.SetActive(true);
            _cityJekyll.SetActive(false);
            _animator = _animatorHyde;
        }
        else
        {
            _cityJekyll.SetActive(true);
            _cityHyde.SetActive(false);
            _animator = _animatorJekyll;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out InteractableController interaction)
            // This interaction can only be used if available to the current personnality controlling the player
            && interaction.IsAvailableTo(_personnality))
            Interactions.Add(interaction);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out InteractableController interaction))
            Interactions.Remove(interaction);
    }

    public override void Move(float x, float y)
    {
        base.Move(x, y);

        // The player is trying to walk do set their new direction
        if (x != 0 || y != 0)
        {
            _animator.SetFloat("x", x);
            _animator.SetFloat("y", y);
            _animator.SetBool("isWalking", true);
        }
        // If x and y are 0: the player transitions to idle BUT still needs to face their original direction
        // If we put x and y at 0 then the player is always idling facing down
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    /// <summary>
    /// Attempts to interact with a nearby interactable object, if one is within range.
    /// </summary>
    public void Interact()
    {
        // DEBUG
        SwapPersonnality();
        return;

        // Since Unity overrides the null operator, we can't use the null propagation (? operator)
        InteractableController interaction = Interactions.FirstOrDefault();
        if (interaction != null)
            interaction.Interact(this);
    }

    public void SwapPersonnality()
    {
        _personnality = _personnality == Personnality.Hyde ? Personnality.Jekyll : Personnality.Hyde;
        DisplayActivePlayer();

        // Remove all interactions that are no longer accessible because of the personnality change
        // TODO If the player is already inside the trigger of an interaction that was for the other player when the
        // personnality swaps, this interaction will not be added
        foreach (InteractableController interaction in Interactions.Where(interaction => !interaction.IsAvailableTo(_personnality)))
            Interactions.Remove(interaction);
    }
}
