using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;

namespace IPMA_3.Model
{
    public class Temperature
    {
        private float tMin;
        public float TMin
        {
            get => tMin;
            set
            {
                tMin = value;
                New_tMed();
            }
        }

        private float tMax;
        public float TMax {
            get => tMax;
            set
            {
                tMax = value;
                New_tMed();
            }
        }
        public float tMed { get; private set; }

        public Temperature(float tMin, float tMax)
        {
            this.tMin = tMin;
            this.tMax = tMax;
            New_tMed();
        }

        private void New_tMed()
        {
            tMed = (tMax + tMin) / 2;
        }
    }
}