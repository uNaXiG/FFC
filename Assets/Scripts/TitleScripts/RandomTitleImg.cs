using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTitleImg : MonoBehaviour
{
    public Sprite[] arr = new Sprite[5];
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        img.GetComponent<Image>();
        System.Random r = new System.Random();
        img.sprite = arr[r.Next(0, 5)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
