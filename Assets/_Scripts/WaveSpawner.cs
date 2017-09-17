using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemyesAlive = 0; //количество живых врагов на сцене

    //public Transform enemyPrefab; //позиция врага
    public Wave[] waves;//класс волны врагов

    public float timeBetweenWaves = 5f;// время между волнами

    public Transform spawnPosition; //место появления врагов

    //public float timeBetweenEnemyInWave = 0.2f; //время между врагами в волне

    float countdown = 2f; //обратный отсчёт(время до первой волны)

    int waveIndex = 0; //номер волны

    public Text waveCountdownText; // ссылка на HUD текста для отображения времени до следующей волны

    public GameManager gameManager; //ссылка на объект GameManager

    void Update()
    {
        //если количество врагов на сцене больше нуля
        if (EnemyesAlive > 0)
        {
            return;//не спавним новую волну
        }

        //если больше нет волн, то победа над уровнем
        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON!");
            gameManager.WinLevel();//победа в уровне
            enabled = false;//выключаем скрипт
        }

        //если пришло время для волны
        if (countdown <= 0f)
        {
            //вызываем волну врагов
            StartCoroutine(SpawnWave());
            //время до новой волны = времени между волнами
            countdown = timeBetweenWaves;
            //останавливаем счётчик времени и ждём пока все враги не пропадут и только потом включаем счётчик
            return;
        }
        //уменьшаем счётчик времени
        countdown -= Time.deltaTime;

        //устанавливаем значение между
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); 
        //устанавливаем формат в котором будет выводиться наша строка
        waveCountdownText.text = string.Format("{0:00.00}", countdown);

    }

    IEnumerator SpawnWave()
    {

        //Debug.Log("Wave Incomming!  " + waveIndex);

        //выбираем какую волну из массива волн мы хотим спавнить
        Wave wave = waves[waveIndex];

        //количество живых врагов равно к-ву врагов в волне
        EnemyesAlive = wave.count;

        PlayerStats.rounds++;//увеличиваем количество раундов, что провёл в игре

        //количество врагов в волне в зависимости от номера волны
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy); //спавн врага
            //задержка между спавнами врага в волне
            yield return new WaitForSeconds(1f / wave.rate);
        }
        //номер волны
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPosition.position, spawnPosition.rotation);
        //EnemyesAlive++;//увеличиваем переменную к-ва врагов

    }

}
