using UnityEngine;

public class RemoveCar : MonoBehaviour
{
  void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.tag == "Car")
    {
      Destroy(collision.gameObject);
    }

    if(collision.gameObject.tag == "Player")
    {
      GameObject.Find("ViewSpotManager").GetComponent<ViewSpotManager>().MoveRandom();
    }
  }
}
