﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotingTest
{
    public interface IBinderTest
    {
        void Function1();
        void Function2(int arg);

        int Function3();
    }
}