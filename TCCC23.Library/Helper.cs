﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TCCC23.Library
{
    public class Helper
    {
        public void TriggerDivisionByZero()
        {
            throw new DivideByZeroException("Whoops! Looks like you need to fix that.");
        }
    }
}