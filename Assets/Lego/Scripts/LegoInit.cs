using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LegoInit : MonoBehaviour
{
  [SerializeField]
  GameObject kinectCamera, viewCamera;
  Vector3 cameraCenterPoint = new Vector3(80f, 0f, 80f);
  float radious = 100f;
  AudioSource audioSource;

  void Awake()
  {
    if (!LegoObjects.IsLoaded) LegoObjects.LoadGameObjects();
  }

  void Start()
  {
    if (LegoData.CalibrationData.HasCalibrationData())
    {
      LegoData.isCalibrated = true;
    }
    audioSource = GetComponent<AudioSource>();
    audioSource.PlayOneShot(audioSource.clip);
  }

  void Update()
  {
    float x = radious * Mathf.Sin(Time.time/5f) + cameraCenterPoint.x;
    float z = radious * Mathf.Cos(Time.time/5f) + cameraCenterPoint.z;
    viewCamera.transform.position = new Vector3(x, 80f, z);
    Vector3 toCenterVec = new Vector3(cameraCenterPoint.x - x, -80f, cameraCenterPoint.z - z);
    viewCamera.transform.rotation = Quaternion.LookRotation(toCenterVec);
  }

  public void OnClickButton_yes()
  {
    kinectCamera.SetActive(true);
    Debug.Log("Calibration画面へ移行します。");
    LegoData.isInitialized = true;
    StartCoroutine(LegoGeneric.DelayMethod(3.5f, () =>
    {
      SceneManager.LoadScene("Calibration");
    }));
  }

  public void OnClickButton_no()
  {
    kinectCamera.SetActive(true);
    LegoData.isInitialized = true;
    if (LegoData.isCalibrated)
    {
      Debug.Log("Main画面へ移行します。");
      StartCoroutine(LegoGeneric.DelayMethod(3.5f, () =>
      {
        SceneManager.LoadScene("Main");
      }));
    }
    else
    {
      Debug.Log("Calibration Dataがありません。\nCalibration画面に移行します。");
      StartCoroutine(LegoGeneric.DelayMethod(3.5f, () =>
      {
        SceneManager.LoadScene("Calibration");
      }));
    }
  }
}
