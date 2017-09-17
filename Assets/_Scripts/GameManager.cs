using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static bool gameIsOver; //конец игры

    public GameObject gameOverUI; //ссылка на UI элемент GameOver

    public SceneFader sceneFader;//ссылка на фейдер

    public GameObject completeLevelUI;//ссылка на UI элемент CompleteLevel


    void Start()
    {
        //в начале каждой сцены устанавливаем переменную конца игры в false
        gameIsOver = false;
    }

	// Update is called once per frame
	void Update () {

        //если игра закончена то всё
        if(gameIsOver)
            return;

        //если нажали E
        if(Input.GetKey("e"))
            EndGame();//конец игры

        //если к-во жизней меньше нуля
        if (PlayerStats.Lives <= 0)
            EndGame();//... конец игры
	}

    void EndGame()
    {
        gameIsOver = true;//игра закончена
        
        //включаем наш UI
        gameOverUI.SetActive(true);
        
    }

    public void WinLevel()
    {
        Debug.Log("LEVEL WON!");
        gameIsOver = true;
        completeLevelUI.SetActive(true);

    }

}
