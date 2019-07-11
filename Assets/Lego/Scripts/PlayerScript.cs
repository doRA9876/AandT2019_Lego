using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  [SerializeField]
  private GameObject anchorR, anchorL;
  private Anchor anchorR_script, anchorL_script;
  private Rigidbody player_rigidbody;
  private static readonly float playerSpeed = 3.0f;

  // Start is called before the first frame update
  void Start()
  {
    player_rigidbody = gameObject.GetComponent<Rigidbody>();
    anchorR_script = anchorR.GetComponent<Anchor>();
    anchorL_script = anchorL.GetComponent<Anchor>();
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

    if (anchorL_script.isCollided || anchorR_script.isCollided)
    {
      Vector3 vecR = new Vector3(0, 0, 0);
      Vector3 vecL = new Vector3(0, 0, 0);

      if (anchorR_script.isCollided)
      {
        vecR = ( anchorR.gameObject.transform.position - gameObject.transform.position).normalized * playerSpeed;
      }

      if (anchorL_script.isCollided)
      {
        vecL = (anchorL.gameObject.transform.position - gameObject.transform.position).normalized * playerSpeed;
      }
      player_rigidbody.velocity = Vector3.zero;
      player_rigidbody.velocity = vecR + vecL;
    }
  }

  void OnCollisionEnter(Collision col)
  {

  }
}
