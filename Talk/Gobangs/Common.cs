﻿using System;

namespace Gobangs
{
    public enum MessageType
    {
        None = 0,
        Sucess,
        Error,

        Reset = 8,
        Piece,
        Set,
        Pass,
    }

    public static class StreamHead
    {
        public const int HeadLength = 16;

        const int HeadLengthIndex = 0;
        const int MessageTypeIndex = 4;
        const int PointXIndex = 8;
        const int PointYIndex = 12;

        public static void WriteHead(byte[] array, MessageType type, int x, int y)
        {
            Array.Copy(BitConverter.GetBytes(HeadLength), 0, array, HeadLengthIndex, 4);
            Array.Copy(BitConverter.GetBytes((int)type), 0, array, MessageTypeIndex, 4);
            Array.Copy(BitConverter.GetBytes(x), 0, array, PointXIndex, 4);
            Array.Copy(BitConverter.GetBytes(y), 0, array, PointYIndex, 4);
        }

        public static void Read(byte[] array, out MessageType type, out int x, out int y)
        {
            type = (MessageType)BitConverter.ToInt32(array, MessageTypeIndex);
            x = BitConverter.ToInt32(array, PointXIndex);
            y = BitConverter.ToInt32(array, PointYIndex);
        }
    }
}
