using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.Util
{
    public class HandleValue
    {
        public List<Object> stringToMultiValue(string value)
        {
            List<Object> listValue = new List<object>();

            if (value.Contains(','))
            {
                string[] singleValues = value.Split(',');
                for (int i = 0; i < singleValues.Length; i++)
                {
                    listValue.Add(singleValues[i]);
                }
            }
            else
            {
                listValue.Add(value);
            }

            return listValue;
        }
    }
}
