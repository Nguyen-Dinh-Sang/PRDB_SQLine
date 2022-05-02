using PRDB_Sqlite.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.Util
{
    public class CompareTriple
    {
        public bool equal(ValueOfTriple triple, List<Object> condition)
        {
            if (triple.Value.Count != condition.Count)
            {
                return false;
            }

            Dictionary<string, string> check = new Dictionary<string, string>();

            foreach (String data in triple.Value)
            {
                check.Add(data.ToLower().Trim(), data);
            }

            foreach (String data in condition)
            {
                if (!check.ContainsKey(data.ToLower().Trim()))
                {
                    return false;
                }
            }

            // chỉ mới so sánh 1 chiều còn trường hợp có phần tử trùng nhau -> số lượng phần tử không đổi nhưng sẽ có phần tử khác mà không được kiểm tra.

            Dictionary<string, string> check2 = new Dictionary<string, string>();

            foreach (String data in condition)
            {
                check2.Add(data.ToLower().Trim(), data);
            }

            foreach (String data in triple.Value)
            {
                if (!check2.ContainsKey(data.ToLower().Trim()))
                {
                    return false;
                }
            }

            return true;
        }


        public bool difference(ValueOfTriple triple, List<Object> condition)
        {
            Dictionary<string, string> check = new Dictionary<string, string>();

            foreach (String data in triple.Value)
            {
                check.Add(data.ToLower().Trim(), data);
            }

            foreach (String data in condition)
            {
                if (check.ContainsKey(data.ToLower().Trim()))
                {
                    return false;
                }
            }

            Dictionary<string, string> check2 = new Dictionary<string, string>();

            foreach (String data in condition)
            {
                check2.Add(data.ToLower().Trim(), data);
            }

            foreach (String data in triple.Value)
            {
                if (check2.ContainsKey(data.ToLower().Trim()))
                {
                    return false;
                }
            }

            return true;
        }

        public bool greaterThanOrEqual(ValueOfTriple triple, List<Object> condition)
        {
            return greaterThan(triple, condition) || equal(triple, condition);
        }

        public bool lessThanOrEqual(ValueOfTriple triple, List<Object> condition)
        {
            return lessThan(triple, condition) || equal(triple, condition);
        }

        public bool greaterThan(ValueOfTriple triple, List<Object> condition)
        {
           Dictionary<string, string> check = new Dictionary<string, string>();

            foreach (String data in triple.Value)
            {
                check.Add(data.ToLower().Trim(), data);
            }

            foreach (String data in condition)
            {
                if (!check.ContainsKey(data.ToLower().Trim()))
                {
                    return false;
                }
            }

            return true;
        }

        public bool lessThan(ValueOfTriple triple, List<Object> condition)
        {
            Dictionary<string, string> check2 = new Dictionary<string, string>();

            foreach (String data in condition)
            {
                check2.Add(data.ToLower().Trim(), data);
            }

            foreach (String data in triple.Value)
            {
                if (!check2.ContainsKey(data.ToLower().Trim()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
