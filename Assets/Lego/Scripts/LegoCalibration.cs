﻿/*
[TODO]
範囲がガバイ。
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LegoCalibration : MonoBehaviour
{
  private float timeLeft__1FPS_;
  private float timeLeft__15FPS_;
  private KinectManager manager_;
  private LegoBase lego_;
  [SerializeField] RawImage depthImage_;
  [SerializeField, Range(750f, 1000f)] float upperDisplayRange_;
  [SerializeField, Range(0f, 850f)] float lowerDisplayRange_;
  private static readonly int upperBasePixelDepthValue_ = 860;
  private static readonly int lowerBasePixelDepthValue_ = 840;
  private Texture2D depthTexture_;
  private ushort[] depthMap_;
  private List<BasePixelInfo> basePixelMap_;
  private List<Vector3> baseXYAndDepth_;
  private Vector3 baseCenter_;

  private struct BasePixelInfo
  {
    public int sectionNumber;
    public ushort depth;
    public int x, y;
  }

  // Start is called before the first frame update
  void Start()
  {
    if (depthImage_ == null)
    {
      Debug.LogError("[Depth Image] is not attaced.", depthImage_);
      Application.Quit();
    }

    manager_ = KinectManager.Instance;

    Vector2 inverseY = new Vector2(1, -1);
    depthImage_.GetComponent<Transform>().localScale *= inverseY;

    depthMap_ = new ushort[LegoData.DEPTH_CAMERA_WIDTH * LegoData.DEPTH_CAMERA_HEIGHT];

    depthTexture_ = new Texture2D(LegoData.DEPTH_CAMERA_WIDTH, LegoData.DEPTH_CAMERA_HEIGHT, TextureFormat.RGBA32, false);
    depthImage_.texture = depthTexture_;

    basePixelMap_ = new List<BasePixelInfo>();
    baseXYAndDepth_ = new List<Vector3>();
    timeLeft__1FPS_ = 1.0f;
    timeLeft__15FPS_ = 0.04f;
  }

  void Update()
  {
    timeLeft__1FPS_ -= Time.deltaTime;
    timeLeft__15FPS_ -= Time.deltaTime;

    if (!(manager_ && manager_.IsInitialized())) return;

    //15fps処理
    if (timeLeft__15FPS_ <= 0.0f)
    {
      timeLeft__15FPS_ = 0.04f;

      Texture2D colorTexture = null;

      colorTexture = manager_.GetUsersClrTex();
      depthMap_ = manager_.GetRawDepthMap();

      ScanFrom4EndPoint(colorTexture);
      depthTexture_.Apply();
    }

    //1fps処理
    if (timeLeft__1FPS_ <= 0.0f)
    {
      timeLeft__1FPS_ = 1.0f;
      baseXYAndDepth_.Clear();
      baseXYAndDepth_.Add(CalcBaseDepthAverage_And_Point(0));
      baseXYAndDepth_.Add(CalcBaseDepthAverage_And_Point(1));
      baseXYAndDepth_.Add(CalcBaseDepthAverage_And_Point(2));
      baseXYAndDepth_.Add(CalcBaseDepthAverage_And_Point(3));
      CalcCenterBaseDepth_And_Point();
      Debug.Log("XY0:" + (int)baseXYAndDepth_[0].x + ", " + (int)baseXYAndDepth_[0].y + " Depth value:" + (int)baseXYAndDepth_[0].z);
      Debug.Log("XY1:" + (int)baseXYAndDepth_[1].x + ", " + (int)baseXYAndDepth_[1].y + " Depth value:" + (int)baseXYAndDepth_[1].z);
      Debug.Log("XY2:" + (int)baseXYAndDepth_[2].x + ", " + (int)baseXYAndDepth_[2].y + " Depth value:" + (int)baseXYAndDepth_[2].z);
      Debug.Log("XY3:" + (int)baseXYAndDepth_[3].x + ", " + (int)baseXYAndDepth_[3].y + " Depth value:" + (int)baseXYAndDepth_[3].z);
      Debug.Log("Center XY:" + baseCenter_.x + ", " + baseCenter_.y + "Depth value:" + baseCenter_.z);
      basePixelMap_.Clear();
    }

    //4つの点からなる2つの直線の交点を求める。
    //http://imagingsolution.blog.fc2.com/blog-entry-137.html
    void CalcCenterBaseDepth_And_Point()
    {
      if (float.IsNaN(baseXYAndDepth_[0].x) || float.IsNaN(baseXYAndDepth_[1].x) || float.IsNaN(baseXYAndDepth_[2].x) || float.IsNaN(baseXYAndDepth_[3].x)) return;
      if (float.IsNaN(baseXYAndDepth_[0].y) || float.IsNaN(baseXYAndDepth_[1].y) || float.IsNaN(baseXYAndDepth_[2].y) || float.IsNaN(baseXYAndDepth_[3].y)) return;

      float s1 = ((baseXYAndDepth_[3].x - baseXYAndDepth_[1].x) * (baseXYAndDepth_[0].y - baseXYAndDepth_[1].y) - (baseXYAndDepth_[3].y - baseXYAndDepth_[1].y) * (baseXYAndDepth_[0].x - baseXYAndDepth_[1].x)) * 0.5f;
      s1 = Mathf.Abs(s1);
      float s2 = ((baseXYAndDepth_[3].x - baseXYAndDepth_[1].x) * (baseXYAndDepth_[1].y - baseXYAndDepth_[2].y) - (baseXYAndDepth_[3].y - baseXYAndDepth_[1].y) * (baseXYAndDepth_[1].x - baseXYAndDepth_[2].x)) * 0.5f;
      s2 = Mathf.Abs(s2);

      baseCenter_.x = baseXYAndDepth_[0].x + (baseXYAndDepth_[2].x - baseXYAndDepth_[0].x) * s1 / (s1 + s2);
      baseCenter_.y = baseXYAndDepth_[0].y + (baseXYAndDepth_[2].y - baseXYAndDepth_[0].y) * s1 / (s1 + s2);
      baseCenter_.z = depthMap_[(int)baseCenter_.y * LegoData.DEPTH_CAMERA_WIDTH + (int)baseCenter_.x] >> 3;
    }
  }

  private Vector3 CalcBaseDepthAverage_And_Point(int sectionNum)
  {
    //x,y: coordinate, z: average of depth value
    float depth = 0, x = 0, y = 0;
    int pixelNum = 0;

    if (basePixelMap_ == null) return new Vector3(0, 0, 0);

    foreach (var item in basePixelMap_)
    {
      if (item.sectionNumber == sectionNum)
      {
        depth += item.depth;
        x += item.x;
        y += item.y;
        pixelNum++;
      }
    }
    return new Vector3(x / pixelNum, y / pixelNum, depth / pixelNum);
  }

  //左上から順番にではなく、4つの端点から中心に向かって走査する
  private void ScanFrom4EndPoint(Texture2D colorTexture)
  {
    ScanFrom4EndPoint_Body();

    //[TODO]
    //・読みやすいコードへのリファクタリング
    //・走査範囲の厳密化
    #region LocalFunction
    void SetPixelForXY(int x, int y, int sectionNum)
    {
      Color col;
      BasePixelInfo pixel;
      int depthData = depthMap_[y * LegoData.DEPTH_CAMERA_WIDTH + x] >> 3;

      if (lowerDisplayRange_ < depthData && depthData < upperDisplayRange_)
      {
        col = new Color(0, 0, 0, 255);
      }
      else
      {
        Vector2 posColor = manager_.GetColorMapPosForDepthPos(new Vector2(x, y));
        col = colorTexture.GetPixel((int)posColor.x, (int)posColor.y);
      }
      depthTexture_.SetPixel(x, y, col);

      if (lowerBasePixelDepthValue_ <= depthData && depthData <= upperBasePixelDepthValue_)
      {
        pixel.sectionNumber = sectionNum;
        pixel.depth = (ushort)depthData;
        pixel.x = x;
        pixel.y = y;
        basePixelMap_.Add(pixel);
      }
    }

    void ScanFrom4EndPoint_Body()
    {
      int x, y;
      int horizontalCenterLine = LegoData.DEPTH_CAMERA_HEIGHT / 2;
      int verticalCenterLine = LegoData.DEPTH_CAMERA_WIDTH / 2;

      for (int i = 0; i < (LegoData.DEPTH_CAMERA_WIDTH / 2) + (LegoData.DEPTH_CAMERA_HEIGHT / 2); i++)
      {
        /*
        |--------|---------|
        |    0   |    1    |
        |------------------| <- horizontal center line 
        |    2   |    3    |
        |--------|---------|
                 ^
       vertical center line
        number = [j] value
        figure: display
        */

        for (int section = 0; section < 4; section++)
        {
          if (i < horizontalCenterLine)
          {
            switch (section)
            {
              /*
              case of j = 1:
              This step is scanning display as shown below.
              |--------------------|
              |********            |
              |******              |
              |****                |
              |**                  |
              |--------------------|
              */
              case 0:
                x = i; y = 0;
                while (x >= 0)
                {
                  SetPixelForXY(x, y, section);
                  x--; y++;
                }
                break;

              case 1:
                x = LegoData.DEPTH_CAMERA_WIDTH - i;
                y = 0;
                while (x <= LegoData.DEPTH_CAMERA_WIDTH)
                {
                  SetPixelForXY(x, y, section);
                  x++; y++;
                }
                break;

              case 2:
                x = i;
                y = LegoData.DEPTH_CAMERA_HEIGHT - 1;
                while (x >= 0)
                {
                  SetPixelForXY(x, y, section);
                  x--; y--;
                }
                break;

              case 3:
                x = LegoData.DEPTH_CAMERA_WIDTH - 1 - i;
                y = LegoData.DEPTH_CAMERA_HEIGHT - 1;

                while (x <= LegoData.DEPTH_CAMERA_WIDTH)
                {
                  SetPixelForXY(x, y, section);
                  x++; y--;
                }
                break;
            }
          }
          else if (horizontalCenterLine <= i && i < verticalCenterLine)
          {
            switch (section)
            {
              /*
              case of j = 1:
              This step is scanning display as shown below.
              |--------------------|
              |            ********|
              |          ********  |
              |        ********    |
              |      ********      |
              |--------------------|
              */
              case 0:
                x = i; y = 0;
                while (x >= i - horizontalCenterLine)
                {
                  SetPixelForXY(x, y, section);
                  x--; y++;
                }
                break;

              case 1:
                x = LegoData.DEPTH_CAMERA_WIDTH - i; y = 0;

                while (x <= LegoData.DEPTH_CAMERA_WIDTH - (i - horizontalCenterLine))
                {
                  SetPixelForXY(x, y, section);
                  x++; y++;
                }
                break;

              case 2:
                x = i; y = LegoData.DEPTH_CAMERA_HEIGHT - 1;

                while (x >= i - horizontalCenterLine)
                {
                  SetPixelForXY(x, y, section);
                  x--; y--;
                }
                break;

              case 3:
                x = (LegoData.DEPTH_CAMERA_WIDTH - i - 1); y = LegoData.DEPTH_CAMERA_HEIGHT - 1;

                while (x <= LegoData.DEPTH_CAMERA_WIDTH - (i - horizontalCenterLine))
                {
                  SetPixelForXY(x, y, section);
                  x++; y--;
                }
                break;
            }

          }
          else if (horizontalCenterLine < i)
          {
            switch (section)
            {
              /*
              case of j = 1:
              This step is scanning display as shown below.
              |--------------------|
              |                    |
              |                  **|
              |                ****|
              |              ******|
              |--------------------|
              */
              case 0:
                x = verticalCenterLine - 1;
                y = i - verticalCenterLine;
                while (x >= i - horizontalCenterLine)
                {
                  SetPixelForXY(x, y, section);
                  x--; y++;
                }
                break;

              case 1:
                x = verticalCenterLine;
                y = i - verticalCenterLine;

                while (x <= LegoData.DEPTH_CAMERA_WIDTH - (i - horizontalCenterLine))
                {
                  SetPixelForXY(x, y, section);
                  x++; y++;
                }
                break;

              case 2:
                x = verticalCenterLine;
                y = (LegoData.DEPTH_CAMERA_HEIGHT - 1) - (i - verticalCenterLine);
                while (x >= i - horizontalCenterLine)
                {
                  SetPixelForXY(x, y, section);
                  x--; y--;
                }
                break;

              case 3:
                x = verticalCenterLine;
                y = (LegoData.DEPTH_CAMERA_HEIGHT - 1) - (i - verticalCenterLine);
                while (x <= LegoData.DEPTH_CAMERA_WIDTH - (i - horizontalCenterLine))
                {
                  SetPixelForXY(x, y, section);
                  x++; y--;
                }
                break;
            }
          }
          else
          {
            Application.Quit();
          }
        }
      }
    }
    # endregion
  }

  public void CompleteCalibration()
  {
    LegoData.CalibrationData.PushCalibrationData(baseXYAndDepth_);
    SceneManager.LoadScene("Main");
  }
}