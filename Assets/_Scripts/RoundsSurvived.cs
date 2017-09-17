using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour {


    public Text roundsText; //текст для показа количества пройденых раундов

    void OnEnable()
    {
        //коротин с анимацией счётчика к-ва раундов
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";//начальный текст
        int round = 0;//начальное количество пройденых раундов

        yield return new WaitForSeconds(0.7f); //задержка


        //до тех пор пока не уравняется значение с пройдеными
        while (round < PlayerStats.rounds)
        {
            //увеличиваем значени раундов
            round++;
            roundsText.text = round.ToString();//помещаем его в UI элемент text
            yield return new WaitForSeconds(0.05f);//задержка
        }


    }

}
