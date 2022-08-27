using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(WaitToStart());
    }
    public void QuitGame()
    {
        StartCoroutine(WaitToQuit());
    }
    IEnumerator WaitToStart()
    {
        //waits to allow for sound effect to play and loads the next scene in the scene manager
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator WaitToQuit()
    {
        //waits to allow for sound effect to play and quits the app
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }

}
