using UnityEngine;
using LegoEnum;

enum Age
{
  Modern, Middle, Fantasy,
}

class LandscapeLegoInfo
{
  public LandscapeType_OverView overView;
  public LandscapeType_OverView north, south, east, west;
  public LandscapeType_Details detail;
  public int height;
  public Direction direction;
  static bool[] setedBuild = new bool[6] { true, true, true, true, true, true };

  public LandscapeLegoInfo()
  {

  }

  public LandscapeLegoInfo(LegoColor lc)
  {
    SetLegoType_OverView(lc);
    detail = LandscapeType_Details.Space;
    north = south = east = west = LandscapeType_OverView.Spaces;
    direction = Direction.North;
    height = 0;
  }

  void SetLegoType_OverView(LegoColor legoColor)
  {
    switch (legoColor)
    {
      case LegoColor.Blue:
        overView = LandscapeType_OverView.Water;
        break;

      case LegoColor.Green:
        overView = LandscapeType_OverView.Nature;
        break;

      case LegoColor.Red:
        overView = LandscapeType_OverView.Building;
        break;

      case LegoColor.White:
        overView = LandscapeType_OverView.Road;
        break;

      /*case LegoColor.None:
        overView = LandscapeType_OverView.Spaces;
        break;*/

      default:
        overView = LandscapeType_OverView.Spaces;
        break;
    }
  }

