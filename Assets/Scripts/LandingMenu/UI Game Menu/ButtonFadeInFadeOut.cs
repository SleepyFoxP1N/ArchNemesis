using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonFadeInFadeOut : MonoBehaviour
{
    [SerializeField] string GoToScene;
    public GameObject ToActivate;
    public GameObject ToDeactivate;

    public Button button;

    void Start()
    {
        button.onClick.AddListener(PlayAnimation);
    }

    void PlayAnimation()
    {
        if(GoToScene == null)
        {
            // Play Button SFX
            AudioManager.Instance.PlaySFX("Main Menu Button Click");
        }


        // Start a coroutine to wait for 2 seconds before activating/deactivating GameObjects
        StartCoroutine(ActivateDeactivateAfterDelay());

        // Perform additional action when the button is clicked
        Debug.Log("Button clicked!");
    }

    IEnumerator ActivateDeactivateAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        if( ToActivate != null ) {
            ToActivate.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(GoToScene);
        }

        if (ToDeactivate != null)
        {
            ToDeactivate.SetActive(false);
        }
    }
}
