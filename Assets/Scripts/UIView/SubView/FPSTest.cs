using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSTest : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        //StartCoroutine(FPSCoroutine());
    }
    IEnumerator FPSCoroutine()
    {
        var wait = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return wait;
            text.text = "FPS: " + (Time.timeScale / Time.deltaTime);
        }
    }


}
