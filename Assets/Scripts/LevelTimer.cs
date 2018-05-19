using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = -1;
    public float timeRemaining;

    public Text displayText;
    public AudioClip alarmSfx;

    private Transform player;

	void Update ()
    {
        if (timeLimit < 0)
            return; // No time limit set

        timeRemaining = timeLimit - Time.timeSinceLevelLoad;
        if (timeRemaining < 0.0f)
            timeRemaining = 0.0f;

        string minutes = Mathf.Floor(timeRemaining / 60).ToString("00");
        string seconds = Mathf.Floor(timeRemaining % 60).ToString("00");

        displayText.text = minutes + ":" + seconds;

        if (timeRemaining <= 10)
        {
            if (Mathf.Floor(timeRemaining) % 2 == 0)
            {
                displayText.color = Color.red;
                if (alarmSfx != null)
                {
                    if (player == null)
                        player = GameObject.FindObjectOfType<PlayerShipController>().gameObject.transform;
                    AudioSource.PlayClipAtPoint(alarmSfx, player.position);
                }
            }
            else
            {
                displayText.color = Color.white;
            }
        }

        if (timeRemaining <= 0)
        {
            GameObject.FindObjectOfType<LevelManager>().GameLost();
        }
	}
}
