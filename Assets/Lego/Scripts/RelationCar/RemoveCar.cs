using UnityEngine;

public class RemoveCar : MonoBehaviour
{
  void OnTriggerEnter(Collider collision)
  {
    Destroy(collision.gameObject);
  }
}
