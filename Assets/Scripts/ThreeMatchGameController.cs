using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeMatchGameController : MonoBehaviour
{
    public GameObject Block;

    public Transform[] spawnPos = new Transform[5];
    public Sprite[] images = new Sprite[4];
    
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
                    GameObject _block = Instantiate(Block, spawnPos[i]);
                    int random = Random.Range(0, images.Length);
                    _block.GetComponent<Image>().sprite = images[random];
                }
                time = 0;
            }
        }
      

    }



}
