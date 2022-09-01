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

    public Vector3 PressBlockPos;
    public Vector3 ReleaseBlockPos;

    public bool isShootRay = false;
    int columnMax = 8;
    bool isMoving = false;
    

    [SerializeField] int blockSum = 0;

    private void Start()
    {
        
   //     StartCoroutine(CountBlocks());
        StartCoroutine(CheckRay());
    }

    // Update is called once per frame
    void Update()
    {
        CheckFullBlocks();


        if(Input.GetMouseButtonDown(0))
        {
            //if(isUsedInput)
            //{
                isShootRay = false;
            //}

            for (int i = 0; i < spawnPos.Length; i++)
            {
                Rigidbody2D[] _blocks = spawnPos[i].GetComponentsInChildren<Rigidbody2D>();
                foreach (var item in _blocks)
                {
                    item.bodyType = RigidbodyType2D.Static;
                    //Ray 쏘지 않기
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {

            for (int i = 0; i < spawnPos.Length; i++)
            {
                Rigidbody2D[] _blocks = spawnPos[i].GetComponentsInChildren<Rigidbody2D>();
                foreach (var item in _blocks)
                {
                    item.bodyType = RigidbodyType2D.Dynamic;
                    isShootRay = true;
                    Debug.Log("buttonup");
                    //Ray 쏘기
                }
            }
        }      
    }



    void CheckFullBlocks()
    {
        time += Time.deltaTime;

        if (time > delayTime)
        {
            for (int i = 0; i < spawnPos.Length; i++)
            {
                int childsCount = spawnPos[i].childCount;

                //max보다 적으면 적은만큼 블럭 채우기
                if (childsCount < columnMax)
                {
                    CreateBlocks(spawnPos[i]);

                    time = 0;
                }
            }
        }
    }



    IEnumerator CheckRay()
    {
        while (true)
        {
            if (blocks.Count >= 40)
            {
                CheckBlockMovement();

                if(!isMoving)
                {
                    isShootRay = true;
                    Debug.Log("chechray");
                    yield return new WaitUntil(() => blockSum < 40);
                }

            }
            else
            {
                isShootRay = false;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    Vector3[] lastPos = new Vector3[40];
    int countStop = 0;
    void CheckBlockMovement()
    {
        //움직일때
        for (int i = 0; i < blocks.Count; i++)
        {
            if (lastPos[i] != blocks[i].transform.position)
            {
                isMoving = true;
                lastPos[i] = blocks[i].transform.position;
                countStop = 0;
            }

            //안움직일때
            if (lastPos[i] == blocks[i].transform.position)
            {
                countStop++;
                if (countStop >= 40)
                {
                    isMoving = false;
                }
            }
        }
    }

    IEnumerator CountBlocks()
    {
        while(true)
        {
            int count = 0;
            foreach (var spawn in spawnPos)
            {
                count += spawn.childCount;
            }

            blockSum = count;
            yield return new WaitForEndOfFrame();
        }    
    }


    [SerializeField] List<GameObject> blocks = new List<GameObject>();

    void InitializeList(List<GameObject> list)
    {
        for(int i=0; i<list.Count; i++)
        {
            if(list[i].gameObject == null)
            {
                list.Remove(list[i]);
            }
        }
    }

    void CreateBlocks(Transform parent)
    {
        InitializeList(blocks);

        isShootRay = false;
        //GameObject _block = Instantiate(Block, spawnPos[i]);
        GameObject _block = Instantiate(Block, parent);
        int random = Random.Range(0, images.Length);
        _block.GetComponent<Image>().sprite = images[random];
        string _blockSpriteName = _block.GetComponent<Image>().sprite.name;


        if (_blockSpriteName.Contains("blue"))
        {
            _block.name = "Blue";
        }
        if (_blockSpriteName.Contains("green"))
        {
            _block.name = "Green";
        }
        if (_blockSpriteName.Contains("red"))
        {
            _block.name = "Red";
        }
        if (_blockSpriteName.Contains("yellow"))
        {
            _block.name = "Yellow";
        }

        blocks.Add(_block);
    }

}
