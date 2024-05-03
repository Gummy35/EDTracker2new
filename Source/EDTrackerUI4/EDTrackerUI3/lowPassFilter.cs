namespace EDTrackerUI3
{
    public class LowPassFilter
    {
        private float f;
        private float fac;

        public LowPassFilter()
        {
        }

        public LowPassFilter(float inp) => this.fac = inp;

        public float filter(float inp)
        {
            this.f = (float)((double)this.f * (double)this.fac + (1.0 - (double)this.fac) * (double)inp);
            return this.f;
        }
    }
}
