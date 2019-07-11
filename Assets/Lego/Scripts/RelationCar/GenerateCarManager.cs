using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCarManager : MonoBehaviour
{
  GenerateCar[] carGenerators;
  int carGeneratorNum;
  private float timeElapsed;
  private readonly float generateSpeed = 3f;
  bool isInitialized = false;
  // Start is called before the first frame update
  void Start()
  {
    timeElapsed = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if (!isInitialized)
    {
      Initialize();
      return;
    }

    timeElapsed += Time.deltaTime;

    if (timeElapsed > generateSpeed)
    {
      timeElapsed = 0;

      int executeNumber = Random.Range(0, carGeneratorNum);
      carGenerators[executeNumber].ExecuteGeneration();
    }
  }

  void Initialize()
  {
    isInitialized = true;
    GameObject[] carGeneratorsObj = GameObject.FindGameObjectsWithTag("CarGenerator");
    carGeneratorNum = carGeneratorsObj.Length;
    carGenerators = new GenerateCar[carGeneratorNum];

    for (int i = 0; i < carGeneratorNum; i++)
    {
      carGenerators[i] = carGeneratorsObj[i].GetComponent<GenerateCar>();
    }
  }
}
