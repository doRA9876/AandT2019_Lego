using LegoEnum;
using UnityEngine;

struct RiverLegoInfo
{
  internal LandscapeType_OverView overView;
  internal LandscapeType_Details detail;
  internal Direction direction;
}

internal class LegoRiverMap
{
  RiverLegoInfo[,] riverMap;
  LandscapeLegoInfo[,] landscapeMap;
  int[,] label;

  internal LegoRiverMap(LandscapeLegoInfo[,] map)
  {
    this.landscapeMap = map;
    riverMap = new RiverLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    label = new int[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        label[x, y] = -1;
      }
    }
    CreateRiverMap();
  }

  void CreateRiverMap()
  {
    Init();
    Debug.Log("Init completed.");

    void Init()
    {
      int labelCount = 0;
      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          if (landscapeMap[x, y].overView != LandscapeType_OverView.Water)
          {
            riverMap[x, y].overView = LandscapeType_OverView.Spaces;
            riverMap[x, y].detail = LandscapeType_Details.Space;
          }
          else
          {
            Labeling(x, y, labelCount);
            if (label[x, y] == labelCount) labelCount++;
          }
        }
      }
    }
  }

  void Labeling(int x, int y, int count)
  {
    if (label[x, y] != -1) return;
    else label[x, y] = count;

    riverMap[x, y].overView = LandscapeType_OverView.Water;

    if (landscapeMap[x, y].north == LandscapeType_OverView.Water) Labeling(x, y - 1, count);
    if (landscapeMap[x, y].east == LandscapeType_OverView.Water) Labeling(x + 1, y, count);
    if (landscapeMap[x, y].south == LandscapeType_OverView.Water) Labeling(x, y + 1, count);
    if (landscapeMap[x, y].west == LandscapeType_OverView.Water) Labeling(x - 1, y, count);
  }
}