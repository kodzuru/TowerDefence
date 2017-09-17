using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{

    public Image img; //ссылка на картинку которой манипулируем

    public AnimationCurve curve;//ссылка на кривую по которой будет следовать анимация

    void Start()
    {
        StartCoroutine(FadeIn());// повторение фунцкии
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
        //исчезнавение чёрного квадрата между сценами
    {
        //начальное значение альфа-канала цвета(времени)
        float t = 1f;

        //пока значение больше нуля
        while (t > 0f)
        {
            t -= Time.deltaTime;//уменьшаем значени альфы

            float a = curve.Evaluate(t); //помещаем значение ИКСОВ(time - t) в нашу кривую

            img.color = new Color(0f, 0f, 0f, a);//изменяем цвет

            yield return 0f;//возвращаем нуль в енумератор
        }
    }


    IEnumerator FadeOut(string scene)
        //появление чёрного квадрата между сценами
    {
        //начальное значение альфа-канала цвета(времени)
        float t = 0f;

        //пока значение больше нуля
        while (t < 1f)
        {
            t += Time.deltaTime;//уменьшаем значени альфы

            float a = curve.Evaluate(t); //помещаем значение ИКСОВ(time - t) в нашу кривую

            img.color = new Color(0f, 0f, 0f, a);//изменяем цвет

            yield return 1f;//возвращаем нуль в енумератор
        }
        //после эффекта между сцен загружаем нужную сцену
        SceneManager.LoadScene(scene);
    }


}
