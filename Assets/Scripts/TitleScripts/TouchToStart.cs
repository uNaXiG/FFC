using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchToStart : MonoBehaviour
{
    public AudioSource click_se;
    public GameObject animator;    
    bool isTouch = false;
    GameObject temp;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if ( (Input.GetMouseButtonDown(0) || Input.anyKey) && !isTouch)
        {
            StartCoroutine(waitfor());
        }

    }
    IEnumerator waitfor()
    {
        if (!isTouch)
        {
            click_se.Play();
            isTouch = true;
        }
        temp = Instantiate(animator);
        yield return 0;
        yield return new WaitForSeconds(1.0f);
        if(!StoryText.isReadFinished)
            SceneManager.LoadScene(5);  // to read story
        else SceneManager.LoadScene(1);  // to main
    }
}