  public GameObject GetLegoObject()
  {
    GameObject obj = LegoObjects.space;

    switch (this.overView)
    {
      case LandscapeType_OverView.Building:
        obj = GetBuildingObject();
        break;

      case LandscapeType_OverView.Road:
        obj = GetRoadObject();
        break;

      case LandscapeType_OverView.Water:
        obj = GetWaterObject();
        break;

      case LandscapeType_OverView.Nature:
        obj = GetNatureObject();
        break;

      default:
        obj = RandomSpaces();
        break;
    }
    return obj;

    GameObject RandomSpaces()
    {
            int randomNum = Random.Range(0, 4);

            switch (randomNum)
            {
                case 0:
                    return LegoObjects.space_2;

                case 1:
                    return LegoObjects.space;

                case 2:
                    return LegoObjects.space;

                case 3:
                    return LegoObjects.space;

                default:
                    return LegoObjects.space;
            }
    }

    GameObject GetBuildingObject()
    {
      switch (this.detail)
      {
        case LandscapeType_Details.House:
          return GetRandomHouse();

        case LandscapeType_Details.SeveralFloors2:
          return GetRandomSeveralFloors2();

        case LandscapeType_Details.SeveralFloors3:
          return GetRandomSeveralFloors3();

        case LandscapeType_Details.SeveralFloors4:
          return GetRandomSeveralFloors4();

        case LandscapeType_Details.Skyscraper:
          return GetRandomSkyscrapers();

        default:
          return LegoObjects.space_2;
      }
    }

    GameObject GetRandomHouse()
    {
        int randomNum = Random.Range(0, 5);

        switch (randomNum)
        {
                case 0:
                    return LegoObjects.building_house1;

                case 1:
                    return LegoObjects.building_house2;

                case 2:
                    return LegoObjects.building_house3;

                case 3:
                    return LegoObjects.building_house4;

                case 4:
                    return LegoObjects.building_house5;

                default:
                    return LegoObjects.space_2;
        }
    }

    GameObject GetRandomSeveralFloors2()
    {
            int randomNum = Random.Range(0, 5);

            switch (randomNum)
            {
                case 0:
                    return LegoObjects.building_severalFloors1_1;

                case 1:
                    return LegoObjects.building_severalFloors1_2;

                case 2:
                    return LegoObjects.building_severalFloors1_3;

                case 3:
                    return LegoObjects.building_severalFloors1_4;

                case 4:
                    return LegoObjects.building_severalFloors1_5;

                default:
                    return LegoObjects.space_2;
            }
    }

    GameObject GetRandomSeveralFloors3()
    {
            int randomNum = Random.Range(0, 5);

            switch (randomNum)
            {
                case 0:
                    return LegoObjects.building_severalFloors2_1;

                case 1:
                    return LegoObjects.building_severalFloors2_2;

                case 2:
                    return LegoObjects.building_severalFloors2_3;

                case 3:
                    return LegoObjects.building_severalFloors2_4;

                case 4:
                    return LegoObjects.building_severalFloors2_5;

                default:
                    return LegoObjects.space_2;
            }
    }

    GameObject GetRandomSeveralFloors4()
    {
            int randomNum = Random.Range(0, 5);

            switch (randomNum)
            {
                case 0:
                    return LegoObjects.building_severalFloors3_1;

                case 1:
                    return LegoObjects.building_severalFloors3_2;

                case 2:
                    return LegoObjects.building_severalFloors3_3;

                case 3:
                    return LegoObjects.building_severalFloors3_4;

                case 4:
                    return LegoObjects.building_severalFloors3_5;

                default:
                    return LegoObjects.space_2;
            }
    }

    GameObject GetRandomSkyscrapers()
    {
            int randomNum = Random.Range(0, 6);

            if (randomNum == 0 && setedBuild[0] == true)
            {
                setedBuild[0] = false;
                return LegoObjects.skyscraper_1;
            }
            else if (randomNum == 1 && setedBuild[1] == true)
            {
                setedBuild[1] = false;
                return LegoObjects.skyscraper_2;
            }
            else if (randomNum == 2 && setedBuild[2] == true)
            {
                setedBuild[2] = false;
                return LegoObjects.skyscraper_3;
            }
            else if (randomNum == 3 && setedBuild[3] == true)
            {
                setedBuild[3] = false;
                return LegoObjects.skyscraper_4;
            }
            else if (randomNum == 4 && setedBuild[4] == true)
            {
                setedBuild[4] = false;
                return LegoObjects.skyscraper_5;
            }
            else if (randomNum == 5 && setedBuild[5] == true)
            {
                setedBuild[5] = false;
                return LegoObjects.eiffel_tower;
            }
            else
            {
                setedBuild[0] = true;
                setedBuild[1] = true;
                setedBuild[2] = true;
                setedBuild[3] = true;
                setedBuild[4] = true;

                return LegoObjects.skyscraper_1;
            }

            /*switch (randomNum)
            {
                case 0:
                    return LegoObjects.skyscraper_1;

                case 1:
                    return LegoObjects.skyscraper_2;

                case 2:
                    return LegoObjects.skyscraper_3;

                case 3:
                    return LegoObjects.skyscraper_4;

                case 4:
                    return LegoObjects.skyscraper_5;

                case 5:
                    return LegoObjects.eiffel_tower;

                default:
                    return LegoObjects.space;
            }*/
        }

    GameObject GetRoadObject()
    {
      switch (this.detail)
      {
        case LandscapeType_Details.Road_Straight:
          return LegoObjects.road_straight;

        case LandscapeType_Details.Road_Curve:
          return LegoObjects.road_curve;

        case LandscapeType_Details.Road_Intersection_T:
          return LegoObjects.road_intersection_T;

        case LandscapeType_Details.Road_Intersection_X:
          return LegoObjects.road_intersection_X;

        case LandscapeType_Details.Road_Underpass:
          return LegoObjects.road_underpass;

        case LandscapeType_Details.Road_Tunnel:
          return LegoObjects.road_tunnel;

        case LandscapeType_Details.Road_Stop:
          return LegoObjects.road_stop;

        case LandscapeType_Details.Road_CrossWalk:
          return LegoObjects.road_crossWalk;

        case LandscapeType_Details.Bridge:
          return LegoObjects.bridge;

        default:
          return LegoObjects.space;
      }
    }

    GameObject GetWaterObject()
    {
      switch (this.detail)
      {
        case LandscapeType_Details.River_Straight:
          return LegoObjects.river_straight;

        case LandscapeType_Details.River_Intersection_T:
          return LegoObjects.river_intersection_T;

        case LandscapeType_Details.River_Curve:
          return LegoObjects.river_curve;

        case LandscapeType_Details.Sea:
          return LegoObjects.sea;

        case LandscapeType_Details.Pond:
          return LegoObjects.pond;

        default:
          return LegoObjects.space;
      }
    }

    GameObject GetNatureObject()
    {
      switch (this.detail)
      {
        case LandscapeType_Details.Forest:
          return GetRandomForests();

        case LandscapeType_Details.Park:
          return GetRandomParks();//LegoObjects.park_1;

        default:
          return LegoObjects.space;
      }
    }

    GameObject GetRandomForests()
    {
            int randomNum = Random.Range(0, 3);

            switch (randomNum)
            {
                case 0:
                    return LegoObjects.forest_1;

                case 1:
                    return LegoObjects.forest_2;

                case 2:
                    return LegoObjects.forest_3;

                default:
                    return LegoObjects.space;
            }
        }

    GameObject GetRandomParks()
    {
            int randomNum = Random.Range(0, 2);

            switch (randomNum)
            {
                case 0:
                    return LegoObjects.park_1;

                case 1:
                    return LegoObjects.shrine_1;

                default:
                    return LegoObjects.space;
            }
    }
  }
}

