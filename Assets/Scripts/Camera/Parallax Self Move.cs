using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSelfMove : MonoBehaviour
{
    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;
    float farthestBack;

    [Range(0.01f, 5f)]
    public float parallaxSpeed;

    [Range(0.1f, 2.0f)]
    public float baseSpeed; 

    void Start()
    {
        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) 
        {
            if((backgrounds[i].transform.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) 
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed * baseSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(Time.time * speed, 0));
        }
    }
}
