using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameVictoryMenu : MonoBehaviour
{
    public void quitGame()
    {
        StartCoroutine(waitForSoundQuit());
    }
    IEnumerator waitForSoundQuit()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
    public void replayGame()
    {
        StartCoroutine(waitForSoundReplay());
    }
    IEnumerator waitForSoundReplay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
