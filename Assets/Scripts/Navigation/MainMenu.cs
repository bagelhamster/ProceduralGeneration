using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayManual()
    {
        SceneManager.LoadScene("Manual");
    }
    public void PlayProcedural()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");

    }

}
