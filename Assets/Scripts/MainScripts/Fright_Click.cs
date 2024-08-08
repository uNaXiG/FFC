using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fright_Click : MonoBehaviour
{
    public GameObject FadeOut;
    public AudioSource click_se;

    private void Start()
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
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
