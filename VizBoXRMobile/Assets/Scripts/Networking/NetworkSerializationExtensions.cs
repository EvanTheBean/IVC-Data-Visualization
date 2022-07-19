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
}
