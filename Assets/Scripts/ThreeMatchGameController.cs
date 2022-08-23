using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeMatchGameController : MonoBehaviour
{
    public GameObject Block;
    //public Transform spawnPos1;
    //public Transform spawnPos2;
    //public Transform spawnPos3;
    //public Transform spawnPos4;
    //public Transform spawnPos5;

    public Transform[] spawnPos = new Transform[5];
    
    public float delayTime;
    
    private float time;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if( time > delayTime)
        {
            if (count < 8)
            {
                count++;
                for(int i=0; i<spawnPos.Length; i++)
                {
                    Instantiate(Block, spawnPos[i]);
                }
                //Instantiate(Block, spawnPos1);
                //Instantiate(Block, spawnPos2);
                //Instantiate(Block, spawnPos3);
                //Instantiate(Block, spawnPos4);
                //Instantiate(Block, spawnPos5);
                time = 0;
            }
        }
      

    }



}
