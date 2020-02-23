using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public static float Level_Timer = 75f;
    public Text Timer_Level;

    void Start()
    {
        Timer_Level.text = Level_Timer.ToString();
    }
    void FixedUpdate()
    {
        Level_Timer -= Time.deltaTime;
        Timer_Level.text = Mathf.Round(Level_Timer).ToString();
        if(Level_Timer < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
            Level_Timer = 75f;
        }
    }

}
