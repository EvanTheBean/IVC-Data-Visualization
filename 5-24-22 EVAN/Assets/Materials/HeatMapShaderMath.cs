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
                    Debug.Log(color + " " + x + " " + y + "CURRENT");
                    texture.SetPixel(x, y, color);
                    textureFloats[x,y] = color;
                    //texture.Apply();

                    //Color colorY = texture.GetPixel(x, y - 10);
                    Color colorY = textureFloats[x, y - 10];
                    Debug.Log(colorY + " " + x + " " + (y - 10) + "High Y");
                    for (int i = y-9; i < y; i++)
                    {
                        Color temp = Color.Lerp(colorY, color, (i-y)/10f);
                        Debug.Log(temp);
                        texture.SetPixel(x, i, temp);
                        textureFloats[x,i] = temp;
                    }
                    //texture.Apply();

                    for (int i = y -9; i <= y; i ++)
                    {
                        //Color colorX = texture.GetPixel(x - 10, i);
                        Color colorX = textureFloats[x - 10, i];
                        //Color colorX2 = texture.GetPixel(x + 10, i);
                        Color colorX2 = textureFloats[x + 10, i];
                        Debug.Log(colorX + " " + colorX2 + " " + (x-10) + " " + (x+10) + " high and low x :)");
                        for (int j = x - 9; j <= x; j++)
                        {
                            Color temp = Color.Lerp(colorX, colorX2, (j - x) / 10f);
                            //Debug.Log(temp);
                            texture.SetPixel(j, i, temp);
                            textureFloats[j, i] = temp; 
                        }
                    }
                    //texture.Apply();
                }

                //Debug.Log(x + " " + y + " " + hits.Length);
                //Debug.Log(color);
            }
        }
        texture.Apply();

        material.SetTexture("_Texture", texture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
