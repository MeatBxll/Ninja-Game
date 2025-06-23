using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class stageSelect : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene(4);
    }
    public void PlayLevel5()
    {
        SceneManager.LoadScene(5);
    }
}
