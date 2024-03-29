﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmcLib
{
    public class MAM
    {
        static double[] mam = {1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
                               1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
                               1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 
                               1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 
                               1.000, 1.010, 1.020, 1.031, 1.043, 
                               1.055, 1.068, 1.082, 1.097, 1.113,
                               1.130, 1.147, 1.165, 1.184, 1.204, 
                               1.225, 1.246, 1.268, 1.291, 1.315,
                               1.340, 1.366, 1.393, 1.421, 1.450, 
                               1.480, 1.511, 1.543, 1.576, 1.610,
                               1.645, 1.681, 1.718, 1.756, 1.795, 
                               1.835, 1.876, 1.918, 1.961, 2.005,
                               2.050, 2.050, 2.050, 2.050, 2.050,
                               2.050, 2.050, 2.050, 2.050, 2.050,
                               2.050
                              };

        public static double GetMAM(int age)
        {
            return mam[age - 1];
        }
        
    }
}
