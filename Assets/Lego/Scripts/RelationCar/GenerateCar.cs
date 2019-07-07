using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCar : MonoBehaviour
{
  public static GameObject car;
  float elapsedTime = 10f;

  void Start()
  {
    car = (GameObject)Resources.Load("Car/SkyCar");
  }

  void Update()
  {
    elapsedTime += Time.deltaTime;

    if(elapsedTime > 10f)
    {
      elapsedTime = 0;
      ExecuteGeneration();
    }
  }

  public void ExecuteGeneration()
  {
    Instantiate(car, gameObject.transform.position, Quaternion.identity);
  }
}
