// Decompiled with JetBrains decompiler
// Type: edflashDownloadMinimumgui
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
[XmlType(AnonymousType = true)]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.0.30319.33440")]
[DebuggerStepThrough]
[Serializable]
public class edflashDownloadMinimumgui
{
  private string majorField;
  private string minorField;
  private string patchField;

  public string major
  {
    get => this.majorField;
    set => this.majorField = value;
  }

  public string minor
  {
    get => this.minorField;
    set => this.minorField = value;
  }

  public string patch
  {
    get => this.patchField;
    set => this.patchField = value;
  }
}
