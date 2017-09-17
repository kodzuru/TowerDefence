using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad = "MainLevel";//название уровня который хотим загрузить
    public SceneFader sceneFader;//эффект загрузки между сценами

    public void Play()
    {
        //эффект исчезания сцены и и загрузки нового уровня
        sceneFader.FadeTo(levelToLoad);
        //загружаем нужный уровень
        //SceneManager.LoadScene(levelToLoad);

    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
