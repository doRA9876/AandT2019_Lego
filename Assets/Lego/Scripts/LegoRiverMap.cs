using LegoEnum;
using UnityEngine;
using System.Collections.Generic;

internal struct RiverLegoInfo
{
  public LandscapeType_Details detail;
  public Direction direction;
}

internal class LegoRiverMap
{
  private RiverLegoInfo[,] riverMap;
  private LandscapeLegoInfo[,] landscapeMap;
  private int labelCount;

  internal LegoRiverMap(LandscapeLegoInfo[,] map)
  {
    landscapeMap = new LandscapeLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        landscapeMap[x, y] = new LandscapeLegoInfo();
        this.landscapeMap[x, y].detail = map[x, y].detail;
        this.landscapeMap[x, y].direction = map[x, y].direction;
        this.landscapeMap[x, y].overView = map[x, y].overView;
        this.landscapeMap[x, y].south = map[x, y].south;
        this.landscapeMap[x, y].north = map[x, y].north;
        this.landscapeMap[x, y].east = map[x, y].east;
        this.landscapeMap[x, y].west = map[x, y].west;
        this.landscapeMap[x, y].height = map[x, y].height;
      }
    }
    riverMap = new RiverLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    CreateRiverMap();
  }

  public RiverLegoInfo[,] GetRiverLegoMap()
  {
    return this.riverMap;
  }

  void CreateRiverMap()
  {
    LandscapeType_OverView[,] waterOverviewMap = new LandscapeType_OverView[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    int[,] label = new int[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    int iterationNumber = 0;
    Init();
    label = Labeling(waterOverviewMap);
    while (labelCount > 1)
    {

      for (int i = 0; i < iterationNumber; i++)
      {
        waterOverviewMap = Expansion(waterOverviewMap);
      }
      for (int i = 0; i < iterationNumber; i++)
      {
        waterOverviewMap = Reduction(waterOverviewMap);
      }
      label = Labeling(waterOverviewMap);
      iterationNumber++;
    }
    UpdateLandscapeMap_overview(waterOverviewMap);
    UpdateDirection();
    SetWaterDetail();

    void Init()
    {
      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          if (landscapeMap[x, y].height == 1)
            waterOverviewMap[x, y] = landscapeMap[x, y].overView;
          else
            waterOverviewMap[x, y] = LandscapeType_OverView.Spaces;
        }
      }
    }
  }

  void UpdateDirection()
  {
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        if (x == 0 || y == 0)
        {
          if (x == 0 && y == 0) continue;
          else if (x == 0) landscapeMap[x, y].north = landscapeMap[x, y - 1].overView;
          else if (y == 0) landscapeMap[x, y].west = landscapeMap[x - 1, y].overView;
        }
        else
        {
          landscapeMap[x, y].west = landscapeMap[x - 1, y].overView;
          landscapeMap[x - 1, y].east = landscapeMap[x, y].overView;
          landscapeMap[x, y].north = landscapeMap[x, y - 1].overView;
          landscapeMap[x, y - 1].south = landscapeMap[x, y].overView;
        }
      }
    }
  }

  void UpdateLandscapeMap_overview(LandscapeType_OverView[,] overview)
  {
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        if (overview[x, y] == LandscapeType_OverView.Water)
        {
          landscapeMap[x, y].overView = overview[x, y];
        }
      }
    }
  }

  LandscapeType_OverView[,] Expansion(LandscapeType_OverView[,] mapBefore)
  {
    LandscapeType_OverView[,] mapAfter = new LandscapeType_OverView[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    int up = 0b0100, down = 0b1000, left = 0b0001, right = 0b0010;
    int a = up + left, b = up, c = up + right, d = right, e = right + down, f = down, g = down + left, h = left;
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        var position = 0b00000;
        //左辺
        if (x == 0) position = position | left;
        //右辺
        if (x == LegoData.LANDSCAPE_MAP_WIDTH - 1) position = position | right;
        //上辺
        if (y == 0) position = position | up;
        //下辺
        if (y == LegoData.LANDSCAPE_MAP_HEIGHT - 1) position = position | down;

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
        //0b0000 -> 淵部分ではない
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
    int up = 0b0100, down = 0b1000, left = 0b0001, right = 0b0010;
    int a = up + left, b = up, c = up + right, d = right, e = right + down, f = down, g = down + left, h = left;
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        var position = 0b00000;
        //左辺
        if (x == 0) position = position | left;
        //右辺
        if (x == LegoData.LANDSCAPE_MAP_WIDTH - 1) position = position | right;
        //上辺
        if (y == 0) position = position | up;
        //下辺
        if (y == LegoData.LANDSCAPE_MAP_HEIGHT - 1) position = position | down;

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

        例：セル１はd, e, fの時だけ処理可能
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

  private void SetWaterDetail()
  {
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        if (landscapeMap[x, y].overView != LandscapeType_OverView.Water)
        {
          riverMap[x, y].detail = LandscapeType_Details.Space;
          riverMap[x, y].direction = Direction.North;
          continue;
        }

        int arround = CheckArroundCell(x, y);
        /*
        arround => 周囲のwatercellの有無を示す8桁のビット列        
        右(小さい位)から、
        北・南・東・西・北西・北東・南東・南西
        を示す。 
        */

        switch (arround)
        {
          //直線南北
          case 0b0011:
            riverMap[x, y].detail = LandscapeType_Details.River_Straight;
            riverMap[x, y].direction = Direction.North;
            break;

          //直線東西
          case 0b1100:
            riverMap[x, y].detail = LandscapeType_Details.River_Straight;
            riverMap[x, y].direction = Direction.East;
            break;

          //カーブ
          case 0b1010:
          case 0b1001:
          case 0b0110:
          case 0b0101:
            riverMap[x, y].detail = LandscapeType_Details.River_Curve;
            Direction direction_C = Direction.North;
            switch (arround)
            {
              case 0b1010: direction_C = Direction.South; break;
              case 0b1001: direction_C = Direction.West; break;
              case 0b0110: direction_C = Direction.East; break;
              case 0b0101: direction_C = Direction.North; break;
            }
            riverMap[x, y].direction = direction_C;
            break;

          //T字路
          case 0b0111:
          case 0b1011:
          case 0b1101:
          case 0b1110:
            riverMap[x, y].detail = LandscapeType_Details.River_Intersection_T;
            Direction direction_T = Direction.North;
            switch (arround)
            {
              case 0b0111: direction_C = Direction.West; break;
              case 0b1011: direction_C = Direction.East; break;
              case 0b1101: direction_C = Direction.South; break;
              case 0b1110: direction_C = Direction.North; break;

            }
            riverMap[x, y].direction = direction_T;
            break;

          //周りがすべて水なので海
          case 0b11111111:

          default:
            riverMap[x, y].detail = LandscapeType_Details.River_Straight;
            Direction direction_I = Direction.North;
            switch (arround)
            {
              case 0b0001: direction_I = Direction.North; break;
              case 0b0010: direction_I = Direction.North; break;
              case 0b0100: direction_I = Direction.North; break;
              case 0b1000: direction_I = Direction.North; break;
            }
            riverMap[x, y].direction = direction_I;
            break;
        }
      }
    }

    int CheckArroundCell(int x, int y)
    {
      int up = 0b0100, down = 0b1000, left = 0b0001, right = 0b0010;
      int a = up + left, b = up, c = up + right, d = right, e = right + down, f = down, g = down + left, h = left;
      int arround = 0b00000000;
      if (landscapeMap[x, y].north == LandscapeType_OverView.Water) arround = arround | 0b00000001;
      if (landscapeMap[x, y].south == LandscapeType_OverView.Water) arround = arround | 0b00000010;
      if (landscapeMap[x, y].east == LandscapeType_OverView.Water) arround = arround | 0b00000100;
      if (landscapeMap[x, y].west == LandscapeType_OverView.Water) arround = arround | 0b00001000;


      int position = 0b0000;
      //左辺
      if (x == 0) position = position | left;
      //右辺
      if (x == LegoData.LANDSCAPE_MAP_WIDTH - 1) position = position | right;
      //上辺
      if (y == 0) position = position | up;
      //下辺
      if (y == LegoData.LANDSCAPE_MAP_HEIGHT - 1) position = position | down;

      /*
      ポジションから見た周囲のセル
      -------------
      | 1 |   | 2 |
      |------------
      |   | p |   |
      |------------
      | 4 |   | 3 |
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

      例：セル１はd, e, fの時だけ処理可能
      */
      if (position != 0b0000)
      {
        //1 -> d, e, f
        if (position == d || position == e || position == f)
          if (landscapeMap[x - 1, y - 1].overView == LandscapeType_OverView.Water) arround = arround | 0b00010000;
        //2 -> f,g,h
        if (position == f || position == g || position == h)
          if (landscapeMap[x + 1, y - 1].overView == LandscapeType_OverView.Water) arround = arround | 0b00100000;
        //3 -> a, b, h
        if (position == a || position == b || position == h)
          if (landscapeMap[x + 1, y + 1].overView == LandscapeType_OverView.Water) arround = arround | 0b01000000;
        //4 -> b, c, d
        if (position == b || position == c || position == d)
          if (landscapeMap[x - 1, y + 1].overView == LandscapeType_OverView.Water) arround = arround | 0b10000000;
      }
      else
      {
        if (landscapeMap[x - 1, y - 1].overView == LandscapeType_OverView.Water) arround = arround | 0b00010000;
        if (landscapeMap[x + 1, y - 1].overView == LandscapeType_OverView.Water) arround = arround | 0b00100000;
        if (landscapeMap[x + 1, y + 1].overView == LandscapeType_OverView.Water) arround = arround | 0b01000000;
        if (landscapeMap[x - 1, y + 1].overView == LandscapeType_OverView.Water) arround = arround | 0b10000000;
      }
      return arround;
    }
  }
}