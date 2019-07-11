using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
  [SerializeField]
  private GameObject player;
  private PlayerScript playerScript;
  private static Rigidbody player_rigidbody;
  private bool isLaunched = false, isPulling = false;
  public bool isCollided = false;
  private Vector3 position;
  private AudioSource audioRazer, audioSlash;

  void Start()
  {
    player_rigidbody = player.GetComponent<Rigidbody>();
    playerScript = player.GetComponent<PlayerScript>();
    AudioSource[] audioSource = GetComponents<AudioSource>();
    audioRazer = audioSource[0];
    audioSlash = audioSource[1];
  }

  void Update()
  {
    if (gameObject.transform.localPosition.z < 0f)
    {
      gameObject.transform.localPosition = new Vector3(0, 0, 0);
      isLaunched = false;
      isPulling = false;
    }

    if (isCollided)
    {
      transform.position = this.position;
    }
    else
    {
      if (isLaunched)
      {
        gameObject.transform.position += gameObject.transform.forward.normalized * 0.2f;
      }

      if (isPulling)
      {
        gameObject.transform.position += gameObject.transform.forward.normalized * -0.2f;
      }
    }

  }

  public void Launch()
  {
    if (isPulling == false)
    {
      isLaunched = true;
      audioRazer.PlayOneShot(audioRazer.clip);
    }
  }

  public void Pull()
  {
    isPulling = true;
    isLaunched = false;
    isCollided = false;
  }

  void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Anchor") return;
    if (isLaunched)
    {
      isCollided = true;
      position = transform.position;
      audioSlash.PlayOneShot(audioSlash.clip);
    }
  }
}
