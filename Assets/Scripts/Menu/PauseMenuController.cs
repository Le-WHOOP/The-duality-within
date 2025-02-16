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

    // Update is called once per frame
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

    public void Resume() 
    {
        StartCoroutine(MenuAnimation(transitionPause));

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Confirmation(bool active) 
    {
        if (!active)
        {
            StartCoroutine(MenuAnimation(transitionConfirmation));
        }

        confirmationMenu.SetActive(active);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(fadeScene.LoadScene("MenuScene"));
    }

    public IEnumerator MenuAnimation(Animator transition)
    {
        // Start animation
        transition.SetTrigger("Start");
        // Wait a bit
        yield return new WaitForSeconds(1);
    }
}
