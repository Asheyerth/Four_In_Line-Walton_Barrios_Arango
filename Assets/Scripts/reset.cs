using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
    public bool destroy=false;



    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("start");
        destroy = true;
        Debug.Log("check4");
    }

    private void OnClick()
    {
        Debug.Log("asdadasdasdasdasdasd");
    }

    public static bool getDestroy()
    {
        return true;
    }

   
}
