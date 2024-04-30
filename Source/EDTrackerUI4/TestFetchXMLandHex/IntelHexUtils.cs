// Decompiled with JetBrains decompiler
// Type: TestFetchXMLandHex.IntelHexUtils
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System;
using System.Linq;

#nullable disable
namespace TestFetchXMLandHex
{
  public class IntelHexUtils
  {
    public static string extractHexData(string hex)
    {
      string hexData = "";
      for (int index = 0; index < hex.Length; ++index)
      {
        if (hex[index] == ':')
        {
          string str = hex.Substring(index + 1, 2);
          hex.Substring(index + 3, 4);
          if (hex.Substring(index + 7, 2).Equals("00"))
          {
            int int32 = Convert.ToInt32(str, 16);
            hexData += hex.Substring(index + 9, int32 * 2);
          }
        }
      }
      return hexData;
    }

    public static byte[] StringToByteArray(string hex)
    {
      return Enumerable.Range(0, hex.Length).Where<int>((Func<int, bool>) (x => x % 2 == 0)).Select<int, byte>((Func<int, byte>) (x => Convert.ToByte(hex.Substring(x, 2), 16))).ToArray<byte>();
    }

    public static byte[] extractRawData(char[] aData)
    {
      byte[] rawData = new byte[1024];
      int destinationIndex = 0;
      bool flag = false;
      for (int index = 0; index < aData.Length && !flag; ++index)
      {
        if (aData[index] == ':')
        {
          int num = Convert.ToInt32(aData[index + 1]) * 16 + Convert.ToInt32(aData[index + 2]);
          switch (Convert.ToInt32(aData[index + 7]) * 16 + Convert.ToInt32(aData[index + 8]))
          {
            case 0:
              if (destinationIndex + num * 2 > rawData.Length)
              {
                byte[] destinationArray = new byte[rawData.Length + 1024];
                Array.Copy((Array) rawData, (Array) destinationArray, rawData.Length);
                rawData = destinationArray;
              }
              Array.Copy((Array) aData, index + 9, (Array) rawData, destinationIndex, num * 2);
              destinationIndex += num * 2;
              continue;
            case 1:
              flag = true;
              continue;
            default:
              continue;
          }
        }
      }
      return rawData;
    }

    public static bool validate(byte[] aData)
    {
      bool flag = true;
      for (int index1 = 0; index1 < aData.Length && flag; ++index1)
      {
        if (aData[index1] == (byte) 58)
        {
          int num1 = Convert.ToInt32(aData[index1 + 1]) * 16 + Convert.ToInt32(aData[index1 + 2]);
          int num2 = 0;
          for (int index2 = 0; index2 < num1 * 2 + 8; index2 += 2)
            num2 += (int) (byte) Convert.ToInt32(aData[index1 + 1 + index2]) * 16 + Convert.ToInt32(aData[index1 + 1 + index2 + 1]);
          int num3 = Convert.ToInt32(aData[index1 + 9 + num1 * 2]) * 16 + Convert.ToInt32(aData[index1 + 10 + num1 * 2]);
          if (num2 % 256 * (int) byte.MaxValue != num3)
            flag = false;
          index1 = index1 + num1 * 2 + 10;
        }
      }
      return flag;
    }
  }
}
