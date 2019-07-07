using LegoEnum;
using UnityEngine;

class RoadLegoInfo
{
    public LandscapeType_OverView overView;
    public LandscapeType_Details detail;
    public Direction direction;

    public RoadLegoInfo()
    {
        overView = LandscapeType_OverView.Spaces;
        detail = LandscapeType_Details.Space;
        direction = Direction.North;
    }
}

class LegoRoadMap
{
    RoadLegoInfo[,] roadMap;
    LandscapeLegoInfo[,] landscapeMap;

    public LegoRoadMap (LandscapeLegoInfo[,] map)
    {
        roadMap = new RoadLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];
        landscapeMap = map;
        CreateRoadMap();
    }

    void CreateRoadMap ()
    {
        for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
        {
            for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
            {
                roadMap[x, y] = new RoadLegoInfo();

                if (landscapeMap[x, y].overView == LandscapeType_OverView.Road && landscapeMap[x, y].detail != LandscapeType_Details.Space)
                {
                    roadMap[x, y].overView = landscapeMap[x, y].overView;
                    roadMap[x, y].detail = landscapeMap[x, y].detail;
                    roadMap[x, y].direction = landscapeMap[x, y].direction;

                    Debug.Log(roadMap[x, y].detail);
                }
            }
        }
    }
}