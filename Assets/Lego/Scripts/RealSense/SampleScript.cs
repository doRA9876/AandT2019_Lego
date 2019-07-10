using UnityEngine.UI;
using UnityEngine;
using OpenCvSharp;
using System;

public static class TextureExt
{
  public static Texture2D ToTexture2D(this Texture self)
  {
    var sw = self.width;
    var sh = self.height;
    var format = TextureFormat.RGBA32;
    var result = new Texture2D(sw, sh, format, false);
    var currentRT = RenderTexture.active;
    var rt = new RenderTexture(sw, sh, 32);
    Graphics.Blit(self, rt);
    RenderTexture.active = rt;
    var source = new UnityEngine.Rect(0, 0, rt.width, rt.height);
    result.ReadPixels(source, 0, 0);
    result.Apply();
    RenderTexture.active = currentRT;
    return result;
  }
}

public class SampleScript : MonoBehaviour
{
  [SerializeField] RawImage colorImage_, depthImage_, debugImage_, debugImage2_;
  //[SerializeField] private int upperBasePixelDepthValue_ = 420, lowerBasePixelDepthValue_ = 415;
  private Texture2D colorTexture_, depthTexture_;
  private Texture depthTexture2_;
  private byte[] depthMap_;
  private Color[] colorMap_;
  private bool isBinarizationRunning = false, isCreatingDepthTexture = false;
  private float timeLeft__1FPS_, timeLeft__15FPS_;

  void Start()
  {
    colorMap_ = new Color[LegoData.DEPTH_CAMERA_WIDTH * LegoData.DEPTH_CAMERA_HEIGHT];
    colorTexture_ = new Texture2D(LegoData.DEPTH_CAMERA_WIDTH, LegoData.DEPTH_CAMERA_HEIGHT, TextureFormat.RGB24, false);
    depthTexture_ = new Texture2D(LegoData.REALSENSE_DEPTH_WIDTH, LegoData.REALSENSE_DEPTH_HEIGHT, TextureFormat.RGB24, false);

    timeLeft__15FPS_ = 0.06f;
  }

  void Update()
  {
    timeLeft__1FPS_ -= Time.deltaTime;
    timeLeft__15FPS_ -= Time.deltaTime;

    //15fps処理

    if (colorImage_ != null)
    {
      colorTexture_ = colorImage_.texture.ToTexture2D();
    }

    if (depthImage_ != null)
    {
      depthMap_ = depthImage_.texture.ToTexture2D().GetRawTextureData();
    }

    if(!isCreatingDepthTexture)
    {
      Action createDepth = CreateDepthTexture;
      createDepth();
      debugImage2_.texture = depthTexture_;
    }

    if (!isBinarizationRunning)
    {
      Action binarization = Binarization;
      binarization();
    }
  }

  void Binarization()
  {
    isBinarizationRunning = true;
    Mat mat = OpenCvSharp.Unity.TextureToMat(colorTexture_);
    for (int y = 0; y < mat.Height; y++)
    {
      for (int x = 0; x < mat.Width; x++)
      {
        Vec3b v = mat.At<Vec3b>(y, x);
        //Debug.Log(v[0]);
        float gr = 0.2126f * v[2] + 0.7152f * v[1] + 0.0722f * v[0];
        if (gr < 128)
        {
          gr = 0;
        }
        else
        {
          gr = 255;
        }
        v[0] = (byte)gr;
        v[1] = (byte)gr;
        v[2] = (byte)gr;
        mat.Set<Vec3b>(y, x, v);
      }
    }
    colorTexture_ = OpenCvSharp.Unity.MatToTexture(mat);
    debugImage_.texture = colorTexture_;
    isBinarizationRunning = false;
  }

  void CreateDepthTexture()
  {
    isCreatingDepthTexture = true;
    for (int y = 0; y < LegoData.REALSENSE_DEPTH_HEIGHT; y++)
    {
      for (int x = 0; x < LegoData.REALSENSE_DEPTH_WIDTH; x++)
      {
        /*
        float depthMapValue = depthMap_[(y * LegoData.REALSENSE_DEPTH_WIDTH + x) * 4];
        float mono = depthMapValue / 255f;
        */
        float depthMapValue = depthMap_[(y * LegoData.REALSENSE_DEPTH_WIDTH + x) * 4];
        float r = depthMapValue / 256f;
        Color color = new Color(r, 0, 0);
        depthTexture_.SetPixel(x, y, color);
      }
    }
    depthTexture_.Apply();
    isCreatingDepthTexture = false;
  }
}

