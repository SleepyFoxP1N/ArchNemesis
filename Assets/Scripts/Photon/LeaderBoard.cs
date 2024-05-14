using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using Photon.Pun.UtilityScripts;

public class LeaderBoard : MonoBehaviour
{


    [SerializeField] GameObject playersHolder;
    [SerializeField] TimerUI timer;


    [Header("Options")]
    [SerializeField] float refreshRate = 1f;


    [Header("UI")]
    [SerializeField] GameObject[] slots;


    [Space]
    [SerializeField] TextMeshProUGUI[] name_Text;
    [SerializeField] TextMeshProUGUI[] score_Text;
    [SerializeField] TextMeshProUGUI[] killDeath_Text;


    private void Start()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshRate);
    }

    public void Refresh()
    {
        foreach(var slot in slots)
        {
            slot.SetActive(false);
        }

        var sortedPlayerList = 
            (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        foreach (var player in sortedPlayerList)
        {
            slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "unnamed";

            name_Text[i].text = player.NickName;
            score_Text[i].text = player.GetScore().ToString();

            if (player.CustomProperties["Kills"] != null)
            {
                killDeath_Text[i].text = player.CustomProperties["Kills"] + "/" + player.CustomProperties["Deaths"];
            }
            else
            {
                killDeath_Text[i].text = "0/0";
            }

            i++;
        }
    }


    private void Update()
    {
        if(timer.timeRemaining > 0)
        {
            playersHolder.SetActive(Input.GetKey(KeyCode.Tab));
        }
    }
}
