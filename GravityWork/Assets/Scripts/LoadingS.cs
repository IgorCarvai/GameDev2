using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingS : MonoBehaviour
{

    public float loadingTime;
    public Image loadingbar;
    public int scenetoLoad;

    // Use this for initialization
    void Start()
    {

        loadingbar.fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (loadingbar.fillAmount <= 1)
        {
            loadingbar.fillAmount += 1.0f / loadingTime * Time.deltaTime;

        }

        if (loadingbar.fillAmount == 1.0f)
        {
            Application.LoadLevel(scenetoLoad);
        }
    }
}
