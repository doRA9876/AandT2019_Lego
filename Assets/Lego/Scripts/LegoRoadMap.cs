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
    RoadLegoInfo[,] roadMap = new RoadLegoInfo[LegoData.LANDSCAPE_MAP_WIDTH, LegoData.LANDSCAPE_MAP_HEIGHT];

    public LegoRoadMap (LandscapeLegoInfo[,] landscapeMap)
    {
        CreateRoadMap(landscapeMap);
    }

    void CreateRoadMap (LandscapeLegoInfo[,] landscapeMap)
    {
        for (int y = 0; y < LegoData.LANDSCAPE_MAP_HEIGHT; y++)
        {
            for (int x = 0; x < LegoData.LANDSCAPE_MAP_WIDTH; x++)
            {
                roadMap[x, y] = new RoadLegoInfo();

                if (landscapeMap[x, y].overView == LandscapeType_OverView.Road)
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