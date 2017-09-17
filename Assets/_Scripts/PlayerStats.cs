using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Money;//бабло во время игры(например если сменить уровень, сцену)
    public int startMoney = 400;//начальное к-во бабла

    public static int Lives;//количество жизней игрока
    public int startLives;//начальное к-во жизней

    public static int rounds; //количество раундов которые провёл в игре

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;

        rounds = 0;
    }


}
