using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public UnityEvent onWinCondition;
    public UnityEvent onLoseCondition;

    public float winResetDelay = 5.0f;
    public float loseResetDelay = 1.5f;

    public bool levelChangeQueued = false;

	void Start ()
    {
        levelChangeQueued = false;
    }

    public void GameWon()
    {
        if (levelChangeQueued)
            return;

        if (onWinCondition != null)
            onWinCondition.Invoke();

        Debug.Log("Game Won!");
        LoadLevel("win", winResetDelay);
    }

    public void GameLost()
    {
        if (levelChangeQueued)
            return;

        if (onLoseCondition != null)
            onLoseCondition.Invoke();

        Debug.Log("Game Over!");
        LoadLevel("lose", loseResetDelay);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
        levelChangeQueued = false;
    }

    public void LoadLevel(string name, float delay)
    {
        StartCoroutine(LoadOnDelay(name, delay));
    }

    IEnumerator LoadOnDelay(string levelName, float delay)
    {
        levelChangeQueued = true;
        yield return new WaitForSeconds(delay);
        LoadLevel(levelName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
