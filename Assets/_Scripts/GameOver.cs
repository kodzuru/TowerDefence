using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{



    public SceneFader sceneFader;//ссылка на эффект между сценами
    public string menuSceneName = "MainMenu"; //переменная имени сцены меню



    public void Retry()
    {
        //загружаем сцену под индексом 0(можно посмотреть индекс в build manager)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
        Debug.Log("Go to MENU !");
    }

}
