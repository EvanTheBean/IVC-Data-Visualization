using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public static class NetworkSerializationExtensions
{
    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<string> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        string[] array;
        if (serializer.IsReader)
        {
            array = new string[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<string>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<rowType> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        rowType[] array;
        if (serializer.IsReader)
        {
            array = new rowType[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<rowType>(array);
        }

    }
    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<axisType> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        axisType[] array;
        if (serializer.IsReader)
        {
            array = new axisType[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<axisType>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<bool> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        bool[] array;
        if (serializer.IsReader)
        {
            array = new bool[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<bool>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<float> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        float[] array;
        if (serializer.IsReader)
        {
            array = new float[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<float>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<Vector2> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        Vector2[] array;
        if (serializer.IsReader)
        {
            array = new Vector2[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<Vector2>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<Gradient> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        Gradient[] array;
        if (serializer.IsReader)
        {
            array = new Gradient[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<Gradient>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref Gradient grad) where TReaderWriter : IReaderWriter
    {
        if (serializer.IsReader)
        {
            grad = new Gradient();
        }

        GradientAlphaKey[] alphaKeys = grad.alphaKeys;
        GradientColorKey[] colorKeys = grad.colorKeys;
        GradientMode mode = grad.mode;

        serializer.SerializeValue(ref alphaKeys);
        serializer.SerializeValue(ref colorKeys);
        serializer.SerializeValue(ref mode);

        if (serializer.IsReader)
        {
            grad.alphaKeys = alphaKeys;
            grad.colorKeys = colorKeys;
            grad.mode = mode;
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref GradientAlphaKey[] array) where TReaderWriter : IReaderWriter
    {
        int length = 0;
        if (!serializer.IsReader)
        {
            length = array.Length;
        }

        serializer.SerializeValue(ref length);

        // Array
        if (serializer.IsReader)
        {
            array = new GradientAlphaKey[length];
        }

        for (int n = 0; n < length; ++n)
        {
            serializer.SerializeValue(ref array[n]);
        }
    }
    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref GradientAlphaKey key) where TReaderWriter : IReaderWriter
    {
        if (serializer.IsReader)
        {
            key = new GradientAlphaKey();
        }

        float alpha = key.alpha;
        float time = key.time;

        serializer.SerializeValue(ref alpha);
        serializer.SerializeValue(ref time);

        if (serializer.IsReader)
        {
            key.alpha = alpha;
            key.time = time;
        }
    }


    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref GradientColorKey[] array) where TReaderWriter : IReaderWriter
    {
        int length = 0;
        if (!serializer.IsReader)
        {
            length = array.Length;
        }

        serializer.SerializeValue(ref length);

        // Array
        if (serializer.IsReader)
        {
            array = new GradientColorKey[length];
        }

        for (int n = 0; n < length; ++n)
        {
            serializer.SerializeValue(ref array[n]);
        }
    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref GradientColorKey key) where TReaderWriter : IReaderWriter
    {
        if (serializer.IsReader)
        {
            key = new GradientColorKey();
        }

        Color color = key.color;
        float time = key.time;

        serializer.SerializeValue(ref color);
        serializer.SerializeValue(ref time);

        if (serializer.IsReader)
        {
            key.color = color;
            key.time = time;
        }
    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<ConnectedTypes> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        ConnectedTypes[] array;
        if (serializer.IsReader)
        {
            array = new ConnectedTypes[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<ConnectedTypes>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<gradientTypes> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        gradientTypes[] array;
        if (serializer.IsReader)
        {
            array = new gradientTypes[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<gradientTypes>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<catagoricalGradientsNames> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        catagoricalGradientsNames[] array;
        if (serializer.IsReader)
        {
            array = new catagoricalGradientsNames[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<catagoricalGradientsNames>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<sequentialGradientsNames> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        sequentialGradientsNames[] array;
        if (serializer.IsReader)
        {
            array = new sequentialGradientsNames[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<sequentialGradientsNames>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<divergingGradientsNames> value) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = value.Count;
        }
        serializer.SerializeValue(ref count);


        divergingGradientsNames[] array;
        if (serializer.IsReader)
        {
            array = new divergingGradientsNames[count];
        }
        else
        {
            array = value.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            value = new List<divergingGradientsNames>(array);
        }

    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref StringListDictionary dict) where TReaderWriter : IReaderWriter
    {

        int count = 0;
        if (serializer.IsWriter)
        {
            count = dict.Count;
        }
        serializer.SerializeValue(ref count);

        string[] keys = new string[count];
        List<string>[] values = new List<string>[count];

        if (serializer.IsWriter)
        {
            int i = 0;
            foreach (KeyValuePair<string, List<string>> entry in dict)
            {
                keys[i] = entry.Key;
                values[i] = entry.Value;
                i++;
            }
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref keys[i]);
            serializer.SerializeValue(ref values[i]);
        }

        if (serializer.IsReader)
        {
            dict = new StringListDictionary();
            for (int i = 0; i < count; i++)
            {
                dict.Add(keys[i], values[i]);
            }
        }
    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref List<ListWrapper> list) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = list.Count;
        }
        serializer.SerializeValue(ref count);


        ListWrapper[] array;
        if (serializer.IsReader)
        {
            array = new ListWrapper[count];
        }
        else
        {
            array = list.ToArray();
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }

        if (serializer.IsReader)
        {
            list = new List<ListWrapper>(array);
        }
    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref ListWrapper list) where TReaderWriter : IReaderWriter
    {

        List<string> innerList = new List<string>();
        if (serializer.IsWriter)
        {
            innerList = list.myList;
        }

        serializer.SerializeValue(ref innerList);

        if (serializer.IsReader)
        {
            list = new ListWrapper(innerList);
        }
    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref AnimationCurve value) where TReaderWriter : IReaderWriter
    {
        if (serializer.IsReader) 
        {
            value = new AnimationCurve();
        }

        Keyframe[] keys = value.keys;
        WrapMode preWrapMode = value.preWrapMode;
        WrapMode postWrapMode = value.postWrapMode;


        serializer.SerializeValue(ref keys);
        serializer.SerializeValue(ref preWrapMode);
        serializer.SerializeValue(ref postWrapMode);

        if (serializer.IsReader)
        {
            value = new AnimationCurve(keys);
            value.preWrapMode = preWrapMode;
            value.postWrapMode = postWrapMode;
        }
    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref Keyframe[] array) where TReaderWriter : IReaderWriter
    {
        int count = 0;
        if (!serializer.IsReader)
        {
            count = array.Length;
        }
        serializer.SerializeValue(ref count);


        if (serializer.IsReader)
        {
            array = new Keyframe[count];
        }

        for (int i = 0; i < count; i++)
        {
            serializer.SerializeValue(ref array[i]);
        }
    }

    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref Keyframe frame) where TReaderWriter : IReaderWriter
    {
        float inTangent = 0f, inWeight = 0f, outTangent = 0f, outWeight = 0f, time = 0f, value = 0f;
        WeightedMode weightedMode = WeightedMode.None;

        if (serializer.IsWriter)
        {
            inTangent = frame.inTangent;
            inWeight = frame.inWeight;
            outTangent = frame.outTangent;
            outWeight = frame.outWeight;
            time = frame.time;
            value = frame.value;
            weightedMode = frame.weightedMode;
        }

        serializer.SerializeValue(ref inTangent);
        serializer.SerializeValue(ref inWeight);
        serializer.SerializeValue(ref outTangent);
        serializer.SerializeValue(ref outWeight);
        serializer.SerializeValue(ref time);
        serializer.SerializeValue(ref value);
        serializer.SerializeValue(ref weightedMode);

        if (serializer.IsReader)
        {
            frame = new Keyframe(time, value, inTangent, outTangent, inWeight, outWeight);
            frame.weightedMode = weightedMode;
        }

    }
    public static void SerializeValue<TReaderWriter>(this BufferSerializer<TReaderWriter> serializer, ref LineRendererValues vals) where TReaderWriter : IReaderWriter
    {
        if (serializer.IsReader)
        {
            vals = new LineRendererValues();
        }
        serializer.SerializeValue(ref vals.positions);
        serializer.SerializeValue(ref vals.widthCurve);
        serializer.SerializeValue(ref vals.color);
    }
}

public static class LineRendererExtensions
{
    public static void ConvertFromValues(this LineRenderer lineRenderer, LineRendererValues values)
    {
        lineRenderer.SetPositions(values.positions);
        lineRenderer.widthCurve = values.widthCurve;
        lineRenderer.colorGradient = values.color;
    }

}

public class LineRendererValues
{

    public Vector3[] positions;
    public AnimationCurve widthCurve;
    public Gradient color;

    public LineRendererValues() { }

    public LineRendererValues(LineRenderer line)
    {
        positions = new Vector3[line.positionCount];
        line.GetPositions(positions);
        widthCurve = line.widthCurve;
        color = line.colorGradient;
    }
}

