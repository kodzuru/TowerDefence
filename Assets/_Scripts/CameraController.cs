using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraPositionCoordinates
{
    //Up-down
    public float minY = 10; //минимальное значение высоты камеры
    public float maxY = 80f;//максимальное значение высоты камеры
}



public class CameraController : MonoBehaviour
{

    public float panSpeed = 30f; //скорость передвижения камеры
    public float panBorderThikness = 10f;//границы для мышки для передвижения камеры

    public float scrollSpeed = 5f; //скорость скрола

    public CameraPositionCoordinates cameraPositionCoordinates;


    //bool doMovement = true; //можно ли передвигать камеру

    // Update is called once per frame
    void Update()
    {
        //если игра закончена
        if (GameManager.gameIsOver)
        {
            enabled = false;
            return;//отключаем управление камеры в GameOverUI;
        }

        #region WASD_CAMERA_CONTROLLER
        //если нажать Escape, то камеру можно/нельзя перемещать
        //if (Input.GetButtonDown(KeyCode.Escape.ToString()))
        //    doMovement = !doMovement;
        //if (!doMovement)
        //    return;

        //если нажата клавише W или курсор мыши снесён к границе экрана
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThikness)
        {
            //перемещаем камеру по высоте вперёд
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        //если нажата клавише S или курсор мыши снесён к границе экрана
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThikness)
        {
            //перемещаем камеру по высоте назад
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        //если нажата клавише D или курсор мыши снесён к границе экрана
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThikness)
        {
            //перемещаем камеру по вправо
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        //если нажата клавише A или курсор мыши снесён к границе экрана
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThikness)
        {
            //перемещаем камеру по влево
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        //получаем значения скрола мышки
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // получаем позицию камеры
        Vector3 pos = transform.position;
        //увеличиваем/уменьшеам координату позиции камеры по Y
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        //проверяем выходит ли за пределы
        pos.y = Mathf.Clamp(pos.y, cameraPositionCoordinates.minY, cameraPositionCoordinates.maxY);
        //устанавливаем новое положение камеры
        transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);



    }
}
