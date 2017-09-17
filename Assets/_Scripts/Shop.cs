using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager; //ссылка на инстанс менеджера

    public TurretBlueprint standartTorret; //ссылка на класс стоимости вышек
    public TurretBlueprint missleLauncher; //ссылка на класс стоимости вышек
    public TurretBlueprint laserBeamer; //ссылка на класс стоимости вышек



    void Start()
    {
        //получаем инстанс
        buildManager = BuildManager.instance;
    }


    public void SelectStandartTorret()
        //выбираем обычную вышку для постройки
    {
        //передаём из UI по клику какой префаб вызвать в BuildManager
        buildManager.SelectTurretToBuild(standartTorret);
        Debug.Log("Purchased Standart Torret!");
    }
    public void SelectMissleLauncher()
        //выбираем другую вышку для постройки
    {
        //передаём из UI по клику какой префаб вызвать в BuildManager
        buildManager.SelectTurretToBuild(missleLauncher);
        Debug.Log("Purchased Missle Launcher!");
    }
    public void SelectLaserBeamer()
    //выбираем другую вышку для постройки
    {
        //передаём из UI по клику какой префаб вызвать в BuildManager
        buildManager.SelectTurretToBuild(laserBeamer);
        Debug.Log("Purchased Laser Beamer!");
    }
}
