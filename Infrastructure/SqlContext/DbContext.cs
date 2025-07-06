using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCCMWebServiceApp.Infrastructure.SqlContext;

public static class Dbcontext
{
    public static string ConnectionString { get; private set; }




    public static void Initialize(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        Dbcontext.ConnectionString = connectionString;

    }
}

