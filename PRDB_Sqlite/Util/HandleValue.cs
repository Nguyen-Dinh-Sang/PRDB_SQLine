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

        public List<Object> getListSingleData(string value)
        {
            List<Object> listValue = new List<object>();
            listValue = getListValueTriple(getListTriple(value));
            List<Object> listData = new List<object>();
            foreach (String singleData in listValue)
            {
                listData.AddRange(stringToMultiValue(singleData));
            }

            return listData;
        }

        public string getDataFromTriple(String value)
        {
            int startIndex = value.IndexOf("{") + 1;
            int lastIndex = value.IndexOf("}");

            return value.Substring(startIndex, (lastIndex - startIndex)).Trim();
        }

        public List<Object> getListValueTriple(List<Object> value)
        {
            List<Object> listValue = new List<object>();

            for (int i = 0; i < value.Count; i++)
            {
                listValue.Add(getDataFromTriple(value[i].ToString()));
            }

            return listValue;
        }

        public List<Object> getListTriple(String value)
        {
            List<Object> listValue = new List<object>();
            string[] separatingStrings = { "||" };
            if (value.Contains("||"))
            {
                string[] singleValues = value.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
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
