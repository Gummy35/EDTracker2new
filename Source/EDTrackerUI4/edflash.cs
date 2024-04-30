// Decompiled with JetBrains decompiler
// Type: edflash
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
[GeneratedCode("xsd", "4.0.30319.33440")]
[XmlRoot(Namespace = "", IsNullable = false)]
[XmlType(AnonymousType = true)]
[DebuggerStepThrough]
[DesignerCategory("code")]
[Serializable]
public class edflash
{
  private edflashDownload[] downloadField;
  private string apilevelField;

  [XmlElement("download")]
  public edflashDownload[] download
  {
    get => this.downloadField;
    set => this.downloadField = value;
  }

  [XmlAttribute]
  public string apilevel
  {
    get => this.apilevelField;
    set => this.apilevelField = value;
  }
}
