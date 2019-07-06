using UnityEngine;
using LegoEnum;

public class RoadPointCircuit : MonoBehaviour
{
  [SerializeField, HeaderAttribute("Parent")]
  private Transform parent;
  [SerializeField, HeaderAttribute("RoadPoint")]
  private Transform roadPoint1;
  [SerializeField]
  private Transform roadPoint2, roadPoint3;

  public RoadPoint GetCurveRoadPoint(Direction enter, Direction exit)
  {
    RoadPoint roadPoint = new RoadPoint(3);
    Vector3[] point = new Vector3[3];
    if (enter == Direction.North)
    {
      point[0] = roadPoint1.position;
      point[1] = roadPoint2.position;
      point[2] = roadPoint3.position;
    }
    else
    {
      point[0] = roadPoint3.position;
      point[1] = roadPoint2.position;
      point[2] = roadPoint1.position;
    }
    roadPoint.point = point;
    return roadPoint;
  }

  public RoadPoint GetStraightRoadPoint(Direction enter, Direction exit)
  {
    RoadPoint roadPoint = new RoadPoint(2);
    Vector3[] point = new Vector3[2];
    if (enter == Direction.North)
    {
      point[0] = roadPoint1.position;
      point[1] = roadPoint2.position;
    }
    else
    {
      point[0] = roadPoint2.position;
      point[1] = roadPoint1.position;
    }
    roadPoint.point = point;
    return roadPoint;
  }
}
