using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{   
    [SerializeField]
    private Animator transition;

    public IEnumerator LoadScene(string sceneName)
    {
        // Start fading animation
        transition.SetTrigger("Start");
        // Wait a bit
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}
