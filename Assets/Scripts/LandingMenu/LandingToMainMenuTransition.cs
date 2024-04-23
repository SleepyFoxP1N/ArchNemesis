using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LandingToMainMenuTransition : MonoBehaviour
{
    // Button on Click that will perform
    public Button BGTransition;

    public GameObject LandingMainMenu;  // Disable The LandingMainMenu To Clear
    public GameObject GameMainMenu;     // Load The Actual GameMainMenu

    void Start()
    {
        // Attach a listener to the button's onClick event
        BGTransition.onClick.AddListener(PerformTransition);
    }

    // Method to handle the transition
    void PerformTransition()
    {
        // Start the animation coroutine
        StartCoroutine(TransitionDelay());
    }

    IEnumerator TransitionDelay()
    {

        // Wait for about 5 seconds
        yield return new WaitForSeconds(2f);

        BGTransition.gameObject.SetActive(false);
        LandingMainMenu.SetActive(false);
        GameMainMenu.SetActive(true);
    }
}
