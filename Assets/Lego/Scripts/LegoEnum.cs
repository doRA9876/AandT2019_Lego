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

  enum LandscapeType_OverView
  {
    Building, Water, Nature, Road, Spaces,
  }

  enum LandscapeType_Details
  {                                                                                                           //LandscapeType_Overview
    House, Shop, Skyscraper,                                                                    //Building
    River_Straight, River_Curve, River_Intersection_T, Sea,                                         //Water
    Forest, Park,                                                                                 //Nature
    Road_Straight, Road_Curve, Road_Intersection_T, Road_Intersection_X, Road_Stop, Road_CrossWalk, Bridge,   //Road
    Space                                                                                                     //Spaces
  }
  enum Direction
  {
    North, South, East, West
  }
}