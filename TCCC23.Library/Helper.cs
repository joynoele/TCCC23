using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace TCCC23.Library
{
    public interface IHelper
    {
        void TriggerDivisionByZero();
    }

    public class Helper : IHelper
    {
        private ILogger<Helper> _logger;

        public Helper(ILogger<Helper> logger)
        {
            _logger = logger;
        }

        public void TriggerDivisionByZero()
        {
            throw new DivideByZeroException("Whoops! Looks like you need to fix that.");
        }


    }
}
