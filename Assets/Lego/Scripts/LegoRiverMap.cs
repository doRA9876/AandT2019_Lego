using LegoEnum;
using UnityEngine;
using System;


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
  private int labelCount;

  internal LegoRiverMap(LandscapeLegoInfo[,] map)
  {
    this.landscapeMap = map;
    riverMap = new RiverLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    CreateRiverMap();
  }

  void CreateRiverMap()
  {
    LandscapeType_OverView[,] overviewMap = new LandscapeType_OverView[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    int[,] label = new int[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    int iterationNumber = 0;
    Init();
    label = Labeling(overviewMap);

    while (labelCount > 1)
    {

      for (int i = 0; i < iterationNumber; i++)
      {
        overviewMap = Expansion(overviewMap);
      }
      for (int i = 0; i < iterationNumber; i++)
      {
        overviewMap = Reduction(overviewMap);
      }
      label = Labeling(overviewMap);
      iterationNumber++;
    }
    Debug.Log("Init completed.");

    void Init()
    {
      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          if (landscapeMap[x, y].height == 1) overviewMap[x, y] = landscapeMap[x, y].overView;
          else overviewMap[x, y] = LandscapeType_OverView.Spaces;
        }
      }
    }

    LandscapeType_OverView[,] Expansion(LandscapeType_OverView[,] mapBefore)
    {
      LandscapeType_OverView[,] mapAfter = new LandscapeType_OverView[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
      int a = 0b0101, b = 0b0100, c = 0b0110, d = 0b0010, e = 0b1010, f = 0b1000, g = 0b1001, h = 0b0001;

      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          var position = 0b00000;
          //左辺
          if (x == 0) position = position | 0b0001;
          //右辺
          if (x == LegoData.LANDSCAPE_MAP_WIDTH - 1) position = position | 0b0010;
          //上辺
          if (y == 0) position = position | 0b0100;
          //下辺
          if (y == LegoData.LANDSCAPE_MAP_HEIGHT - 1) position = position | 0b1000;

          /*
          ポジションから見た周囲のセル
          -------------
          | 1 | 2 | 3 |
          |------------
          | 8 | p | 4 |
          |------------
          | 7 | 6 | 5 |
          |------------

          膨張・収縮処理の例外部分
          ------------------------
          | a |      b       | c |
          |---|--------------|----
          |   |              |   |
          | h |              | d |
          |   |              |   |
          |---|--------------|----
          | g |       f      | e |
          ------------------------
          */
          if (position != 0b0000)
          {
            LandscapeType_OverView cell = LandscapeType_OverView.Spaces;
            //1 -> d, e, f
            if (position == d || position == e || position == f)
              if (mapBefore[x - 1, y - 1] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            //2 -> d, e, f, g, h
            if (position == d || position == e || position == f || position == g || position == h)
              if (mapBefore[x, y - 1] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            //3 -> f,g,h
            if (position == f || position == g || position == h)
              if (mapBefore[x + 1, y - 1] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            //4 -> a, b, f, g, h
            if (position == a || position == b || position == f || position == g || position == h)
              if (mapBefore[x + 1, y] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            //5 -> a, b, h
            if (position == a || position == b || position == h)
              if (mapBefore[x + 1, y + 1] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            //6 -> a, b, c, d, h
            if (position == a || position == b || position == c || position == d || position == h)
              if (mapBefore[x, y + 1] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            //7 -> b, c, d
            if (position == b || position == c || position == d)
              if (mapBefore[x - 1, y + 1] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            //8 -> b, c, d, e, f
            if (position == b || position == c || position == d || position == e || position == f)
              if (mapBefore[x - 1, y] == LandscapeType_OverView.Water) cell = LandscapeType_OverView.Water;
            mapAfter[x, y] = cell;
          }
          else
          {
            if (mapBefore[x - 1, y - 1] == LandscapeType_OverView.Water || mapBefore[x, y - 1] == LandscapeType_OverView.Water || mapBefore[x + 1, y - 1] == LandscapeType_OverView.Water ||
            mapBefore[x - 1, y] == LandscapeType_OverView.Water || mapBefore[x + 1, y] == LandscapeType_OverView.Water ||
            mapBefore[x - 1, y + 1] == LandscapeType_OverView.Water || mapBefore[x, y + 1] == LandscapeType_OverView.Water || mapBefore[x + 1, y + 1] == LandscapeType_OverView.Water)
            {
              mapAfter[x, y] = LandscapeType_OverView.Water;
            }
            else
            {
              mapAfter[x, y] = LandscapeType_OverView.Spaces;
            }
          }
        }
      }
      return mapAfter;
    }

    LandscapeType_OverView[,] Reduction(LandscapeType_OverView[,] mapBefore)
    {
      LandscapeType_OverView[,] mapAfter = new LandscapeType_OverView[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
      int a = 0b0101, b = 0b0100, c = 0b0110, d = 0b0010, e = 0b1010, f = 0b1000, g = 0b1001, h = 0b0001;
      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          var position = 0b00000;
          //左辺
          if (x == 0) position = position | 0b0001;
          //右辺
          if (x == LegoData.LANDSCAPE_MAP_WIDTH-1) position = position | 0b0010;
          //上辺
          if (y == 0) position = position | 0b0100;
          //下辺
          if (y == LegoData.LANDSCAPE_MAP_HEIGHT-1) position = position | 0b1000;

          /*
          ポジションから見た周囲のセル
          -------------
          | 1 | 2 | 3 |
          |------------
          | 8 | p | 4 |
          |------------
          | 7 | 6 | 5 |
          |------------

          膨張・収縮処理の例外部分
          ------------------------
          | a |      b       | c |
          |---|--------------|----
          |   |              |   |
          | h |              | d |
          |   |              |   |
          |---|--------------|----
          | g |       f      | e |
          ------------------------
          */
          if (position != 0b0000)
          {
            LandscapeType_OverView cell = LandscapeType_OverView.Water;
            //1 -> d, e, f
            if (position == d || position == e || position == f)
              if (mapBefore[x - 1, y - 1] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            //2 -> d, e, f, g, h
            if (position == d || position == e || position == f || position == g || position == h)
              if (mapBefore[x, y - 1] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            //3 -> f,g,h
            if (position == f || position == g || position == h)
              if (mapBefore[x + 1, y - 1] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            //4 -> a, b, f, g, h
            if (position == a || position == b || position == f || position == g || position == h)
              if (mapBefore[x + 1, y] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            //5 -> a, b, h
            if (position == a || position == b || position == h)
              if (mapBefore[x + 1, y + 1] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            //6 -> a, b, c, d, h
            if (position == a || position == b || position == c || position == d || position == h)
              if (mapBefore[x, y + 1] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            //7 -> b, c, d
            if (position == b || position == c || position == d)
              if (mapBefore[x - 1, y + 1] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            //8 -> b, c, d, e, f
            if (position == b || position == c || position == d || position == e || position == f)
              if (mapBefore[x - 1, y] == LandscapeType_OverView.Spaces) cell = LandscapeType_OverView.Spaces;
            mapAfter[x, y] = cell;
          }
          else
          {
            if (mapBefore[x - 1, y - 1] == LandscapeType_OverView.Spaces || mapBefore[x, y - 1] == LandscapeType_OverView.Spaces || mapBefore[x + 1, y - 1] == LandscapeType_OverView.Spaces ||
            mapBefore[x - 1, y] == LandscapeType_OverView.Spaces || mapBefore[x + 1, y] == LandscapeType_OverView.Spaces ||
            mapBefore[x - 1, y + 1] == LandscapeType_OverView.Spaces || mapBefore[x, y + 1] == LandscapeType_OverView.Spaces || mapBefore[x + 1, y + 1] == LandscapeType_OverView.Spaces)
            {
              mapAfter[x, y] = LandscapeType_OverView.Spaces;
            }
            else
            {
              mapAfter[x, y] = LandscapeType_OverView.Water;
            }
          }
        }
      }
      return mapAfter;
    }
  }

  int[,] Labeling(LandscapeType_OverView[,] overviewMap)
  {
    labelCount = 0;
    int[,] labelMap = new int[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        labelMap[x, y] = -1;
      }
    }
    Labeling_Wrap();
    return labelMap;

    void Labeling_Wrap()
    {
      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          if (overviewMap[x, y] != LandscapeType_OverView.Water)
          {
            overviewMap[x, y] = LandscapeType_OverView.Spaces;
          }
          else
          {
            Labeling_Main(x, y);
            if (labelMap[x, y] == labelCount) labelCount++;
          }
        }
      }
    }

    void Labeling_Main(int x, int y)
    {
      if (labelMap[x, y] != -1) return;
      else labelMap[x, y] = labelCount;

      overviewMap[x, y] = LandscapeType_OverView.Water;

      if (0 < y - 1 && overviewMap[x, y - 1] == LandscapeType_OverView.Water) Labeling_Main(x, y - 1);
      if (x + 1 < LegoData.LANDSCAPE_MAP_WIDTH && overviewMap[x + 1, y] == LandscapeType_OverView.Water) Labeling_Main(x + 1, y);
      if (y + 1 < LegoData.LANDSCAPE_MAP_HEIGHT && overviewMap[x, y + 1] == LandscapeType_OverView.Water) Labeling_Main(x, y + 1);
      if (0 < x - 1 && overviewMap[x - 1, y] == LandscapeType_OverView.Water) Labeling_Main(x - 1, y);
    }
  }
}