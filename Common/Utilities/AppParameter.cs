using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MCCMWebServiceApp.Common.Utilities;

public class AppParameter
{
    public Test test { get; set; }

    public string appParameter { get; set; }


    public class Test
    {

        public string test { get; set; }

    }
}

