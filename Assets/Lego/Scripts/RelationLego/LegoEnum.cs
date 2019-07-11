namespace LegoEnum
{
  public enum LegoColor
  {
    White, Green, Blue, Red, Yellow, YellowishGreen, Brown, Black, Orange, None
  }

  [System.Serializable]
  public struct LegoBlockInfo
  {
    public LegoColor legoColor;
    public int height;
  }

  public enum LandscapeType_OverView
  {
    Building, Water, Nature, Road, Spaces
  }

  public enum LandscapeType_Details
  {                                                                                                                                        //LandscapeType_Overview
    House, SeveralFloors2, SeveralFloors3, SeveralFloors4, Skyscraper, /*Eiffel_Tower,*/                                                   //Building
    River_Straight, River_Curve, River_Intersection_T, River_Intersection_X, Sea, Pond,                                                    //Water
    Forest, Park,                                                                                                                          //Nature
    Road_Straight, Road_Curve, Road_Intersection_T, Road_Intersection_X, Road_Stop, Road_CrossWalk, Bridge, Road_Underpass, Road_Tunnel,   //Road
    Space                                                                                                                                  //Spaces
  }
  public enum Direction
  {
    North, South, East, West
  }
}