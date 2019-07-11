using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  private Rigidbody player_rigidbody;
  Vector3 normalizedDirection;

  // Start is called before the first frame update
  void Start()
  {
    player_rigidbody = gameObject.GetComponent<Rigidbody>();
    normalizedDirection = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void FixedUpdate()
  {
    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    player_rigidbody.angularVelocity = Vector3.zero;
    //gameObject.transform.position += normalizedDirection * 0.005f;
    if (gameObject.transform.position.y < -3f)
    {
      player_rigidbody.velocity = Vector3.zero;
      gameObject.GetComponent<ViewSpot>().MoveRandom();
    }
  }

  public void Move(Vector3 direction)
  {
    normalizedDirection = direction;
    player_rigidbody.velocity += normalizedDirection * 0.4f;
  }

  void OnCollisionEnter(Collision col)
  {
    normalizedDirection = Vector3.zero;
    Debug.Log("ok");
  }
}
