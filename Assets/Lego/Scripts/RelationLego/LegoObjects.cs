using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LegoObjects
{
  private static bool isLoaded = false;

  public static bool IsLoaded{
    get{return isLoaded;}
  }

  //Modern
  //Road
  public static GameObject road_straight, road_intersection_T, road_intersection_X, road_curve, road_tunnel, road_underpass, road_stop, road_crossWalk, bridge;

  //Building
  public static GameObject building_house1,
                           building_house2,
                           building_house3,
                           building_house4,
                           building_house5, 
                           building_severalFloors, skyscraper;

  //Middle
  //public static GameObject middle_building_1;

  //Fantasy


  //Water
  public static GameObject river_straight, river_curve, river_intersection_T, river_intersection_X, pond, sea;

  //Nature
  public static GameObject forest_1;

  //Space
  public static GameObject space;

  public static void LoadGameObjects()
  {
    //Modern
    //Road
    road_straight = (GameObject)Resources.Load("Modern/Road/Road_Straight");
    road_intersection_T = (GameObject)Resources.Load("Modern/Road/Road_Intersection_T");
    road_intersection_X = (GameObject)Resources.Load("Modern/Road/Road_Intersection_X");
    road_curve = (GameObject)Resources.Load("Modern/Road/Road_Curve");
    road_tunnel = (GameObject)Resources.Load("Modern/Road/Tunnel");
    road_underpass = (GameObject)Resources.Load("Modern/Road/Underpass");
    road_stop = (GameObject)Resources.Load("Modern/Road/Road_Stop");
    road_crossWalk = (GameObject)Resources.Load("Modern/Road/Road_Crosswalk");
    bridge = (GameObject)Resources.Load("Modern/Road/Bridge");

    //Building
    //RandomLoadBuildings();
    building_house1 = (GameObject)Resources.Load("Modern/Building/House_1");
    building_house2 = (GameObject)Resources.Load("Modern/Building/House_2");
    building_house3 = (GameObject)Resources.Load("Modern/Building/House_3");
    building_house4 = (GameObject)Resources.Load("Modern/Building/House_4");
    building_house5 = (GameObject)Resources.Load("Modern/Building/House_5");

    building_severalFloors = (GameObject)Resources.Load("Modern/Building/Building_2");
    skyscraper = (GameObject)Resources.Load("Modern/Building/skyscraper");

    //Middle
    //building_house = (GameObject)Resources.Load("Middle/Building/Building_1");
    //Fantasy

    //Water
    river_straight = (GameObject)Resources.Load("Water/River_Straight");
    river_curve = (GameObject)Resources.Load("Water/River_Curve");
    river_intersection_T = (GameObject)Resources.Load("Water/River_Intersection_T");
    river_intersection_X = (GameObject)Resources.Load("Water/River_Intersection_X");
    sea = (GameObject)Resources.Load("Water/Sea");
    pond = (GameObject)Resources.Load("Water/Pond");

    //Nature
    forest_1 = (GameObject)Resources.Load("Nature/Forest_1");

    //Space
    space = (GameObject)Resources.Load("Space/Space");

    isLoaded = true;

    /*void RandomLoadBuildings()
    {
        switch (random)
        {
            case 0:
                building_house = (GameObject)Resources.Load("Modern/Building/House_1");
                break;

            case 1:
                building_house = (GameObject)Resources.Load("Modern/Building/House_2");
                break;

            case 2:
                building_house = (GameObject)Resources.Load("Modern/Building/House_3");
                break;

            case 3:
                building_house = (GameObject)Resources.Load("Modern/Building/House_4");
                break;

            case 4:
                building_house = (GameObject)Resources.Load("Modern/Building/House_5");
                break;

            default:
                building_house = (GameObject)Resources.Load("Space/Space");
                break;
        }
    }*/
  }
}
