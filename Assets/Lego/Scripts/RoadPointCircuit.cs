using UnityEngine;

public class RoadPointCircuit : MonoBehaviour
{
  [SerializeField, HeaderAttribute("Parent")]
  private Transform parent;
  [SerializeField, HeaderAttribute("RightSide RoadPoint")]
  private Transform roadPointR1;
  [SerializeField]
  private Transform roadPointR2, roadPointR3;
  [SerializeField, HeaderAttribute("LeftSide RoadPoint")]
  private Transform roadPointL1;
  [SerializeField]
  private Transform roadPointL2, roadPointL3;

  public RoadPoint GetCurveRoadPoint_R()
  {
    
    Vector3[] point = new Vector3[3];
    point[0] = roadPointR1.position;
    point[1] = roadPointR2.position;
    point[2] = roadPointR3.position;
    RoadPoint roadPoint = new RoadPoint(3);
    roadPoint.point = point;
    return roadPoint;
  }

  public RoadPoint GetCurveRoadPoint_L()
  {
    Vector3[] point = new Vector3[3];
    point[0] = roadPointL1.position;
    point[1] = roadPointL2.position;
    point[2] = roadPointL3.position;
    RoadPoint roadPoint = new RoadPoint(3);
    roadPoint.point = point;
    return roadPoint;
  }

  public RoadPoint GetStraightRoadPoint_R()
  {
    Vector3[] point = new Vector3[2];
    point[0] = roadPointR1.position;
    point[1] = roadPointR2.position;
    RoadPoint roadPoint = new RoadPoint(2);
    roadPoint.point = point;
    return roadPoint;
  }

  public RoadPoint GetStraightRoadPoint_L()
  {
    Vector3[] point = new Vector3[2];
    point[0] = roadPointL1.position;
    point[1] = roadPointL2.position;
    RoadPoint roadPoint = new RoadPoint(2);
    roadPoint.point = point;
    return roadPoint;
  }
}
