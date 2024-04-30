// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.edtracker
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Media3D;

#nullable disable
namespace EDTrackerUI3
{
  public partial class edtracker : UserControl, IComponentConnector
  {
    //internal Viewport3D viewport3D1;
    //internal PerspectiveCamera camMain;
    //internal DirectionalLight dirLightMain;
    //internal ModelVisual3D MyModel;
    //internal MeshGeometry3D meshMain;
    //internal DiffuseMaterial matDiffuseMain;
    //internal AxisAngleRotation3D rotatePitch;
    //internal AxisAngleRotation3D rotateYaw;
    //private bool _contentLoaded;

    //public edtracker() => this.InitializeComponent();

    public void rotateHead(float yaw, float pitch)
    {
      this.Dispatcher.Invoke((Delegate) (() =>
      {
        this.rotatePitch.Angle = (double) pitch;
        this.rotateYaw.Angle = (double) yaw;
      }));
    }

    //[DebuggerNonUserCode]
    //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    //public void InitializeComponent()
    //{
    //  if (this._contentLoaded)
    //    return;
    //  this._contentLoaded = true;
    //  Application.LoadComponent((object) this, new Uri("/EDTrackerUI4;component/edtracker.xaml", UriKind.Relative));
    //}

    //[DebuggerNonUserCode]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    //void IComponentConnector.Connect(int connectionId, object target)
    //{
    //  switch (connectionId)
    //  {
    //    case 1:
    //      this.viewport3D1 = (Viewport3D) target;
    //      break;
    //    case 2:
    //      this.camMain = (PerspectiveCamera) target;
    //      break;
    //    case 3:
    //      this.dirLightMain = (DirectionalLight) target;
    //      break;
    //    case 4:
    //      this.MyModel = (ModelVisual3D) target;
    //      break;
    //    case 5:
    //      this.meshMain = (MeshGeometry3D) target;
    //      break;
    //    case 6:
    //      this.matDiffuseMain = (DiffuseMaterial) target;
    //      break;
    //    case 7:
    //      this.rotatePitch = (AxisAngleRotation3D) target;
    //      break;
    //    case 8:
    //      this.rotateYaw = (AxisAngleRotation3D) target;
    //      break;
    //    default:
    //      this._contentLoaded = true;
    //      break;
    //  }
    //}
  }
}
