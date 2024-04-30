// Decompiled with JetBrains decompiler
// Type: edflashDownload
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
[GeneratedCode("xsd", "4.0.30319.33440")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[Serializable]
public class edflashDownload
{
  private string nameField;
  private string releasestateField;
  private string majorField;
  private string minorField;
  private string patchField;
  private string imageurlField;
  private string altlinkField;
  private string commentField;
  private string[] validhardwareField;
  private edflashDownloadMinimumgui minimumguiField;

  public string name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  public string releasestate
  {
    get => this.releasestateField;
    set => this.releasestateField = value;
  }

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

  public string imageurl
  {
    get => this.imageurlField;
    set => this.imageurlField = value;
  }

  public string altlink
  {
    get => this.altlinkField;
    set => this.altlinkField = value;
  }

  public string comment
  {
    get => this.commentField;
    set => this.commentField = value;
  }

  [XmlArrayItem("device", IsNullable = false)]
  public string[] validhardware
  {
    get => this.validhardwareField;
    set => this.validhardwareField = value;
  }

  public edflashDownloadMinimumgui minimumgui
  {
    get => this.minimumguiField;
    set => this.minimumguiField = value;
  }
}
