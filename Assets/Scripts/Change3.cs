using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change3 : MonoBehaviour
{
    public void SceneChange()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage3");
    }
}