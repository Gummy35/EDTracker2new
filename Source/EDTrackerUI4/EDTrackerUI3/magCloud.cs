// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.magCloud
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

#nullable disable
namespace EDTrackerUI3
{
  public partial class magCloud : UserControl, IComponentConnector
  {
    private DispatcherTimer dispatcherTimer;
    //internal Viewport3D mainViewport;
    //internal PerspectiveCamera camMain;
    //internal DirectionalLight dirLightMain;
    //internal ModelVisual3D MyModel;
    //internal AxisAngleRotation3D rotatePitch;
    //internal AxisAngleRotation3D rotateYaw;
    //private bool _contentLoaded;

    public magCloud()
    {
      this.InitializeComponent();
      this.dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render);
      this.dispatcherTimer.Tick += new EventHandler(this.dispatcherTimer_Tick);
      this.dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 40);
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) => this.rotateYaw.Angle += 2.0;

    public void Show()
    {
      this.Visibility = Visibility.Visible;
      this.dispatcherTimer.Start();
    }

    public void Hide()
    {
      this.dispatcherTimer.Stop();
      this.Visibility = Visibility.Collapsed;
    }

    private MeshGeometry3D GenerateSphere(Point3D center, double radius, int slices, int stacks)
    {
      MeshGeometry3D sphere = new MeshGeometry3D();
      for (int index1 = 0; index1 <= stacks; ++index1)
      {
        double num1 = Math.PI / 2.0 - (double) index1 * Math.PI / (double) stacks;
        double y = radius * Math.Sin(num1);
        double num2 = -radius * Math.Cos(num1);
        for (int index2 = 0; index2 <= slices; ++index2)
        {
          double num3 = (double) (index2 * 2) * Math.PI / (double) slices;
          double x = num2 * Math.Sin(num3);
          double z = num2 * Math.Cos(num3);
          Vector3D vector3D = new Vector3D(x, y, z);
          sphere.Normals.Add(vector3D);
          sphere.Positions.Add(vector3D + center);
          sphere.TextureCoordinates.Add(new Point((double) index2 / (double) slices, (double) index1 / (double) stacks));
        }
      }
      for (int index3 = 0; index3 < stacks; ++index3)
      {
        for (int index4 = 0; index4 < slices; ++index4)
        {
          int num = slices + 1;
          if (index3 != 0)
          {
            sphere.TriangleIndices.Add(index3 * num + index4);
            sphere.TriangleIndices.Add((index3 + 1) * num + index4);
            sphere.TriangleIndices.Add(index3 * num + index4 + 1);
          }
          if (index3 != stacks - 1)
          {
            sphere.TriangleIndices.Add(index3 * num + index4 + 1);
            sphere.TriangleIndices.Add((index3 + 1) * num + index4);
            sphere.TriangleIndices.Add((index3 + 1) * num + index4 + 1);
          }
        }
      }
      return sphere;
    }

    public void Simple3DSceneInCode(Vector3D[] rawPoints, Vector3D[] xformPoints, int pCount)
    {
      this.MyModel.Content = (Model3D) null;
      Model3DGroup model3Dgroup = new Model3DGroup();
      MeshGeometry3D sphere = this.GenerateSphere(new Point3D(0.0, 0.0, 0.0), 3.0, 6, 6);
      sphere.Freeze();
      DiffuseMaterial diffuseMaterial1 = new DiffuseMaterial((Brush) Brushes.Red);
      DiffuseMaterial diffuseMaterial2 = new DiffuseMaterial((Brush) Brushes.LightGreen);
      for (int index = 0; index < pCount; ++index)
      {
        GeometryModel3D geometryModel3D = new GeometryModel3D();
        geometryModel3D.Geometry = (Geometry3D) sphere;
        geometryModel3D.Material = (Material) diffuseMaterial1;
        geometryModel3D.BackMaterial = (Material) diffuseMaterial1;
        double x = rawPoints[index].X;
        double y = rawPoints[index].Y;
        double z = rawPoints[index].Z;
        geometryModel3D.Transform = (Transform3D) new TranslateTransform3D(x, y, z);
        geometryModel3D.Freeze();
        model3Dgroup.Children.Add((Model3D) geometryModel3D);
      }
      for (int index = 0; index < pCount; ++index)
      {
        GeometryModel3D geometryModel3D = new GeometryModel3D();
        geometryModel3D.Geometry = (Geometry3D) sphere;
        geometryModel3D.Material = (Material) diffuseMaterial2;
        geometryModel3D.BackMaterial = (Material) diffuseMaterial2;
        double x = xformPoints[index].X;
        double y = xformPoints[index].Y;
        double z = xformPoints[index].Z;
        geometryModel3D.Transform = (Transform3D) new TranslateTransform3D(x, y, z);
        geometryModel3D.Freeze();
        model3Dgroup.Children.Add((Model3D) geometryModel3D);
      }
      this.MyModel.Content = (Model3D) model3Dgroup;
      this.rotateYaw.Angle = 0.0;
    }

    public void compensate(Vector3D[] points, int cnt, Matrix<double> C, double[] offsets)
    {
      MatrixBuilder<double> build1 = Matrix<double>.Build;
      VectorBuilder<double> build2 = Vector<double>.Build;
      int rows = cnt;
      Matrix<double> other1 = build1.Dense(rows, 1);
      Matrix<double> other2 = build1.Dense(rows, 1);
      Matrix<double> other3 = build1.Dense(rows, 1);
      Random random = new Random();
      double num1 = 999.9;
      double num2 = -9999.99;
      for (int rowIndex = 0; rowIndex < cnt; ++rowIndex)
      {
        double num3 = 2.0 * random.NextDouble() - 1.0;
        double num4 = 2.0 * random.NextDouble() - 1.0;
        double num5 = 2.0 * random.NextDouble() - 1.0;
        double num6 = 6.0 * Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5) * (0.8 + random.NextDouble() * 0.2);
        double num7 = num3 / num6 + 0.2;
        double num8 = num4 / num6 + 0.3;
        double num9 = num5 / num6 + 0.5;
        double num10 = num8 * 0.22100000083446503;
        double num11 = num9 * 0.654;
        double num12 = 0.3876;
        Math.Cos(num12);
        Math.Sin(num12);
        Math.Sin(num12);
        Math.Cos(num12);
        other1.SetRow(rowIndex, new double[1]
        {
          points[rowIndex].X + 500.0
        });
        other2.SetRow(rowIndex, new double[1]
        {
          points[rowIndex].Y + 500.0
        });
        other3.SetRow(rowIndex, new double[1]
        {
          points[rowIndex].Z + 500.0
        });
        if (points[rowIndex].X > num2)
          num2 = points[rowIndex].X;
        if (points[rowIndex].X < num1)
          num1 = points[rowIndex].X;
      }
      Matrix<double> matrix1 = build1.Dense(other1.RowCount, 9);
      matrix1.SetSubMatrix(0, 0, other1.PointwiseMultiply(other1));
      matrix1.SetSubMatrix(0, 1, other2.PointwiseMultiply(other2));
      matrix1.SetSubMatrix(0, 2, other3.PointwiseMultiply(other3));
      matrix1.SetSubMatrix(0, 3, other1.PointwiseMultiply(other2) * 2.0);
      matrix1.SetSubMatrix(0, 4, other1.PointwiseMultiply(other3) * 2.0);
      matrix1.SetSubMatrix(0, 5, other2.PointwiseMultiply(other3) * 2.0);
      matrix1.SetSubMatrix(0, 6, other1 * 2.0);
      matrix1.SetSubMatrix(0, 7, other2 * 2.0);
      matrix1.SetSubMatrix(0, 8, other3 * 2.0);
      Matrix<double> matrix2 = matrix1.Transpose() * matrix1;
      Matrix<double> matrix3 = build1.Dense(rows, 1);
      matrix3.Clear();
      Matrix<double> matrix4 = matrix3.Add(1.0);
      Matrix<double> matrix5 = matrix1.Transpose() * matrix4;
      Matrix<double> matrix6 = matrix2.Inverse() * matrix5;
      Matrix<double> other4 = build1.DenseOfColumnArrays(new double[4]
      {
        matrix6[0, 0],
        matrix6[3, 0],
        matrix6[4, 0],
        matrix6[6, 0]
      }, new double[4]
      {
        matrix6[3, 0],
        matrix6[1, 0],
        matrix6[5, 0],
        matrix6[7, 0]
      }, new double[4]
      {
        matrix6[4, 0],
        matrix6[5, 0],
        matrix6[2, 0],
        matrix6[8, 0]
      }, new double[4]
      {
        matrix6[6, 0],
        matrix6[7, 0],
        matrix6[8, 0],
        -1.0
      });
      Matrix<double> matrix7 = -1.0 * other4.SubMatrix(0, 3, 0, 3).Inverse() * matrix6.SubMatrix(6, 3, 0, 1);
      offsets[0] = matrix7[0, 0] - 500.0;
      offsets[1] = matrix7[1, 0] - 500.0;
      offsets[2] = matrix7[2, 0] - 500.0;
      Matrix<double> matrix8 = build1.DenseIdentity(4);
      matrix8.SetSubMatrix(3, 0, matrix7.Transpose());
      Matrix<double> matrix9 = matrix8.Multiply(other4).Multiply(matrix8.Transpose());
      Evd<double> evd = matrix9.SubMatrix(0, 3, 0, 3).Divide(-matrix9[3, 3]).Evd();
      Matrix<double> matrix10 = build1.Dense(3, 3);
      matrix10.SetSubMatrix(0, 0, evd.EigenVectors.SubMatrix(0, 3, 0, 1));
      matrix10.SetSubMatrix(0, 1, evd.EigenVectors.SubMatrix(0, 3, 1, 1));
      matrix10.SetSubMatrix(0, 2, evd.EigenVectors.SubMatrix(0, 3, 2, 1));
      matrix10.SetSubMatrix(0, 3, 0, 3, evd.EigenVectors);
      Matrix<double> matrix11 = build1.DenseOfColumnArrays(new double[3]
      {
        Math.Sqrt(1.0 / evd.EigenValues[0].Real),
        Math.Sqrt(1.0 / evd.EigenValues[1].Real),
        Math.Sqrt(1.0 / evd.EigenValues[2].Real)
      });
      Matrix<double> matrix12 = build1.Dense(3, 3);
      matrix12.Clear();
      matrix12[0, 0] = matrix11[0, 0];
      matrix12[1, 1] = matrix11[1, 0];
      matrix12[2, 2] = matrix11[2, 0];
      Matrix<double> other5 = matrix12.Inverse().Multiply(matrix11.Enumerate().Min());
      Matrix<double> other6 = matrix10.Transpose();
      matrix10.Multiply(other5).Multiply(other6).CopyTo(C);
    }

    public void doItToIt(Vector3D[] rawPoints, int pCount, float[] offset, Matrix<double> m)
    {
      Vector3D[] xformPoints = new Vector3D[2000];
      double[] offsets = new double[3];
      this.compensate(rawPoints, pCount, m, offsets);
      for (int index = 0; index < pCount; ++index)
      {
        Vector<double> vector = Vector<double>.Build.DenseOfArray(new double[3]
        {
          rawPoints[index].X - offsets[0],
          rawPoints[index].Y - offsets[1],
          rawPoints[index].Z - offsets[2]
        }) * m;
        xformPoints[index].X = vector[0];
        xformPoints[index].Y = vector[1];
        xformPoints[index].Z = vector[2];
      }
      this.Simple3DSceneInCode(rawPoints, xformPoints, pCount);
      for (int index = 0; index < 3; ++index)
        offset[index] = (float) offsets[index];
    }

    //[DebuggerNonUserCode]
    //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    //public void InitializeComponent()
    //{
    //  if (this._contentLoaded)
    //    return;
    //  this._contentLoaded = true;
    //  Application.LoadComponent((object) this, new Uri("/EDTrackerUI4;component/magcloud.xaml", UriKind.Relative));
    //}

    //[DebuggerNonUserCode]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    //void IComponentConnector.Connect(int connectionId, object target)
    //{
    //  switch (connectionId)
    //  {
    //    case 1:
    //      this.mainViewport = (Viewport3D) target;
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
    //      this.rotatePitch = (AxisAngleRotation3D) target;
    //      break;
    //    case 6:
    //      this.rotateYaw = (AxisAngleRotation3D) target;
    //      break;
    //    default:
    //      this._contentLoaded = true;
    //      break;
    //  }
    //}
  }
}
