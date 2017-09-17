using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject ui;//ссылка на UI элемент меню паузы

    public SceneFader sceneFader;//ссылка на эффект между сценами

    public string menuSceneName = "MainMenu"; //переменная имени сцены меню

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        //тогл-ает меню
        ui.SetActive(!ui.activeSelf);

        //если меню активно
        if (ui.activeSelf)
            //замедляем время до нуля(останавливаем время)
            Time.timeScale = 0f;
        else//если меню не активно
            Time.timeScale = 1f;//снимаем с паузы(востанавливаем нормальный поток времени)
        
    }

    public void Retry()
    {
        Toggle();//для возвращения потока времени в нормальное состояние
        //загружаем сцену под индексом 0(можно посмотреть индекс в build manager)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();//для возвращения потока времени в нормальное состояние
        sceneFader.FadeTo(menuSceneName);
    }


}
