using UnityEngine;
using System.Collections.Generic;

class MovingCar
{
  public GameObject car;
  Vector3[] wayPoint;
  public float timeElapsed;

  public MovingCar(Vector3[] wayPoint, GameObject obj)
  {
    car = obj;
    this.wayPoint = wayPoint;
    timeElapsed = 0f;
  }

  public void Move()
  {
    float t = timeElapsed / LegoData.CAR_SPEED;
    Vector3 pos = Bezier3D(Bezier3D(wayPoint[0], wayPoint[1], t), Bezier3D(wayPoint[1], wayPoint[2], t), t);
    car.transform.position = pos;
    Vector3 direction = 2 * ((t - 1) * wayPoint[0] + (1 - 2 * t) * wayPoint[1] + wayPoint[2] * t);
    car.transform.rotation = Quaternion.LookRotation(direction);
  }

  Vector3 Bezier3D(Vector3 p1, Vector3 p2, float t)
  {
    return (1 - t) * p1 + t * p2;
  }
}

public class MoveCar : MonoBehaviour
{
  List<MovingCar> movingCar = new List<MovingCar>();

  public void Init(Collider collider, Vector3[] wayPoint)
  {
    foreach (var item in movingCar)
    {
      if (item.car == collider.gameObject) return;
    }
    MovingCar car = new MovingCar(wayPoint, collider.gameObject);
    movingCar.Add(car);
  }

  void Update()
  {
    List<MovingCar> removeList = new List<MovingCar>();

    foreach (var item in movingCar)
    {
      if(item.car == null)
      {
        removeList.Add(item);
        continue;
      }

      item.timeElapsed += Time.deltaTime;
      item.Move();

      if (item.timeElapsed > LegoData.CAR_SPEED)
      {
        removeList.Add(item);
      }
    }

    foreach (var item in removeList)
    {
      movingCar.Remove(item);
    }
  }
}
