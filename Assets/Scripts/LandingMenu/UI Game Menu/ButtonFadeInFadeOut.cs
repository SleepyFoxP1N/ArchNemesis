using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFadeInFadeOut : MonoBehaviour
{
    public GameObject ToActivate;
    public GameObject ToDeactivate;

    public Button button;

    void Start()
    {
        button.onClick.AddListener(PlayAnimation);
    }

    void PlayAnimation()
    {
        // Play Button SFX
        AudioManager.Instance.PlaySFX("Main Menu Button Click");

        // Start a coroutine to wait for 2 seconds before activating/deactivating GameObjects
        StartCoroutine(ActivateDeactivateAfterDelay());

        // Perform additional action when the button is clicked
        Debug.Log("Button clicked!");
    }

    IEnumerator ActivateDeactivateAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        ToActivate.SetActive(true);


        ToDeactivate.SetActive(false);
    }
}
