﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public interface IDatabaseProvider
    {
        IDatabaseConnection GetDatabase(string database);
    }
}
