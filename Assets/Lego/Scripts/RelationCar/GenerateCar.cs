using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCar : MonoBehaviour
{
  public static GameObject skyCar_Red, policeCar, car_Sedan;
  float elapsedTime = 0f;
  float nextGenerateTiming = 0f;

  void Start()
  {
    skyCar_Red = (GameObject)Resources.Load("Car/SkyCar_Red");
    policeCar = (GameObject)Resources.Load("Car/PoliceCar");
    car_Sedan = (GameObject)Resources.Load("Car/Car_Sedan");
  }

  public void ExecuteGeneration()
  {
    int random = Random.Range(0, 3);
    GameObject obj;
    switch (random)
    {
      case 0:
        obj = skyCar_Red;
        break;

      case 1:
        obj = policeCar;
        break;
      
      case 2:
        obj = car_Sedan;
        break;

      default:
        obj = skyCar_Red;
        break;
    }
    Instantiate(obj, gameObject.transform.position, Quaternion.identity);
  }
}
