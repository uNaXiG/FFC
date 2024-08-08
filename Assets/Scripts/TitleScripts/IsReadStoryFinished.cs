using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReadStoryFinished : MonoBehaviour
{
    // Scene fade out //
    public GameObject FadeOut;

    IEnumerator waitforFadeOut()
    {
        Instantiate(FadeOut);
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    void ToLoadMainScene()
    {
        StartCoroutine(waitforFadeOut());
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
}