public class LegoCreateLandscape : MonoBehaviour
{
  [SerializeField]
  private bool UseSaveData = false, isDebugMode = false, isPlayable = false;
  [SerializeField]
  private string SaveDataName = "";

  private LandscapeLegoInfo[,] landscapeLegoMap_ = new LandscapeLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
  //private LandscapeLegoInfo[,] roadMap = new LandscapeLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];//ロードマップ
  private LegoCreateTex legoCreateTex_;

  void Start()
  {
    LegoBlockInfo[,] legoBlockMap;
    if (UseSaveData)
    {
      legoBlockMap = JsonHelper_TwodimensionalArray.LoadJson<LegoBlockInfo>(SaveDataName + ".json");
    }
    else
    {
      legoBlockMap = LegoData.legoMap;
    }

    if(legoBlockMap == null)
    {
      Debug.LogError("No SaveData");
      UnityEditor.EditorApplication.isPlaying = false;
    }

    if (isDebugMode)
    {
      legoCreateTex_ = gameObject.GetComponent<LegoCreateTex>();
      legoCreateTex_.CreateTexture(legoBlockMap);

    }
    ConvertLegoBlockInfo2LandscapeInfo(legoBlockMap);
    LegoRiverMap legoRiverMap = new LegoRiverMap(landscapeLegoMap_);
    UpdateLandscapeMap();
    LegoRoadMap legoRoadMap = new LegoRoadMap(landscapeLegoMap_);
    CreateLandscape();

    if (isPlayable)
    {
      SetStartPlayerPosition();
    }
  }

