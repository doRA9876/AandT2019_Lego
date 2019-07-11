using UnityEngine;

public class ArroundRotationBehaviour : MonoBehaviour
{
  private Vector3 cameraCenterPoint = new Vector3(LegoData.LANDSCAPE_MAP_WIDTH * LegoData.LANDSCAPE_OBJECT_WIDTH / 2, 0f, LegoData.LANDSCAPE_MAP_HEIGHT * LegoData.LANDSCAPE_OBJECT_HEIGHT / 2);
  private float radious = 100f;

  void Update()
  {
    float x = radious * Mathf.Sin(Time.time / 10f) + cameraCenterPoint.x;
    float z = radious * Mathf.Cos(Time.time / 10f) + cameraCenterPoint.z;
    gameObject.transform.position = new Vector3(x, 80f, z);
    Vector3 toCenterVec = new Vector3(cameraCenterPoint.x - x, -80f, cameraCenterPoint.z - z);
    gameObject.transform.rotation = Quaternion.LookRotation(toCenterVec);
  }
}
