using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class HeatMapShaderMath : MonoBehaviour
{
    public Material material;
    Texture2D texture;
    Color[,] textureFloats;


    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(Screen.width, Screen.height);
        textureFloats = new Color[Screen.width,Screen.height];

        for (int y = 0; y < texture.height; y += 1)
        {
            for (int x = 0; x < texture.width; x += 1)
            {
                texture.SetPixel(x, y, new Color(0,0,0,0));
                textureFloats[x,y]= new Color(0,0,0,0);
            }
        }

        for (int y = 0; y < texture.height; y+=10)
        {
            for (int x = 0; x < texture.width; x+=10)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(x, y));
                RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

                if(hits.Length > 0)
                {
                    Color color = new Color(hits.Length/5f, 0, 0, 1);
                    for (int i = x - 5; i <= x + 5; i++)
                    {
                        for (int j = y - 5; j <= y + 5; j++)
                        {
                            //Color temp = color - new Color(((0.2f) * new Vector2(Mathf.Abs(i - x), Mathf.Abs(j - y)).magnitude), 0, 0, 0);
                            //textureFloats[i, j] += temp;
                            texture.SetPixel(i, j, color);
                        }
                    }
                    /*
                    for (int i = x - 10; i <= x + 10; i++)
                    {
                        for(int j = y - 10; j <= y + 10; j++)
                        {
                            Color temp = color - new Color(((0.2f) * new Vector2(Mathf.Abs(i-x), Mathf.Abs(j-y)).magnitude), 0, 0, 0);
                            //textureFloats[i, j] += temp;
                            texture.SetPixel(i,j, temp + texture.GetPixel(i,j));
                        }
                    }
                    */
                }

                //Debug.Log(x + " " + y + " " + hits.Length);
                //Debug.Log(color);
            }
        }

       /*
        for (int y = 0; y < texture.height; y ++)
        {
            for (int x = 0; x < texture.width; x ++)
            {
                texture.SetPixel(x,y, textureFloats[x, y]);
                Debug.Log(textureFloats[x, y]);
            }
        }
       */
        

        texture.Apply();

        material.SetTexture("_Texture", texture);
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < texture.height; y += 1)
        {
            for (int x = 0; x < texture.width; x += 1)
            {
                texture.SetPixel(x, y, new Color(0, 0, 0, 0));
                textureFloats[x, y] = new Color(0, 0, 0, 0);
            }
        }

        for (int y = 0; y < texture.height; y += 10)
        {
            for (int x = 0; x < texture.width; x += 10)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(x, y));
                RaycastHit[] hits = Physics.RaycastAll(ray, 100f);


                if (hits.Length > 0)
                {
                    Color color = new Color(hits.Length / 5f, 0, 0, 1);
                    for (int i = x - 5; i <= x + 5; i++)
                    {
                        for (int j = y - 5; j <= y + 5; j++)
                        {
                            //Color temp = color - new Color(((0.2f) * new Vector2(Mathf.Abs(i - x), Mathf.Abs(j - y)).magnitude), 0, 0, 0);
                            //textureFloats[i, j] += temp;
                            texture.SetPixel(i, j, color);
                        }
                    }
                    /*
                    for (int i = x - 10; i <= x + 10; i++)
                    {
                        for(int j = y - 10; j <= y + 10; j++)
                        {
                            Color temp = color - new Color(((0.2f) * new Vector2(Mathf.Abs(i-x), Mathf.Abs(j-y)).magnitude), 0, 0, 0);
                            //textureFloats[i, j] += temp;
                            texture.SetPixel(i,j, temp + texture.GetPixel(i,j));
                        }
                    }
                    */
                }

                //Debug.Log(x + " " + y + " " + hits.Length);
                //Debug.Log(color);
            }
        }
        texture.Apply();
    }
}
