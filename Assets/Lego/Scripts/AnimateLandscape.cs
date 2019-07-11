using UnityEngine;

public class AnimateLandscape : MonoBehaviour
{
  GameObject[] buildingBlocks;
  float height;
  int legoNum;
  AudioSource audioSource;

  // Start is called before the first frame update
  void Start()
  {
    buildingBlocks = GameObject.FindGameObjectsWithTag("Building");
    height = 0f;
    legoNum = 0;
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    buildingBlocks[legoNum].transform.position -= new Vector3(0f, 2f, 0f);
    height++;

    if (height == 50f)
    {
      legoNum++;
      height = 0;
      audioSource.PlayOneShot(audioSource.clip);
    }
    if (legoNum == buildingBlocks.Length)
      enabled = false;
  }
}