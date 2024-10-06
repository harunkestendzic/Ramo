using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public float waveSpeed = 2f;
    public float waveWidth = 30f;      
    public float waveFrequency = 2f;   

    void Start()
    {
    }
    void Update(){
        StartCoroutine(AnimateTextWaveHorizontal());

    }

    IEnumerator AnimateTextWaveHorizontal()
    {
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        while (true)
        {
            textMeshPro.ForceMeshUpdate();
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (textInfo.characterInfo[i].isVisible)
                {
                    Vector3[] vertices = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;

                    for (int j = 0; j < 4; j++) 
                    {
                        int vertexIndex = textInfo.characterInfo[i].vertexIndex + j;
                        Vector3 originalPosition = vertices[vertexIndex];
                        float waveOffset = Mathf.Sin(Time.time * waveSpeed + i * waveFrequency) * waveWidth;
                        vertices[vertexIndex] = originalPosition + new Vector3(waveOffset, 0, 0);
                    }

                    textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                }
            }

            yield return null; 
        }
    }
}
