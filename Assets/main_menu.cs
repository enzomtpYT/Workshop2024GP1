using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene("Charlie");
    }

    public void QuitGame ()
    {
        Debug.Log("Le jeu a quitté");
        Application.Quit();
    }
}
