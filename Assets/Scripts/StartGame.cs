using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour
{
    public void StartGameLevel ()
    {
        SceneManager.LoadScene(0);
        HeroController.Player_Level = 1;
    }
}
