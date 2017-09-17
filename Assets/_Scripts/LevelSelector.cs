using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public SceneFader sceneFader; //ссылка на фейдер

    public Button[] levelButtons;


    void Start()
    {
        //какие уровни доступны
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //выключаем все кнопки
        for (int i = 0; i < levelButtons.Length; i++)
        {
            //если уровень ещё не открыт
            if(i + 1 > levelReached)
                levelButtons[i].interactable = false;//кнопка уровня не доступна
        }
    }

    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);//загружаем уровень
    }


}
