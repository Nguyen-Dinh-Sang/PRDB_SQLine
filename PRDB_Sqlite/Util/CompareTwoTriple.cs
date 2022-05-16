using PRDB_Sqlite.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.Util
{
    public class CompareTwoTriple
    {
        public CompareTwoTriple()
        {

        }

        public Boolean compareTriple(ValueOfTriple set1, ValueOfTriple set2)
        {
            if (set1.Value.Count != set2.Value.Count)
            {
                return false;
            }

            Dictionary<string, string> check = new Dictionary<string, string>();

            foreach (String data in set1.Value)
            {
                check.Add(data.ToLower().Trim(), data);
            }

            foreach (String data in set2.Value)
            {
                if (!check.ContainsKey(data.ToLower().Trim()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
