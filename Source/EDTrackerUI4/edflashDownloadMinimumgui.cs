using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
[DesignerCategory("code")]
[GeneratedCode("xsd", "4.0.30319.33440")]
[DebuggerStepThrough]
[Serializable]
public class EDFlashDownloadMinimumgui
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