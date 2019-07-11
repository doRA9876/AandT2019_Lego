using UnityEngine;

public class ViewSpotManager : MonoBehaviour
{
  [SerializeField]
  GameObject player;
  [SerializeField]
  GameObject arrows;
  private GameObject[,] viewSpots;
  private Vector2Int currentPos, nextN, nextS, nextE, nextW;
  private readonly Vector2Int EXCEPT_POS = new Vector2Int(-1, -1);

  public void Init()
  {
    viewSpots = new GameObject[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    GameObject[] viewSpotObj = GameObject.FindGameObjectsWithTag("ViewSpot");

    foreach (var item in viewSpotObj)
    {
      Vector3 pos = item.transform.position;
      int x = (int)(pos.x / LegoData.LANDSCAPE_OBJECT_WIDTH + 0.5f);
      int z = (int)(pos.z / LegoData.LANDSCAPE_OBJECT_HEIGHT + 0.5f);
      viewSpots[x, z] = item;
    }

    currentPos = new Vector2Int(0, 0);
    nextN = new Vector2Int(0, 0);
    nextS = new Vector2Int(0, 0);
    nextW = new Vector2Int(0, 0);
    nextE = new Vector2Int(0, 0);
  }

  public void Move(int x, int y)
  {
    float xpos = viewSpots[x, y].transform.position.x;
    float zpos = viewSpots[x, y].transform.position.z;
    Vector3 newPos = new Vector3(xpos, 5f, zpos);
    player.transform.position = newPos;
    currentPos.x = x;
    currentPos.y = y;
    FindArroundNext();
    UpdateArrow();
  }

  public void Move(Vector2Int v2)
  {
    Move(v2.x, v2.y);
  }

  public void MoveRandom()
  {
    while (true)
    {
      int x = Random.Range(0, LegoData.LANDSCAPE_MAP_WIDTH);
      int y = Random.Range(0, LegoData.LANDSCAPE_MAP_HEIGHT);

      if (viewSpots[x, y] != null)
      {
        Move(x, y);
        break;
      }
    }
  }

  void FindArroundNext()
  {
    nextN = FindNextViewPoint(currentPos.x, currentPos.y, 0, -1);
    nextS = FindNextViewPoint(currentPos.x, currentPos.y, 0, 1);
    nextE = FindNextViewPoint(currentPos.x, currentPos.y, -1, 0);
    nextW = FindNextViewPoint(currentPos.x, currentPos.y, 1, 0);

    Vector2Int FindNextViewPoint(int x, int y, int dx, int dy)
    {
      if (x + dx < 0 || LegoData.LANDSCAPE_MAP_WIDTH <= x + dx || y + dy < 0 || LegoData.LANDSCAPE_MAP_HEIGHT <= y + dy)
        return EXCEPT_POS;

      if (viewSpots[x + dx, y + dy] != null)
        return new Vector2Int(x + dx, y + dy);
      else
        return FindNextViewPoint(x + dx, y + dy, dx, dy);
    }
  }

  void UpdateArrow()
  {
    /*
    if (nextN == EXCEPT_POS) arrowN.SetActive(false);
    else arrowN.SetActive(true);

    if(nextS == EXCEPT_POS) arrowS.SetActive(false);
    else arrowS.SetActive(true);

    if(nextE == EXCEPT_POS) arrowE.SetActive(false);
    else arrowE.SetActive(true);

    if(nextW == EXCEPT_POS) arrowW.SetActive(false);
    else arrowW.SetActive(true);
    */
  }

  public void TouchedArrow(string arrowName)
  {
    if (arrowName == "Arrow_North") Move(nextN);
    if (arrowName == "Arrow_South") Move(nextS);
    if (arrowName == "Arrow_East") Move(nextE);
    if (arrowName == "Arrow_West") Move(nextW);
  }
}
