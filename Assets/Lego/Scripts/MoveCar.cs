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
    float t = timeElapsed / 5f;
    Vector2 p0 = new Vector2(wayPoint[0].x, wayPoint[0].z);
    Vector2 p1 = new Vector2(wayPoint[1].x, wayPoint[1].z);
    Vector2 p2 = new Vector2(wayPoint[2].x, wayPoint[2].z);
    Vector2 pos = Bezier2D(Bezier2D(p0, p1, t), Bezier2D(p1, p2, t), t);
    Vector3 pos3d = new Vector3(pos.x, 0f, pos.y);
    car.transform.position = pos3d;
    Vector2 direction = 2 * ((t - 1) * p0 + (1 - 2 * t) * p1 + p2 * t);
    Vector3 direction3d = new Vector3(direction.x, 0f, direction.y);
    car.transform.rotation = Quaternion.LookRotation(direction3d);
  }
  
  Vector2 Bezier2D(Vector2 p1, Vector2 p2, float t)
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
      if(item.car == collider.gameObject) return;
    }
    MovingCar car = new MovingCar(wayPoint, collider.gameObject);
    movingCar.Add(car);
  }

  void Update()
  {
    foreach (var item in movingCar)
    {
      item.timeElapsed += Time.deltaTime;
      item.Move();

      if(item.timeElapsed > 5f)
      {
        movingCar.Remove(item);
      }
    }
  }
}
