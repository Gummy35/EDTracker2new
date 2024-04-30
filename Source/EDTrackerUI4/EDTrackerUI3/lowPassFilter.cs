// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.lowPassFilter
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

#nullable disable
namespace EDTrackerUI3
{
  public class lowPassFilter
  {
    private float f;
    private float fac;

    public lowPassFilter()
    {
    }

    public lowPassFilter(float inp) => this.fac = inp;

    public float filter(float inp)
    {
      this.f = (float) ((double) this.f * (double) this.fac + (1.0 - (double) this.fac) * (double) inp);
      return this.f;
    }
  }
}
