﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject
{
    internal class ProgramConfig
    {
        public static readonly string DB_NAME = "CelebrationOfCapitalism";
        public static readonly string DB_SOURCE = $"{Environment.MachineName}";
        // public static readonly string DB_CONNECTION_STRING = "Data Source = SOUNDBOARD\\SQLEXPRESS; Initial Catalog = CelebrationOfCapitalism ; Integrated Security = True; TrustServerCertificate=True";
        public static readonly string DB_CONNECTION_STRING = "Data Source = SOUNDBOARD\\SQLEXPRESS; Initial Catalog = CelebrationOfCapitalism ; Integrated Security = True; TrustServerCertificate=True";
    }
}
