using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour {


    public SceneFader sceneFader;//ссылка на эффект между сценами
    public string menuSceneName = "MainMenu"; //переменная имени сцены меню


    public string nextLevel = "Level02";//название следующего уровня
    public int levelToUnlock = 2;//уровень который откроеться после прохождение предыдущего


    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);//открываем новый уровень, чтобы он был доступен
        sceneFader.FadeTo(nextLevel);//переходим на новый уровень
    }


    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
        Debug.Log("Go to MENU !");
    }
}
