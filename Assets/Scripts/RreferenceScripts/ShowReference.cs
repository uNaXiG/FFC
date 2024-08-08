using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowReference : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = this.GetComponent<Text>();
    }

    void ReMoveTextToTitle()
    {
        Destroy(text);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
