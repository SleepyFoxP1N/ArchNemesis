using UnityEngine;
using System.Collections;

public class QuitApplication : MonoBehaviour
{
    // Example usage: Call this method when a button is clicked or any other condition is met
    public void QuitWithDelay()
    {
        StartCoroutine(QuitAfterDelay());
    }

    IEnumerator QuitAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Quit the application
        Application.Quit();
    }
}
