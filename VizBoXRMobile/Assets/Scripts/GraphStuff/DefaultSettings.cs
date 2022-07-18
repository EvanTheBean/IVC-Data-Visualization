using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum gradientTypes
{
    sequential, diverging, catagorical, custom
}
public enum catagoricalGradientsNames
{
    retro, dutchField, pastels
}
public enum sequentialGradientsNames
{
    viridis, plasma, inferno
}
public enum divergingGradientsNames
{
    seismic, pinkFoam, orangePurple
}

public class Gradients
{
    public static List<Gradient> catagoricalGradients = new List<Gradient>();
    public static List<Gradient> sequentialGradients = new List<Gradient>();
    public static List<Gradient> divergingGradients = new List<Gradient>();

    public Gradients()
    {
        GradientUsageAttribute gua = new GradientUsageAttribute(false);

        //virdis = new Gradient();
        for(int i = 0; i < 3; i++)
        {
            catagoricalGradients.Add(new Gradient());
        }
        for (int i = 0; i < 3; i++)
        {
            sequentialGradients.Add(new Gradient());
        }
        for (int i = 0; i < 3; i++)
        {
            divergingGradients.Add(new Gradient());
        }

        GradientColorKey[] colorKey = new GradientColorKey[6];
        colorKey[0].color = new Color(253f / 256f, 231f / 256f, 37f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(122f / 256f, 209f / 256f, 81f / 256f);
        colorKey[1].time = 0.2f;
        colorKey[2].color = new Color(34f / 256f, 168f / 256f, 132f / 256f);
        colorKey[2].time = 0.4f;
        colorKey[3].color = new Color(42f / 256f, 120f / 256f, 142f / 256f);
        colorKey[3].time = 0.6f;
        colorKey[4].color = new Color(65f / 256f, 68f / 256f, 135f / 256f);
        colorKey[4].time = 0.8f;
        colorKey[5].color = new Color(68f / 256f, 1f / 256f, 84f / 256f);
        colorKey[5].time = 1.0f;
        sequentialGradients[(int)sequentialGradientsNames.viridis].colorKeys = colorKey;

        colorKey = new GradientColorKey[6];
        colorKey[0].color = new Color(240f / 256f, 249f / 256f, 33f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(252f / 256f, 166f / 256f, 54f / 256f);
        colorKey[1].time = 0.2f;
        colorKey[2].color = new Color(225f / 256f, 100f / 256f, 98f / 256f);
        colorKey[2].time = 0.4f;
        colorKey[3].color = new Color(177f / 256f, 42f / 256f, 144f / 256f);
        colorKey[3].time = 0.6f;
        colorKey[4].color = new Color(106f / 256f, 0f / 256f, 168f / 256f);
        colorKey[4].time = 0.8f;
        colorKey[5].color = new Color(13f / 256f, 8f / 256f, 135f / 256f);
        colorKey[5].time = 1.0f;
        sequentialGradients[(int)sequentialGradientsNames.plasma].colorKeys = colorKey;

        colorKey = new GradientColorKey[6];
        colorKey[0].color = new Color(252f / 256f, 255f / 256f, 164f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(252f / 256f, 165f / 256f, 10f / 256f);
        colorKey[1].time = 0.2f;
        colorKey[2].color = new Color(221f / 256f, 81f / 256f, 58f / 256f);
        colorKey[2].time = 0.4f;
        colorKey[3].color = new Color(147f / 256f, 38f / 256f, 103f / 256f);
        colorKey[3].time = 0.6f;
        colorKey[4].color = new Color(66f / 256f, 10f / 256f, 104f / 256f);
        colorKey[4].time = 0.8f;
        colorKey[5].color = new Color(0f / 256f, 0f / 256f, 4f / 256f);
        colorKey[5].time = 1.0f;
        sequentialGradients[(int)sequentialGradientsNames.inferno].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(25f / 256f, 132f / 256f, 197f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(34f / 256f, 167f / 256f, 240f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(99f / 256f, 191f / 256f, 240f / 256f);
        colorKey[2].time = 0.25f;
        colorKey[3].color = new Color(167f / 256f, 213f / 256f, 237f / 256f);
        colorKey[3].time = 0.375f;
        colorKey[4].color = new Color(225f / 256f, 166f / 256f, 146f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(222f / 256f, 110f / 256f, 86f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(225f / 256f, 75f / 256f, 49f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(194f / 256f, 55f / 256f, 40f / 256f);
        colorKey[7].time = 1.0f;
        divergingGradients[(int)divergingGradientsNames.seismic].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(255f / 256f, 180f / 256f, 0f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(210f / 256f, 152f / 256f, 13f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(165f / 256f, 124f / 256f, 27f / 256f);
        colorKey[2].time = 0.25f;
        colorKey[3].color = new Color(120f / 256f, 96f / 256f, 40f / 256f);
        colorKey[3].time = 0.375f;
        colorKey[4].color = new Color(72f / 256f, 68f / 256f, 110f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(94f / 256f, 86f / 256f, 155f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(119f / 256f, 107f / 256f, 205f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(144f / 256f, 128f / 256f, 255f / 256f);
        colorKey[7].time = 1.0f;
        divergingGradients[(int)divergingGradientsNames.orangePurple].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(84f / 256f, 190f / 256f, 190f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(118f / 256f, 200f / 256f, 200f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(152f / 256f, 209f / 256f, 209f / 256f);
        colorKey[2].time = 0.25f;
        colorKey[3].color = new Color(120f / 256f, 96f / 256f, 40f / 256f);
        colorKey[3].time = 0.375f;
        colorKey[4].color = new Color(228f / 256f, 188f / 256f, 173f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(223f / 256f, 151f / 256f, 158f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(215f / 256f, 101f / 256f, 139f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(200f / 256f, 0f / 256f, 100f / 256f);
        colorKey[7].time = 1.0f;
        divergingGradients[(int)divergingGradientsNames.pinkFoam].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(234f / 256f, 85f / 256f, 69f / 256f);
        colorKey[0].time = 0.125f;
        colorKey[1].color = new Color(244f / 256f, 106f / 256f, 155f / 256f);
        colorKey[1].time = 0.25f;
        colorKey[2].color = new Color(239f / 256f, 155f / 256f, 32f / 256f);
        colorKey[2].time = 0.375f;
        colorKey[3].color = new Color(237f / 256f, 191f / 256f, 51f / 256f);
        colorKey[3].time = 0.5f;
        colorKey[4].color = new Color(189f / 256f, 207f / 256f, 50f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(135f / 256f, 188f / 256f, 69f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(39f / 256f, 174f / 256f, 239f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(179f / 256f, 61f / 256f, 198f / 256f);
        colorKey[7].time = 1.0f;
        catagoricalGradients[(int)catagoricalGradientsNames.retro].colorKeys = colorKey;
        catagoricalGradients[(int)catagoricalGradientsNames.retro].mode = GradientMode.Fixed;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(230f / 256f, 0f / 256f, 73f / 256f);
        colorKey[0].time = 0.125f;
        colorKey[1].color = new Color(11f / 256f, 180f / 256f, 255f / 256f);
        colorKey[1].time = 0.25f;
        colorKey[2].color = new Color(80f / 256f, 233f / 256f, 145f / 256f);
        colorKey[2].time = 0.375f;
        colorKey[3].color = new Color(230f / 256f, 216f / 256f, 0f / 256f);
        colorKey[3].time = 0.5f;
        colorKey[4].color = new Color(255f / 256f, 163f / 256f, 0f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(220f / 256f, 10f / 256f, 180f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(179f / 256f, 212f / 256f, 255f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(0f / 256f, 191f / 256f, 160f / 256f);
        colorKey[7].time = 1.0f;
        catagoricalGradients[(int)catagoricalGradientsNames.dutchField].colorKeys = colorKey;
        catagoricalGradients[(int)catagoricalGradientsNames.dutchField].mode = GradientMode.Fixed;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(253f / 256f, 127f / 256f, 111f / 256f);
        colorKey[0].time = 0.125f;
        colorKey[1].color = new Color(126f / 256f, 176f / 256f, 213f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(178f / 256f, 224f / 256f, 97f / 256f);
        colorKey[2].time = 0.375f;
        colorKey[3].color = new Color(189f / 256f, 126f / 256f, 190f / 256f);
        colorKey[3].time = 0.5f;
        colorKey[4].color = new Color(255f / 256f, 238f / 256f, 101f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(190f / 256f, 185f / 256f, 219f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(253f / 256f, 204f / 256f, 229f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(139f / 256f, 211f / 256f, 199f / 256f);
        colorKey[7].time = 1.0f;
        catagoricalGradients[(int)catagoricalGradientsNames.pastels].colorKeys = colorKey;
        catagoricalGradients[(int)catagoricalGradientsNames.pastels].mode = GradientMode.Fixed;
    }

    public void Reset()
    {
        GradientUsageAttribute gua = new GradientUsageAttribute(false);

        //virdis = new Gradient();
        for (int i = 0; i < 3; i++)
        {
            catagoricalGradients.Add(new Gradient());
        }
        for (int i = 0; i < 3; i++)
        {
            sequentialGradients.Add(new Gradient());
        }
        for (int i = 0; i < 3; i++)
        {
            divergingGradients.Add(new Gradient());
        }

        GradientColorKey[] colorKey = new GradientColorKey[6];
        colorKey[0].color = new Color(253f / 256f, 231f / 256f, 37f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(122f / 256f, 209f / 256f, 81f / 256f);
        colorKey[1].time = 0.2f;
        colorKey[2].color = new Color(34f / 256f, 168f / 256f, 132f / 256f);
        colorKey[2].time = 0.4f;
        colorKey[3].color = new Color(42f / 256f, 120f / 256f, 142f / 256f);
        colorKey[3].time = 0.6f;
        colorKey[4].color = new Color(65f / 256f, 68f / 256f, 135f / 256f);
        colorKey[4].time = 0.8f;
        colorKey[5].color = new Color(68f / 256f, 1f / 256f, 84f / 256f);
        colorKey[5].time = 1.0f;
        sequentialGradients[(int)sequentialGradientsNames.viridis].colorKeys = colorKey;

        colorKey = new GradientColorKey[6];
        colorKey[0].color = new Color(240f / 256f, 249f / 256f, 33f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(252f / 256f, 166f / 256f, 54f / 256f);
        colorKey[1].time = 0.2f;
        colorKey[2].color = new Color(225f / 256f, 100f / 256f, 98f / 256f);
        colorKey[2].time = 0.4f;
        colorKey[3].color = new Color(177f / 256f, 42f / 256f, 144f / 256f);
        colorKey[3].time = 0.6f;
        colorKey[4].color = new Color(106f / 256f, 0f / 256f, 168f / 256f);
        colorKey[4].time = 0.8f;
        colorKey[5].color = new Color(13f / 256f, 8f / 256f, 135f / 256f);
        colorKey[5].time = 1.0f;
        sequentialGradients[(int)sequentialGradientsNames.plasma].colorKeys = colorKey;

        colorKey = new GradientColorKey[6];
        colorKey[0].color = new Color(252f / 256f, 255f / 256f, 164f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(252f / 256f, 165f / 256f, 10f / 256f);
        colorKey[1].time = 0.2f;
        colorKey[2].color = new Color(221f / 256f, 81f / 256f, 58f / 256f);
        colorKey[2].time = 0.4f;
        colorKey[3].color = new Color(147f / 256f, 38f / 256f, 103f / 256f);
        colorKey[3].time = 0.6f;
        colorKey[4].color = new Color(66f / 256f, 10f / 256f, 104f / 256f);
        colorKey[4].time = 0.8f;
        colorKey[5].color = new Color(0f / 256f, 0f / 256f, 4f / 256f);
        colorKey[5].time = 1.0f;
        sequentialGradients[(int)sequentialGradientsNames.inferno].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(25f / 256f, 132f / 256f, 197f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(34f / 256f, 167f / 256f, 240f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(99f / 256f, 191f / 256f, 240f / 256f);
        colorKey[2].time = 0.25f;
        colorKey[3].color = new Color(167f / 256f, 213f / 256f, 237f / 256f);
        colorKey[3].time = 0.375f;
        colorKey[4].color = new Color(225f / 256f, 166f / 256f, 146f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(222f / 256f, 110f / 256f, 86f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(225f / 256f, 75f / 256f, 49f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(194f / 256f, 55f / 256f, 40f / 256f);
        colorKey[7].time = 1.0f;
        divergingGradients[(int)divergingGradientsNames.seismic].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(255f / 256f, 180f / 256f, 0f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(210f / 256f, 152f / 256f, 13f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(165f / 256f, 124f / 256f, 27f / 256f);
        colorKey[2].time = 0.25f;
        colorKey[3].color = new Color(120f / 256f, 96f / 256f, 40f / 256f);
        colorKey[3].time = 0.375f;
        colorKey[4].color = new Color(72f / 256f, 68f / 256f, 110f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(94f / 256f, 86f / 256f, 155f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(119f / 256f, 107f / 256f, 205f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(144f / 256f, 128f / 256f, 255f / 256f);
        colorKey[7].time = 1.0f;
        divergingGradients[(int)divergingGradientsNames.orangePurple].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(84f / 256f, 190f / 256f, 190f / 256f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(118f / 256f, 200f / 256f, 200f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(152f / 256f, 209f / 256f, 209f / 256f);
        colorKey[2].time = 0.25f;
        colorKey[3].color = new Color(120f / 256f, 96f / 256f, 40f / 256f);
        colorKey[3].time = 0.375f;
        colorKey[4].color = new Color(228f / 256f, 188f / 256f, 173f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(223f / 256f, 151f / 256f, 158f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(215f / 256f, 101f / 256f, 139f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(200f / 256f, 0f / 256f, 100f / 256f);
        colorKey[7].time = 1.0f;
        divergingGradients[(int)divergingGradientsNames.pinkFoam].colorKeys = colorKey;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(234f / 256f, 85f / 256f, 69f / 256f);
        colorKey[0].time = 0.125f;
        colorKey[1].color = new Color(244f / 256f, 106f / 256f, 155f / 256f);
        colorKey[1].time = 0.25f;
        colorKey[2].color = new Color(239f / 256f, 155f / 256f, 32f / 256f);
        colorKey[2].time = 0.375f;
        colorKey[3].color = new Color(237f / 256f, 191f / 256f, 51f / 256f);
        colorKey[3].time = 0.5f;
        colorKey[4].color = new Color(189f / 256f, 207f / 256f, 50f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(135f / 256f, 188f / 256f, 69f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(39f / 256f, 174f / 256f, 239f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(179f / 256f, 61f / 256f, 198f / 256f);
        colorKey[7].time = 1.0f;
        catagoricalGradients[(int)catagoricalGradientsNames.retro].colorKeys = colorKey;
        catagoricalGradients[(int)catagoricalGradientsNames.retro].mode = GradientMode.Fixed;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(230f / 256f, 0f / 256f, 73f / 256f);
        colorKey[0].time = 0.125f;
        colorKey[1].color = new Color(11f / 256f, 180f / 256f, 255f / 256f);
        colorKey[1].time = 0.25f;
        colorKey[2].color = new Color(80f / 256f, 233f / 256f, 145f / 256f);
        colorKey[2].time = 0.375f;
        colorKey[3].color = new Color(230f / 256f, 216f / 256f, 0f / 256f);
        colorKey[3].time = 0.5f;
        colorKey[4].color = new Color(255f / 256f, 163f / 256f, 0f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(220f / 256f, 10f / 256f, 180f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(179f / 256f, 212f / 256f, 255f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(0f / 256f, 191f / 256f, 160f / 256f);
        colorKey[7].time = 1.0f;
        catagoricalGradients[(int)catagoricalGradientsNames.dutchField].colorKeys = colorKey;
        catagoricalGradients[(int)catagoricalGradientsNames.dutchField].mode = GradientMode.Fixed;

        colorKey = new GradientColorKey[8];
        colorKey[0].color = new Color(253f / 256f, 127f / 256f, 111f / 256f);
        colorKey[0].time = 0.125f;
        colorKey[1].color = new Color(126f / 256f, 176f / 256f, 213f / 256f);
        colorKey[1].time = 0.125f;
        colorKey[2].color = new Color(178f / 256f, 224f / 256f, 97f / 256f);
        colorKey[2].time = 0.375f;
        colorKey[3].color = new Color(189f / 256f, 126f / 256f, 190f / 256f);
        colorKey[3].time = 0.5f;
        colorKey[4].color = new Color(255f / 256f, 238f / 256f, 101f / 256f);
        colorKey[4].time = 0.625f;
        colorKey[5].color = new Color(190f / 256f, 185f / 256f, 219f / 256f);
        colorKey[5].time = 0.75f;
        colorKey[6].color = new Color(253f / 256f, 204f / 256f, 229f / 256f);
        colorKey[6].time = 0.875f;
        colorKey[7].color = new Color(139f / 256f, 211f / 256f, 199f / 256f);
        colorKey[7].time = 1.0f;
        catagoricalGradients[(int)catagoricalGradientsNames.pastels].colorKeys = colorKey;
        catagoricalGradients[(int)catagoricalGradientsNames.pastels].mode = GradientMode.Fixed;
    }
}

public class DefaultSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
