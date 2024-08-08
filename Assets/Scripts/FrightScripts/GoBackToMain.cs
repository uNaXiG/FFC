using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoBackToMain : MonoBehaviour
{
    public GameObject FadeOut;
    public AudioSource click_se;
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        StartCoroutine(waitfor());
    }    

    IEnumerator waitfor()
    {
        click_se.Play();
        Instantiate(FadeOut);
        yield return 0;
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