  void ConvertLegoBlockInfo2LandscapeInfo(LegoBlockInfo[,] legoBlockMap)
  {
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        landscapeLegoMap_[x, y] = new LandscapeLegoInfo(legoBlockMap[x, y].legoColor);

        if (legoBlockMap[x, y].height == 0)
        {
          landscapeLegoMap_[x, y].height = 0;
          landscapeLegoMap_[x, y].overView = LandscapeType_OverView.Spaces;
          landscapeLegoMap_[x, y].detail = LandscapeType_Details.Space;
          continue;
        }

        landscapeLegoMap_[x, y].height = legoBlockMap[x, y].height;
        if (x == 0 || y == 0)
        {
          if (x == 0 && y == 0) continue;
          else if (x == 0) landscapeLegoMap_[x, y].north = landscapeLegoMap_[x, y - 1].overView;
          else if (y == 0) landscapeLegoMap_[x, y].west = landscapeLegoMap_[x - 1, y].overView;
        }
        else
        {
          landscapeLegoMap_[x, y].west = landscapeLegoMap_[x - 1, y].overView;
          landscapeLegoMap_[x - 1, y].east = landscapeLegoMap_[x, y].overView;
          landscapeLegoMap_[x, y].north = landscapeLegoMap_[x, y - 1].overView;
          landscapeLegoMap_[x, y - 1].south = landscapeLegoMap_[x, y].overView;
        }
      }
    }
  }

  /// <summary>
  /// 景観作成
  /// ・各オブジェクトの向きを指定
  /// ・各オブジェクトの種類の詳細を指定
  /// ・高さによる種類の変化を設定
  /// ・他オブジェクトとの隣接関係を設定
  /// 例：
  /// ・橋や道路、交差点など、高さが違う場合は立体交差は歩道橋など
  /// ・１マスのみの場合や孤立している場合のことも考える。
  /// ・２マス分の建物
  /// ・川や海、高さが違う場合は滝など
  /// ・水のマスが１マスのみの場合は噴水など
  /// </summary>
  void UpdateLandscapeMap()
  {
    RiverLegoInfo[,] riverMap = new RiverLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
    riverMap = new LegoRiverMap(landscapeLegoMap_).GetRiverLegoMap();

    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        if (riverMap[x, y].detail != LandscapeType_Details.Space)
        {
          landscapeLegoMap_[x, y].detail = riverMap[x, y].detail;
          landscapeLegoMap_[x, y].direction = riverMap[x, y].direction;
        }

        if (landscapeLegoMap_[x, y].height != 0)
        {
          switch (landscapeLegoMap_[x, y].overView)
          {
            case LandscapeType_OverView.Building:
              landscapeLegoMap_[x, y].detail = SetBuildingDetails(landscapeLegoMap_[x, y]);
              landscapeLegoMap_[x, y].direction = SetBuildingDirection(landscapeLegoMap_[x, y]);
              break;

            /*case LandscapeType_OverView.Water:
              landscapeLegoMap_[x, y].detail = SetWaterDetails(landscapeLegoMap_[x, y]);
              SetSeaDetails(x, y);
              landscapeLegoMap_[x, y].direction = SetWaterDirection(landscapeLegoMap_[x, y]);
              break;*/

            case LandscapeType_OverView.Nature:
              landscapeLegoMap_[x, y].detail = SetNatureDetails(landscapeLegoMap_[x, y]);
              landscapeLegoMap_[x, y].direction = SetNatureDirection(landscapeLegoMap_[x, y]);
              break;

            case LandscapeType_OverView.Road:
              landscapeLegoMap_[x, y].detail = SetRoadDetails(landscapeLegoMap_[x, y]);
              SetTunnelDetails(x, y);
              landscapeLegoMap_[x, y].direction = SetRoadDirection(landscapeLegoMap_[x, y]);
              break;

            case LandscapeType_OverView.Spaces:
              landscapeLegoMap_[x, y].detail = LandscapeType_Details.Space;
              landscapeLegoMap_[x, y].direction = Direction.North;
              break;

            default:
              break;
          }
        }
        else
        {
          landscapeLegoMap_[x, y].overView = LandscapeType_OverView.Spaces;
          landscapeLegoMap_[x, y].detail = LandscapeType_Details.Space;
          landscapeLegoMap_[x, y].direction = Direction.North;
        }

        if (landscapeLegoMap_[x, y].overView != LandscapeType_OverView.Spaces)
        {
          Debug.Log("x:" + x + " y:" + y + "OverView:" + landscapeLegoMap_[x, y].overView + " Detail:" + landscapeLegoMap_[x, y].detail + " Direction:" + landscapeLegoMap_[x, y].direction);
        }
      }
    }

    SetRoad_StopDtails();
  }

  LandscapeType_Details SetBuildingDetails(LandscapeLegoInfo landscapeLegoMap)
  {
    if (landscapeLegoMap.height == 2)
      return LandscapeType_Details.SeveralFloors2;
    else if (landscapeLegoMap.height == 3)
        return LandscapeType_Details.SeveralFloors3;
    else if (landscapeLegoMap.height == 4)
        return LandscapeType_Details.SeveralFloors4;
    else if (landscapeLegoMap.height >= 5)
        return LandscapeType_Details.Skyscraper;
    else
      return LandscapeType_Details.House;
  }

  Direction SetBuildingDirection(LandscapeLegoInfo landscapeLegoMap)
  {
    if (landscapeLegoMap.north == LandscapeType_OverView.Road)//北南西東の順に見ていき、Roadがあった方向を向きに設定
      return Direction.North;
    else if (landscapeLegoMap.south == LandscapeType_OverView.Road)
      return Direction.South;
    else if (landscapeLegoMap.east == LandscapeType_OverView.Road)
      return Direction.East;
    else if (landscapeLegoMap.west == LandscapeType_OverView.Road)
      return Direction.West;
    else
      return Direction.North;
  }

  /*LandscapeType_Details SetWaterDetails(LandscapeLegoInfo landscapeLegoMap)
  {
    int Count = 0;

    if (landscapeLegoMap.north == LandscapeType_OverView.Water)
      Count++;
    if (landscapeLegoMap.south == LandscapeType_OverView.Water)
      Count++;
    if (landscapeLegoMap.east == LandscapeType_OverView.Water)
      Count++;
    if (landscapeLegoMap.west == LandscapeType_OverView.Water)
      Count++;

        if (Count == 0)//東西南北のWaterの数で種類を判定
            return LandscapeType_Details.Pond;
        else if (Count == 1)
            return LandscapeType_Details.River_Straight;
        else if (Count == 2)
        {
            if ((landscapeLegoMap.north == LandscapeType_OverView.Water && landscapeLegoMap.south == LandscapeType_OverView.Water)
                || (landscapeLegoMap.east == LandscapeType_OverView.Water && landscapeLegoMap.west == LandscapeType_OverView.Water))
                return LandscapeType_Details.River_Straight;
            else
                return LandscapeType_Details.River_Curve;
        }
        else if (Count == 3)
            return LandscapeType_Details.River_Intersection_T;
        else if (Count == 4)
            return LandscapeType_Details.River_Straight;

    return LandscapeType_Details.Space;
  }

  void SetSeaDetails(int x, int y)//River_Curve, River_Intersection_Tが連続していたらSeaとしておく
  {
    if (landscapeLegoMap_[x, y].detail == LandscapeType_Details.River_Curve || landscapeLegoMap_[x, y].detail == LandscapeType_Details.River_Intersection_T)
    {
      if (landscapeLegoMap_[x - 1, y].detail == LandscapeType_Details.River_Curve || landscapeLegoMap_[x - 1, y].detail == LandscapeType_Details.River_Intersection_T
          || landscapeLegoMap_[x, y - 1].detail == LandscapeType_Details.River_Curve || landscapeLegoMap_[x, y - 1].detail == LandscapeType_Details.River_Intersection_T
          || landscapeLegoMap_[x + 1, y].detail == LandscapeType_Details.River_Curve || landscapeLegoMap_[x + 1, y].detail == LandscapeType_Details.River_Intersection_T
          || landscapeLegoMap_[x, y + 1].detail == LandscapeType_Details.River_Curve || landscapeLegoMap_[x, y + 1].detail == LandscapeType_Details.River_Intersection_T)
        landscapeLegoMap_[x, y].detail = LandscapeType_Details.Sea;
    }
  }

  Direction SetWaterDirection(LandscapeLegoInfo landscapeLegoMap)
  {
    //　□←ここ
    //□□□    River_Intersection_Tの一本出てる方を向きにする
    if (landscapeLegoMap.detail == LandscapeType_Details.River_Intersection_T)
    {
      if (landscapeLegoMap.south != LandscapeType_OverView.Water)
        return Direction.North;
      else if (landscapeLegoMap.north != LandscapeType_OverView.Water)
        return Direction.South;
      else if (landscapeLegoMap.west != LandscapeType_OverView.Water)
        return Direction.East;
      else if (landscapeLegoMap.east != LandscapeType_OverView.Water)
        return Direction.West;
    }

    //　□←ここ
    //□□      北・西River_Curveの北部分の向きにする
    if (landscapeLegoMap.detail == LandscapeType_Details.River_Curve)
    {
      if (landscapeLegoMap.north == LandscapeType_OverView.Water && landscapeLegoMap.east == LandscapeType_OverView.Water)
        return Direction.North;
      else if (landscapeLegoMap.south == LandscapeType_OverView.Water && landscapeLegoMap.west == LandscapeType_OverView.Water)
        return Direction.South;
      else if (landscapeLegoMap.east == LandscapeType_OverView.Water && landscapeLegoMap.south == LandscapeType_OverView.Water)
        return Direction.East;
      else if (landscapeLegoMap.west == LandscapeType_OverView.Water && landscapeLegoMap.north == LandscapeType_OverView.Water)
        return Direction.West;
    }

    //River_Straight、その他
    if (landscapeLegoMap.north == LandscapeType_OverView.Water)
      return Direction.North;
    else if (landscapeLegoMap.south == LandscapeType_OverView.Water)
      return Direction.South;
    else if (landscapeLegoMap.east == LandscapeType_OverView.Water)
      return Direction.East;
    else if (landscapeLegoMap.west == LandscapeType_OverView.Water)
      return Direction.West;
    else
      return Direction.North;
  }*/

  LandscapeType_Details SetNatureDetails(LandscapeLegoInfo landscapeLegoMap)
  {
    int Count = 0;
    if (landscapeLegoMap.north == LandscapeType_OverView.Nature)
      Count++;
    if (landscapeLegoMap.south == LandscapeType_OverView.Nature)
      Count++;
    if (landscapeLegoMap.east == LandscapeType_OverView.Nature)
      Count++;
    if (landscapeLegoMap.west == LandscapeType_OverView.Nature)
      Count++;

    if (Count == 0)//東西南北のNatureの数で種類を判定
      return LandscapeType_Details.Park;
    else
      return LandscapeType_Details.Forest;
  }

  Direction SetNatureDirection(LandscapeLegoInfo landscapeLegoMap)
  {
        if (landscapeLegoMap.detail == LandscapeType_Details.Forest)//Forestは全て北向き
            return Direction.North;
        else if (landscapeLegoMap.north == LandscapeType_OverView.Road)
            return Direction.North;
        else if (landscapeLegoMap.south == LandscapeType_OverView.Road)
            return Direction.South;
        else if (landscapeLegoMap.east == LandscapeType_OverView.Road)
            return Direction.East;
        else if (landscapeLegoMap.west == LandscapeType_OverView.Road)
            return Direction.West;
        else
            return Direction.North;
  }

  LandscapeType_Details SetRoadDetails(LandscapeLegoInfo landscapeLegoMap)
  {
    if ((landscapeLegoMap.north == LandscapeType_OverView.Water && landscapeLegoMap.south == LandscapeType_OverView.Water) || (landscapeLegoMap.east == LandscapeType_OverView.Water
        && landscapeLegoMap.west == LandscapeType_OverView.Water))//南北か東西がWaterで挟まれているときBridgeと判定
      return LandscapeType_Details.Bridge;

    int Count = 0;
    if (landscapeLegoMap.north == LandscapeType_OverView.Road)
      Count++;
    if (landscapeLegoMap.south == LandscapeType_OverView.Road)
      Count++;
    if (landscapeLegoMap.east == LandscapeType_OverView.Road)
      Count++;
    if (landscapeLegoMap.west == LandscapeType_OverView.Road)
      Count++;

    if (Count == 1)//東西南北のRoadの数で種類を判定
      return LandscapeType_Details.Road_Stop;
    else if (Count == 2)
    {
      if ((landscapeLegoMap.north == LandscapeType_OverView.Road && landscapeLegoMap.south == LandscapeType_OverView.Road)
          || (landscapeLegoMap.east == LandscapeType_OverView.Road && landscapeLegoMap.west == LandscapeType_OverView.Road))
        return LandscapeType_Details.Road_Straight;
      else
        return LandscapeType_Details.Road_Curve;
    }
    else if (Count == 3)
      return LandscapeType_Details.Road_Intersection_T;
    else if (Count == 4)
      return LandscapeType_Details.Road_Intersection_X;

    return LandscapeType_Details.Space;//一つだけあるRoadはSpaceにしておく
  }

  void SetTunnelDetails(int x, int y)
  {
        if (x == 0 || y == 0 || x == 15 || y == 15)
            landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Tunnel;
  }

  Direction SetRoadDirection(LandscapeLegoInfo landscapeLegoMap)
  {
    //　□←ここ
    //□□□    Road_Intersection_Tの一本出てる方を向きにする
    if (landscapeLegoMap.detail == LandscapeType_Details.Road_Intersection_T)
    {
      if (landscapeLegoMap.south != LandscapeType_OverView.Road)
        return Direction.North;
      else if (landscapeLegoMap.north != LandscapeType_OverView.Road)
        return Direction.South;
      else if (landscapeLegoMap.west != LandscapeType_OverView.Road)
        return Direction.East;
      else if (landscapeLegoMap.east != LandscapeType_OverView.Road)
        return Direction.West;
    }

    //　□←ここ
    //□□      北・西Road_Curveの北部分の向きにする
    if (landscapeLegoMap.detail == LandscapeType_Details.Road_Curve)
    {
      if (landscapeLegoMap.north == LandscapeType_OverView.Road && landscapeLegoMap.east == LandscapeType_OverView.Road)
        return Direction.North;
      else if (landscapeLegoMap.south == LandscapeType_OverView.Road && landscapeLegoMap.west == LandscapeType_OverView.Road)
        return Direction.South;
      else if (landscapeLegoMap.east == LandscapeType_OverView.Road && landscapeLegoMap.south == LandscapeType_OverView.Road)
        return Direction.East;
      else if (landscapeLegoMap.west == LandscapeType_OverView.Road && landscapeLegoMap.north == LandscapeType_OverView.Road)
        return Direction.West;
    }

    //Road_Straight、その他
    if (landscapeLegoMap.north == LandscapeType_OverView.Road)
      return Direction.North;
    else if (landscapeLegoMap.south == LandscapeType_OverView.Road)
      return Direction.South;
    else if (landscapeLegoMap.east == LandscapeType_OverView.Road)
      return Direction.East;
    else if (landscapeLegoMap.west == LandscapeType_OverView.Road)
      return Direction.West;
    else
      return Direction.North;
  }

  void SetRoad_StopDtails()
  {
    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        if (landscapeLegoMap_[x, y].detail == LandscapeType_Details.Road_Stop)
          CreateRoad_StopAfter(x, y);
      }
    }

    void CreateRoad_StopAfter(int x, int y)
    {
      if (x == 15 || y == 15 || x == 0 || y == 0)
      {
        landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Tunnel;
        return;
      }

      int[] DirectionCount = new int[4] { 0, 0, 0, 0 };
      if (landscapeLegoMap_[x, y].north == LandscapeType_OverView.Road)
        DirectionCount[0]++;
      else if (landscapeLegoMap_[x, y].south == LandscapeType_OverView.Road)
        DirectionCount[1]++;
      else if (landscapeLegoMap_[x, y].east == LandscapeType_OverView.Road)
        DirectionCount[2]++;
      else if (landscapeLegoMap_[x, y].west == LandscapeType_OverView.Road)
        DirectionCount[3]++;

      if (DirectionCount[0] == 1 && landscapeLegoMap_[x, y + 1].detail == LandscapeType_Details.Space)
      {
        landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Straight;

        landscapeLegoMap_[x, y + 1].overView = LandscapeType_OverView.Road;
        landscapeLegoMap_[x, y + 1].detail = LandscapeType_Details.Road_Underpass;
        landscapeLegoMap_[x, y + 1].direction = Direction.North;
        landscapeLegoMap_[x, y + 1].north = LandscapeType_OverView.Road;

        CreateRoad_StopAfter(x, y + 1);
      }
      else if (DirectionCount[1] == 1 && landscapeLegoMap_[x, y - 1].detail == LandscapeType_Details.Space)
      {
        landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Straight;

        landscapeLegoMap_[x, y - 1].overView = LandscapeType_OverView.Road;
        landscapeLegoMap_[x, y - 1].detail = LandscapeType_Details.Road_Underpass;
        landscapeLegoMap_[x, y - 1].direction = Direction.South;
        landscapeLegoMap_[x, y - 1].south = LandscapeType_OverView.Road;

        CreateRoad_StopAfter(x, y - 1);
      }
      else if (DirectionCount[2] == 1 && landscapeLegoMap_[x - 1, y].detail == LandscapeType_Details.Space)
      {
        landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Straight;

        landscapeLegoMap_[x - 1, y].overView = LandscapeType_OverView.Road;
        landscapeLegoMap_[x - 1, y].detail = LandscapeType_Details.Road_Underpass;
        landscapeLegoMap_[x - 1, y].direction = Direction.East;
        landscapeLegoMap_[x - 1, y].east = LandscapeType_OverView.Road;

        CreateRoad_StopAfter(x - 1, y);
      }
      else if (DirectionCount[3] == 1 && landscapeLegoMap_[x + 1, y].detail == LandscapeType_Details.Space)
      {
        landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Straight;

        landscapeLegoMap_[x + 1, y].overView = LandscapeType_OverView.Road;
        landscapeLegoMap_[x + 1, y].detail = LandscapeType_Details.Road_Underpass;
        landscapeLegoMap_[x + 1, y].direction = Direction.West;
        landscapeLegoMap_[x + 1, y].west = LandscapeType_OverView.Road;

        CreateRoad_StopAfter(x + 1, y);
      }
      else
        SetBuildingNearRoad(DirectionCount, x, y);
    }

    void SetBuildingNearRoad(int[] d, int x, int y)//Road_Stop周りにBuilding or ParkがあったらRoad_Stop、なかったらRoad_Underpass
        {
        int[] BuildingCount = new int[4] { 0, 0, 0, 0 };
        int[] ParkCount = new int[4] { 0, 0, 0, 0 };

        if (landscapeLegoMap_[x, y + 1].overView == LandscapeType_OverView.Building)
            BuildingCount[0]++;
        else if (landscapeLegoMap_[x, y - 1].overView == LandscapeType_OverView.Building)
            BuildingCount[1]++;
        else if (landscapeLegoMap_[x - 1, y].overView == LandscapeType_OverView.Building)
            BuildingCount[2]++;
        else if (landscapeLegoMap_[x + 1, y].overView == LandscapeType_OverView.Building)
            BuildingCount[3]++;

        if (landscapeLegoMap_[x, y + 1].detail == LandscapeType_Details.Park)
            ParkCount[0]++;
        else if (landscapeLegoMap_[x, y - 1].detail == LandscapeType_Details.Park)
            ParkCount[1]++;
        else if (landscapeLegoMap_[x - 1, y].detail == LandscapeType_Details.Park)
            ParkCount[2]++;
        else if (landscapeLegoMap_[x + 1, y].detail == LandscapeType_Details.Park)
            ParkCount[3]++;

            if (d[0] == 1 && (landscapeLegoMap_[x, y + 1].overView == LandscapeType_OverView.Building || landscapeLegoMap_[x, y + 1].detail == LandscapeType_Details.Park)
                && landscapeLegoMap_[x, y + 1].direction == Direction.North)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else if (d[1] == 1 && (landscapeLegoMap_[x, y - 1].overView == LandscapeType_OverView.Building || landscapeLegoMap_[x, y - 1].detail == LandscapeType_Details.Park)
                     && landscapeLegoMap_[x, y - 1].direction == Direction.South)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else if (d[2] == 1 && (landscapeLegoMap_[x - 1, y].overView == LandscapeType_OverView.Building || landscapeLegoMap_[x - 1, y].detail == LandscapeType_Details.Park)
                     && landscapeLegoMap_[x - 1, y].direction == Direction.East)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else if (d[3] == 1 && (landscapeLegoMap_[x + 1, y].overView == LandscapeType_OverView.Building || landscapeLegoMap_[x + 1, y].detail == LandscapeType_Details.Park)
                     && landscapeLegoMap_[x + 1, y].direction == Direction.West)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else if ((BuildingCount[0] == 1 || ParkCount[0] == 1) && landscapeLegoMap_[x, y + 1].direction == Direction.North)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else if ((BuildingCount[1] == 1 || ParkCount[1] == 1) && landscapeLegoMap_[x, y - 1].direction == Direction.South)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else if ((BuildingCount[0] == 1 || ParkCount[0] == 1) && landscapeLegoMap_[x - 1, y].direction == Direction.East)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else if ((BuildingCount[1] == 1 || ParkCount[1] == 1) && landscapeLegoMap_[x + 1, y].direction == Direction.West)
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Stop;
            else
                landscapeLegoMap_[x, y].detail = LandscapeType_Details.Road_Underpass;
    }
  }

  void CreateLandscape()
  {
    LegoObjects.LoadGameObjects();
    GameObject NewObj;

    for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
      {
        GameObject obj = landscapeLegoMap_[x, y].GetLegoObject();
        if (obj == null)
        {
          Debug.LogError(landscapeLegoMap_[x, y].detail + " is not exist.");
          obj = LegoObjects.space_2;
        }
        float rotationAngle;
        switch (landscapeLegoMap_[x, y].direction)
        {
          case Direction.North:
            rotationAngle = 0f;
            break;

          case Direction.East:
            rotationAngle = -90f;
            break;

          case Direction.West:
            rotationAngle = 90f;
            break;

          case Direction.South:
            rotationAngle = 180f;
            break;

          default:
            rotationAngle = 0f;
            break;
        }

        if (landscapeLegoMap_[x, y].overView == LandscapeType_OverView.Building)
        {
            NewObj = Instantiate(obj, new Vector3(x * LegoData.LANDSCAPE_OBJECT_WIDTH, 100f, y * LegoData.LANDSCAPE_OBJECT_HEIGHT), Quaternion.Euler(0, rotationAngle, 0));
            NewObj.tag = "Building";
        }
        else
            Instantiate(obj, new Vector3(x * LegoData.LANDSCAPE_OBJECT_WIDTH, 0f, y * LegoData.LANDSCAPE_OBJECT_HEIGHT), Quaternion.Euler(0, rotationAngle, 0));
      }
    }
  }

  void SetStartPlayerPosition()//プレイヤーの初期位置を決める
  {
    ViewSpot spotScript = gameObject.GetComponent<ViewSpot>();
    spotScript.Init();
    MovePosition();

    void MovePosition()//配置場所の優先順位:Road_Intersection_X > Road_Intersection_T > otherRoadDetails > Center: 優先順位の高いdetailが無ければ次の優先順位のdetailに配置する
    {
      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          if (landscapeLegoMap_[x, y].detail == LandscapeType_Details.Road_Intersection_X)
          {
            spotScript.Move(x, y);
            return;
          }
          else if (landscapeLegoMap_[x, y].detail == LandscapeType_Details.Road_Intersection_T)
          {
            spotScript.Move(x, y);
            return;
          }
        }
      }
      spotScript.MoveRandom();
    }
    /*
    void MovePosition()//配置場所の優先順位:Road_Intersection_X > Road_Intersection_T > otherRoadDetails > Center: 優先順位の高いdetailが無ければ次の優先順位のdetailに配置する
    {
      int[] TCount = new int[2] { -1, -1 }, otherCount = new int[2] { -1, -1 };
      for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
      {
        for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
        {
          if (landscapeLegoMap_[x, y].detail == LandscapeType_Details.Road_Intersection_X)
          {
            //player.transform.position = new Vector3((x * LegoData.LANDSCAPE_OBJECT_WIDTH) - 4.5f, 0f, (y * LegoData.LANDSCAPE_OBJECT_HEIGHT) - 4.5f);
            spotScript.MovePosition(x, y);
            return;
          }
          else if (landscapeLegoMap_[x, y].detail == LandscapeType_Details.Road_Intersection_T)
          {
            TCount[0] = x;
            TCount[1] = y;
          }
          else if (landscapeLegoMap_[x, y].overView == LandscapeType_OverView.Road && landscapeLegoMap_[x, y].detail != LandscapeType_Details.Space)
          {
            otherCount[0] = x;
            otherCount[1] = y;
          }
        }
      }
      /*
      if (TCount[0] != -1)
        player.transform.position = new Vector3((TCount[0] * LegoData.LANDSCAPE_OBJECT_WIDTH) - 4.5f, 0f, (TCount[1] * LegoData.LANDSCAPE_OBJECT_HEIGHT) - 4.5f);
      else if (otherCount[0] != -1)
        player.transform.position = new Vector3((otherCount[0] * LegoData.LANDSCAPE_OBJECT_WIDTH) - 4.5f, 0f, (otherCount[1] * LegoData.LANDSCAPE_OBJECT_HEIGHT) - 4.5f);
      else
        player.transform.position = new Vector3(0f, 0f, 0f);
        */
  }
}