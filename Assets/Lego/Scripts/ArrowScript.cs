using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
  [SerializeField]
  ViewSpotManager viewSpot;

  void OnTriggerEnter()
  {
    viewSpot.TouchedArrow(gameObject.name);
  }
}
