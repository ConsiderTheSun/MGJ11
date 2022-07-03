using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void startGame()
    {
        StartCoroutine(waittostart());
    }
    public void quitGame()
    {
        StartCoroutine(waittoquit());
    }
    IEnumerator waittostart()
    {
        //waits to allow for sound effect to play and loads the next scene in the scene manager
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator waittoquit()
    {
        //waits to allow for sound effect to play and quits the app
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

}
