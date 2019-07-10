using UnityEngine;

public class AnimateLandscape : MonoBehaviour
{
    GameObject[] buildingBlocks;//, roadBlocks;
    float height;
    int legoNum;
    //bool updateBlocks;

    // Start is called before the first frame update
    void Start()
    {
        buildingBlocks = GameObject.FindGameObjectsWithTag("Building");
        //roadBlocks = GameObject.FindGameObjectsWithTag("Road");
        height = 0f;
        legoNum = 0;
        //updateBlocks = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (updateBlocks == true)
          roadBlocks[legoNum].transform.position -= new Vector3(0f, 10f, 0f);*/
        //else
          buildingBlocks[legoNum].transform.position -= new Vector3(0f, 5f, 0f);

        height++;

        if (height == 10f)
        {
            legoNum++;
            height = 0;
        }

        /*if (legoNum == roadBlocks.Length)
        {
            updateBlocks = false;
            legoNum = 0;
        }*/
        /*else */if (legoNum == buildingBlocks.Length/* && updateBlocks == false*/)
            enabled = false;
    }
}