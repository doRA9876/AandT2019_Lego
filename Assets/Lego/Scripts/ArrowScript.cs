using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
  [SerializeField]
  ViewSpot viewSpot;

  void OnTriggerEnter()
  {
    viewSpot.TouchedArrow(gameObject.name);
    Debug.Log("ok");
  }
}
