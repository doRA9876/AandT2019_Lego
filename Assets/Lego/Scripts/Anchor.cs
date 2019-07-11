using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
  [SerializeField]
  private GameObject player;
  private PlayerScript playerScript;
  private static Rigidbody player_rigidbody;
  private static Vector3 normalizedDirection;
  bool isLaunched = false, isPulling = false, isCollided = false;

  void Start()
  {
    player_rigidbody = player.GetComponent<Rigidbody>();
    normalizedDirection = new Vector3(0, 0, 0);
    playerScript = player.GetComponent<PlayerScript>();
  }

  void Update()
  {
    if (gameObject.transform.localPosition.z < 0f)
    {
      gameObject.transform.localPosition = new Vector3(0, 0, 0);
      isLaunched = false;
      isPulling = false;
    }

    if (isLaunched && !isPulling)
    {
      gameObject.transform.position += gameObject.transform.forward.normalized * 0.2f;
    }

    if (isPulling)
    {
      gameObject.transform.position += gameObject.transform.forward.normalized * -0.2f;
    }

  }

  public void Launch()
  {
    if (isPulling == false)
    {
      isLaunched = true;
    }
  }

  public void Pull()
  {
    isPulling = true;
    isLaunched = false;
  }

  void OnTriggerEnter(Collider collider)
  {
    if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "Anchor") return;
    if (isLaunched)
    {
      /*
      player.transform.position = gameObject.transform.position;
      player.transform.rotation = Quaternion.LookRotation(player.transform.forward);
      player_rigidbody.velocity = Vector3.zero;
      player_rigidbody.angularVelocity = Vector3.zero;
      */
      Vector3 direction = gameObject.transform.position + new Vector3(0,1,0) - player.transform.position;
      //direction = direction.normalized;
      //direction = direction + normalizedDirection;
      //normalizedDirection = direction.normalized;
      //playerScript.Move(normalizedDirection);
      playerScript.Move(direction);
      gameObject.transform.localPosition = new Vector3(0, 0, 0);
      isLaunched = false;
      isPulling = false;
    }
  }
}
