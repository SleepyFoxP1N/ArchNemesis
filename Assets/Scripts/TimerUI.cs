using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class TimerUI : MonoBehaviourPunCallbacks
{ 
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject leaderboard;
    [SerializeField] GameObject MainmenuButton;
    public float timeRemaining = 600; // 10 minutes in seconds

    public IEnumerator timer(GameObject player)
    {
        timerText = player.transform.Find("Main Camera/Canvas/Timer/Time Text").gameObject.GetComponent<TextMeshProUGUI>();
        while (timeRemaining > 0 && PhotonNetwork.IsMasterClient)
        {
            yield return new WaitForSecondsRealtime(1f);

            timeRemaining -= 1f;
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            photonView.RPC("SyncTimer", RpcTarget.AllBuffered, minutes, seconds); 
        }
    }

    [PunRPC]
    void SyncTimer(int minutes, int seconds)
    {
        if (minutes <= 0 && seconds <= 0)
        {
            leaderboard.SetActive(true);
            MainmenuButton.SetActive(true);
        }
        else timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
