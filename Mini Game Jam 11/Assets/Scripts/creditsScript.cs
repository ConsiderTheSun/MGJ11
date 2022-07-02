using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); 
    }
    IEnumerator waitForCredits()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(true);
    }
    public void showCredits()
    {
        gameObject.SetActive(true);
        StartCoroutine(waitForCredits());
    }
}
