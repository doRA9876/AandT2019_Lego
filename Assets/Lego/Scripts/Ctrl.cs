using UnityEngine;
using Valve.VR;

public class Ctrl : MonoBehaviour
{
  [SerializeField]
  Anchor anchor;
  public SteamVR_Action_Boolean grabAction;
  public SteamVR_Input_Sources handType;
  bool isGrasping, isGriping;

  void Update()
  {
    if (grabAction.GetState(handType))
    {
      if (isGrasping == false)
      {
        anchor.Launch();
        isGrasping = true;
      }
    }
    else
    {
      if (isGrasping == true)
      {
        anchor.Pull();
        isGrasping = false;
      }
    }


  }
}
