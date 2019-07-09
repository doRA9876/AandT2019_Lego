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
    Building, Water, Nature, Road, Spaces,
  }

  public enum LandscapeType_Details
  {                                                                                                                                          //LandscapeType_Overview
    House, Shop, Skyscraper,                                                                                                                 //Building
    River_Straight, River_Curve, River_Intersection_T, Sea, Pond,                                                                            //Water
    Forest, Park,                                                                                                                            //Nature
    Road_Straight, Road_Curve, Road_Intersection_T, Road_Intersection_X, Road_Underpass, Road_Tunnel, Road_Stop, Road_CrossWalk, Bridge,     //Road
    Space                                                                                                                                    //Spaces
  }
  public enum Direction
  {
    North, South, East, West
  }
}