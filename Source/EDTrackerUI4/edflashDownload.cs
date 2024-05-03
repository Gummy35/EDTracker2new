using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
[GeneratedCode("xsd", "4.0.30319.33440")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[Serializable]
public class EDFlashDownload
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
    private EDFlashDownloadMinimumgui minimumguiField;

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

    public EDFlashDownloadMinimumgui minimumgui
    {
        get => this.minimumguiField;
        set => this.minimumguiField = value;
    }
}
