using UnityEngine;

public static class LegoObjects
{
  private static bool isLoaded = false;

  public static bool IsLoaded{
    get{return isLoaded;}
  }

  //Modern
  //Road
  public static GameObject modern_road_straight, modern_road_intersection_T, modern_road_intersection_X, modern_road_curve, modern_road_stop, modern_road_crossWalk, modern_bridge;

  //Building
  public static GameObject modern_building_1, modern_building_2;
  public static GameObject eiffelTower;

  //Middle
  public static GameObject middle_building_1;

  //Fantasy


  //Water
  public static GameObject river_straight, river_curve, river_intersection_T, pond, sea;

  //Nature
  public static GameObject forest_1;

  //Space
  public static GameObject space;

  public static void LoadGameObjects()
  {
    //Modern
    //Road
    modern_road_straight = (GameObject)Resources.Load("Modern/Road/Road_Straight");
    modern_road_intersection_T = (GameObject)Resources.Load("Modern/Road/Road_Intersection_T");
    modern_road_intersection_X = (GameObject)Resources.Load("Modern/Road/Road_Intersection_X");
    modern_road_curve = (GameObject)Resources.Load("Modern/Road/Road_Curve");
    modern_road_stop = (GameObject)Resources.Load("Modern/Road/Underpass");//(GameObject)Resources.Load("Modern/Road/Road_Stop");
        modern_road_crossWalk = (GameObject)Resources.Load("Modern/Road/Road_Crosswalk");
    modern_bridge = (GameObject)Resources.Load("Modern/Road/Bridge");

    //Building
    modern_building_1 = (GameObject)Resources.Load("Modern/Building/Building_1");
    modern_building_2 = (GameObject)Resources.Load("Modern/Building/Building_2");
    eiffelTower = (GameObject)Resources.Load("Modern/Building/EiffelTower");

    //Middle
    modern_building_1 = (GameObject)Resources.Load("Middle/Building/Building_1");
    //Fantasy

    //Water
    river_straight = (GameObject)Resources.Load("Water/River_Straight");
    river_curve = (GameObject)Resources.Load("Water/River_Curve");
    river_intersection_T = (GameObject)Resources.Load("Water/River_Intersection_T");
    sea = (GameObject)Resources.Load("Water/Sea");
    pond = (GameObject)Resources.Load("Water/Pond");

    //Nature
    forest_1 = (GameObject)Resources.Load("Nature/Forest_1");

    //Space
    space = (GameObject)Resources.Load("Space/Space");

    isLoaded = true;
  }
}
