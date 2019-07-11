using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
  [SerializeField]
  RectTransform stack, art, block, logo;
  [SerializeField]
  Text presenText;
  [SerializeField]
  GameObject parent;
  private float elapsedTime;
  private int step;
  private readonly float dropSpeed = 30f;
  private AudioSource sound01, sound02;

  // Start is called before the first frame update
  void Start()
  {
    block.localPosition = new Vector3(0, 650, 0);
    art.localPosition = new Vector3(0, 850, 0);
    stack.localPosition = new Vector3(0, 1050, 0);
    logo.localPosition = new Vector3(0, 1150, 0);
    elapsedTime = 0;
    step = 0;

    AudioSource[] audioSources = GetComponents<AudioSource>();
    sound01 = audioSources[0];
    sound02 = audioSources[1];
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      if (step >= 4) return;
      block.localPosition = new Vector3(0, -350, 0);
      art.localPosition = new Vector3(0, -150, 0);
      stack.localPosition = new Vector3(0, 50, 0);
      logo.localPosition = new Vector3(0, 230, 0);
      step = 4;
    }

    switch (step)
    {
      case 0:
        Vector3 vec0 = new Vector3(block.localPosition.x, block.localPosition.y - dropSpeed, block.localPosition.z);
        block.localPosition = vec0;
        if (block.localPosition.y < -350)
        {
          block.localPosition = new Vector3(0, -350, 0);
          sound02.PlayOneShot(sound02.clip);
          step++;
        }
        break;

      case 1:
        Vector3 vec1 = new Vector3(art.localPosition.x, art.localPosition.y - dropSpeed, art.localPosition.z);
        art.localPosition = vec1;
        if (art.localPosition.y < -150)
        {
          art.localPosition = new Vector3(0, -150, 0);
          sound02.PlayOneShot(sound02.clip);
          step++;
        }
        break;

      case 2:
        Vector3 vec2 = new Vector3(stack.localPosition.x, stack.localPosition.y - dropSpeed, stack.localPosition.z);
        stack.localPosition = vec2;
        if (stack.localPosition.y < 50)
        {
          stack.localPosition = new Vector3(0, 50, 0);
          sound02.PlayOneShot(sound02.clip);
          step++;
        }
        break;

      case 3:
        Vector3 vec3 = new Vector3(logo.localPosition.x, logo.localPosition.y - dropSpeed, logo.localPosition.z);
        logo.localPosition = vec3;
        if (logo.localPosition.y < 230)
        {
          logo.localPosition = new Vector3(0, 230, 0);
          sound02.PlayOneShot(sound02.clip);
          step++;
        }
        break;

      case 4:
        StartCoroutine(LegoGeneric.DelayMethod(0.5f, () =>
        {
          sound01.PlayOneShot(sound01.clip);
        }));
        step++;
        break;

      case 5:
        Color color = presenText.color;
        presenText.color = new Color(color.r, color.g, color.b, color.a + 0.01f);
        if (color.a > 0.99f)
        {
          step++;
        }
        break;

      case 6:
        StartCoroutine(LegoGeneric.DelayMethod(3f, () =>
        {
          gameObject.SetActive(false);
          parent.GetComponent<LegoInit>().enabled = true;
        }));
        step++;
        break;
      default:
        break;
    }
  }
}

