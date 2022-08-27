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

    public void showCredits()
    {
        gameObject.SetActive(true);
    }
    public void hideCredits()
    {
        gameObject.SetActive(false);
    }
}
