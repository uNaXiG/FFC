using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFrightScene : MonoBehaviour
{
    public GameObject FadeIn;   // Fade in prefab
    GameObject tempFadeIn;      // temp fade in
    public GameObject[] Actor_Img = new GameObject[3];
    void Start()
    {
        Actor_Img[ActorAbility.TypeIndex].SetActive(true);
        StartCoroutine(waitforFadeIn());
    }    

    IEnumerator waitforFadeIn()
    {
        tempFadeIn = Instantiate(FadeIn);
        yield return 0;
        yield return new WaitForSeconds(1.0f);
        Destroy(tempFadeIn);
    }
}
