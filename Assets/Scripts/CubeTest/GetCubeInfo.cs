using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCubeInfo : MonoBehaviour
{
    MeshFilter mf;
    MeshRenderer mr;
    Mesh mesh;
    Vector3[] vertices;

    public GameObject vertex;
    public float scale;
    float SumDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mf = transform.GetComponent<MeshFilter>();
        mr = transform.GetComponent<MeshRenderer>();
        vertices = mf.mesh.vertices;
        Debug.Log(vertices.Length);
        //GetSumLinesLength();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetSumLinesLength()
    {
        
        for(int i = vertices.Length; i > 0; i--)
        {
            float distance = Vector3.Distance(vertices[i], vertices[i - 1]);
            SumDistance += distance;
        }
        Debug.Log(SumDistance);
    }
}
