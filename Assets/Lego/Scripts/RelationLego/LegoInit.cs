using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LegoInit : MonoBehaviour
{
  [SerializeField]
  GameObject mainCamera;
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

  public void OnClickButton_Calibration()
  {
    mainCamera.SetActive(true);
    Debug.Log("Calibration画面へ移行します。");
    LegoData.isInitialized = true;
    StartCoroutine(LegoGeneric.DelayMethod(3.5f, () =>
    {
      SceneManager.LoadScene("Calibration");
    }));
  }

  public void OnClickButton_Play()
  {
    mainCamera.SetActive(true);
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
