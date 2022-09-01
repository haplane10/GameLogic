using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance;
    public List<LinkedBlock> CrushedBlockList = new List<LinkedBlock>();


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        CrushBlocks();
    }

    void CrushBlocks()
    {
        for(int i=0; i<CrushedBlockList.Count; i++)
        {
            Destroy(CrushedBlockList[i].preBlock.gameObject);
            Destroy(CrushedBlockList[i].postBlock.gameObject);
        }
    }

}



[System.Serializable]
public class LinkedBlock
{
    public GameObject preBlock;
    public GameObject postBlock;
}
