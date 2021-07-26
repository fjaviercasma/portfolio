using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace groU
{
    public class groUtils
    {
        public static object GestionarNulos(object valOriginal)
        {
            if (valOriginal == System.DBNull.Value)
                return null;
            else
                return valOriginal;
        }
    }
}