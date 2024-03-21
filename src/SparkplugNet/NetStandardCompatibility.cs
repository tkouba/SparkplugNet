#if NETSTANDARD2_0

#pragma warning disable IDE0161 // Convert to file-scoped namespace
namespace System.Runtime.CompilerServices
#pragma warning restore IDE0161 // Convert to file-scoped namespace
{
    internal static class IsExternalInit { }
}

internal static class BinaryConverter
{
    internal static float ReadSingleLittleEndian(ReadOnlySpan<byte> source)
    {
        if (BitConverter.IsLittleEndian)
        {
            return BitConverter.ToSingle(source.ToArray(), 0);
        }
        // other-endian; reverse this portion of the data (4 bytes)
        byte[] data = source.ToArray();
        byte tmp = data[0];
        data[0] = data[3];
        data[3] = tmp;
        tmp = data[1];
        data[1] = data[2];
        data[2] = tmp;
        return BitConverter.ToSingle(data, 0);
    }

    internal static double ReadDoubleLittleEndian(ReadOnlySpan<byte> source)
    {
        if (BitConverter.IsLittleEndian)
        {
            return BitConverter.ToDouble(source.ToArray(), 0);
        }
        // other-endian; reverse this portion of the data (4 bytes)
        byte[] data = source.ToArray();
        byte tmp = data[0];
        data[0] = data[7];
        data[7] = tmp;
        tmp = data[1];
        data[1] = data[6];
        data[6] = tmp;
        tmp = data[2];
        data[2] = data[5];
        data[5] = tmp;
        tmp = data[3];
        data[3] = data[4];
        data[4] = tmp;
        return BitConverter.ToDouble(data, 0);
    }

    internal static void WriteSingleLittleEndian(Span<byte> destination, float value)
    {
        byte[] data = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            data.CopyTo(destination);
        }
        else
        {
            for (int i = 0; i < data.Length; i++)
            {
                destination[data.Length - 1 - i] = data[i];
            }
        }
    }
    internal static void WriteDoubleLittleEndian(Span<byte> destination, double value)
    {
        byte[] data = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            data.CopyTo(destination);
        }
        else
        {
            for (int i = 0; i < data.Length; i++)
            {
                destination[data.Length - 1 - i] = data[i];
            }
        }
    }
}

#endif