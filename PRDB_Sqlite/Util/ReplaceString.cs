using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.Util
{
    public class ReplaceString
    {
        private String data;
        public ReplaceString(String data)
        {
            this.data = data;
        }

        public String replace()
        {
            if (data.Contains("⊆"))
            {
                return data.Replace("⊆", "<");
            }

            if (data.Contains("⊇"))
            {
                return data.Replace("⊇", ">");
            }

            if (data.Contains("⊉"))
            {
                return data.Replace("⊉", "!=");
            }

            return data;
        }
    }
}
