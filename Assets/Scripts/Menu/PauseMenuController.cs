using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;

    [Header("Menus")]
    [SerializeField]
    private GameObject confirmationMenu;

    [SerializeField]
    private GameObject pauseMenu;

    [Header("Scene")]
    [SerializeField]
    private FadeScene fadeScene;

    [Header("Animations")]
    [SerializeField]
    private Animator transitionConfirmation;
    [SerializeField]
    private Animator transitionPause;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    /// <summary>
    /// Resume the game
    /// </summary>
    /// <remarks>
    /// It has only one line because a button cannot have 
    /// an onclick function that returns an IEnumerator
    /// </remarks>
    public void Resume() 
    {
        StartCoroutine(PauseMenuAnimation());
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void Pause()
    {
        pauseMenu.SetActive(true);

        // Time.timeScale is a function that changes the time of the game
        // Time.timeScale = 0f => the game is paused and the player cannot play when the 
        // pause menu is active
        Time.timeScale = 0f;
        isPaused = true;
    }

    /// <summary>
    /// Shows the confirmation menu
    /// </summary>
    /// <remarks>
    /// It has only one line because a button cannot have 
    /// an onclick function that returns an IEnumerator
    /// </remarks>
    /// <param name="active">The boolean that dictates if the menu should be active or not</param>
    public void Confirmation(bool active) 
    {
        StartCoroutine(ConfirmationCoroutine(active));
    }

    /// <summary>
    /// Shows the confirmation menu
    /// </summary>
    /// <remarks>
    /// The time of the game is scaled back to normal BEFORE the coroutine because
    /// the WaitForSeconds function waits game time seconds
    /// </remarks>
    public void QuitGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(fadeScene.LoadScene("MenuScene"));
    }

    /// <summary>
    /// Handles the pause menu animation
    /// </summary>
    /// <remarks>
    /// The time of the game is scaled back to normal BEFORE the coroutine because
    /// the WaitForSeconds function waits game time seconds
    /// Also, the block handling the animation is copy pasted into the function
    /// ConfirmationCoroutine and not in a function because calling 
    /// StartCoroutine() doesn't stop the rest of the code
    /// </remarks>
    public IEnumerator PauseMenuAnimation()
    {
        Time.timeScale = 1f;
        isPaused = false;
        
        // Start animation
        transitionPause.SetTrigger("Start");
        // Wait a bit
        yield return new WaitForSeconds(0.25f);

        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Handles the confirmation menu animation
    /// </summary>
    /// <remarks>
    /// The time of the game is scaled back to normal BEFORE the coroutine because
    /// the WaitForSeconds function waits game time seconds
    /// Also, the block handling the animation is copy pasted into the function
    /// ConfirmationCoroutine and not in a function because calling 
    /// StartCoroutine() doesn't stop the rest of the code
    /// </remarks>
    /// <param name="active">The boolean that dictates if the menu should be active or not</param>
    public IEnumerator ConfirmationCoroutine(bool active)
    {
        Time.timeScale = 1f;
        if (!active)
        {
            // Start animation
            transitionConfirmation.SetTrigger("Start");
            // Wait a bit
            yield return new WaitForSeconds(0.25f);
        }

        confirmationMenu.SetActive(active);
        Time.timeScale = 0f;
    }
}
