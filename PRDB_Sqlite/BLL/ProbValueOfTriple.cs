using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.BLL
{
    public class ValueOfTriple
    {
        public List<Object> Value { get; set; }

        public ValueOfTriple()
        {
            this.Value = new List<object>();
        }

        public ValueOfTriple(String multiValue)
        {
            this.Value = new List<object>();
            if (multiValue.Contains(','))
            {
                string[] singleValues = multiValue.Split(',');
                for (int i = 0; i < singleValues.Length; i++)
                {
                    this.Value.Add(singleValues[i]);
                }
            } else
            {
                this.Value.Add(multiValue);
            }
        }

        public string GetStrValue()
        {
            string strValue = "";

            for (int i = 0; i < Value.Count; i++)
            {
                strValue += Value[i].ToString().Trim();
                strValue += ", ";
            }
            if (strValue != "")
                strValue = strValue.Remove(strValue.Length - 2); // loại bỏ kí tự ', ' thừa

            return strValue;
        }
    }
}
