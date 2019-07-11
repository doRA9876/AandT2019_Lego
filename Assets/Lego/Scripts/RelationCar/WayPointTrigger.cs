using UnityEngine;

public class WayPointTrigger : MonoBehaviour
{
  [SerializeField]
  GameObject parent;
  [SerializeField]
  Transform wayPoint1, wayPoint2, wayPoint3;
  Vector3[] wayPoint;

  void Start()
  {
    wayPoint = new Vector3[3];
    wayPoint[0] = wayPoint1.position;
    wayPoint[1] = wayPoint2.position;
    wayPoint[2] = wayPoint3.position;
  }

  void OnTriggerStay(Collider collider)
  {
    if (collider.gameObject.tag == "Car")
      parent.GetComponent<MoveCar>().Init(collider, wayPoint);
  }
}
