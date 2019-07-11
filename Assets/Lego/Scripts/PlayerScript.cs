using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
  [SerializeField] private GameObject anchorR, anchorL;
  [SerializeField] Image blackImage;
  private Anchor anchorR_script, anchorL_script;
  private Rigidbody player_rigidbody;
  private static readonly float playerSpeed = 3.0f;
  private float elapsedTime;
  private ViewSpotManager viewSpotManager;

  // Start is called before the first frame update
  void Start()
  {
    player_rigidbody = gameObject.GetComponent<Rigidbody>();
    anchorR_script = anchorR.GetComponent<Anchor>();
    anchorL_script = anchorL.GetComponent<Anchor>();
    viewSpotManager = GameObject.Find("ViewSpotManager").GetComponent<ViewSpotManager>();
    elapsedTime = 0f;
  }

  void FixedUpdate()
  {
    elapsedTime += Time.deltaTime;
    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    player_rigidbody.angularVelocity = Vector3.zero;
    if (gameObject.transform.position.y < -3f)
    {
      player_rigidbody.velocity = Vector3.zero;
      viewSpotManager.MoveRandom();
    }

    if (elapsedTime > 6.5f)
    {
      blackImage.color = new Color(0, 0, 0, (elapsedTime - 5f));
    }

    if (anchorL_script.isCollided || anchorR_script.isCollided)
    {
      if (anchorL_script.isCollisionPlayer || anchorR_script.isCollisionPlayer)
        return;

      Vector3 vecR = new Vector3(0, 0, 0);
      Vector3 vecL = new Vector3(0, 0, 0);

      if (anchorR_script.isCollided)
      {
        vecR = anchorR.gameObject.transform.position - gameObject.transform.position;
      }

      if (anchorL_script.isCollided)
      {
        vecL = anchorL.gameObject.transform.position - gameObject.transform.position;
      }

      player_rigidbody.velocity = Vector3.zero;
      player_rigidbody.velocity = vecL.normalized * playerSpeed + vecR.normalized * playerSpeed;
    }
  }

  void OnCollisionStay(Collision collision)
  {
    elapsedTime = 0f;
    blackImage.color = new Color(0, 0, 0, 0);
  }
}
