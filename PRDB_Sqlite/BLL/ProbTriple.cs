using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRDB_Sqlite.BLL
{
    // Data trong 1 cell
    public class ProbTriple
    {
        #region Properties

        // Tập các giá trị
        // public List<Object> Value { get; set; }
      
        // Tập xác suất cận dưới
        public List<double> MinProb { get; set; }
     
        // Tập xác suất cận trên
        public List<double> MaxProb { get; set; }

        // Tập các giá trị mới
        public List<ValueOfTriple> Value2 { get; set; }


        #endregion
        #region Methods

        public ProbTriple()
        {
            // this.Value = new List<object>();
            this.MinProb = new List<double>();
            this.MaxProb = new List<double>();

            // Giá trị mới
            this.Value2 = new List<ValueOfTriple>();
        }

        public bool UniformDistribution()
        {

            for (int i = 0; i < this.MinProb.Count - 1 ; i++)
            {
                if (this.MinProb[i] != this.MinProb[i + 1])
                    return false;
                if (this.MaxProb[i] != this.MaxProb[i + 1])
                    return false;
            }

            return true;
        }



        // Tạo bộ ba xác suất từ chuỗi text
        public ProbTriple(string V)
        {
            try
            {
                // this.Value = new List<object>();
                this.MinProb = new List<double>();
                this.MaxProb = new List<double>();

                // Giá trị mới
                this.Value2 = new List<ValueOfTriple>();

                if (!V.Contains("||") && !V.Contains("{") && !V.Contains("}") && !V.Contains("[") && !V.Contains("]"))
                {
                    V = String.Format("{{{0}}}[ 1, 1]", V);
                }
                
                string[] seperator = { "||" };
                string[] value = V.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                int j1, j2, j3, j4, j5;
                for (int i = 0; i < value.Length; i++)
                {
                    j1 = value[i].IndexOf('{');
                    j2 = value[i].IndexOf('}');
                    j3 = value[i].IndexOf('[');
                    j4 = value[i].LastIndexOf(',');
                    j5 = value[i].IndexOf(']');
                    Value2.Add(new ValueOfTriple(value[i].Substring(j1 + 1, j2 - j1 - 1)));
                    MinProb.Add(Convert.ToDouble(value[i].Substring(j3 + 1, j4 - j3 - 1)));
                    MaxProb.Add(Convert.ToDouble(value[i].Substring(j4 + 1, j5 - j4 - 1)));
                }
            }
            catch
            {
               
            }
        }

        public ProbTriple(string V, string typeName)
        {
            try
            {
                // this.Value = new List<object>();
                this.MinProb = new List<double>();
                this.MaxProb = new List<double>();

                // Giá trị mới
                this.Value2 = new List<ValueOfTriple>();

                if (!V.Contains("||") && !V.Contains("{") && !V.Contains("}") && !V.Contains("[") && !V.Contains("]"))
                {
                    V = String.Format("{{{0}}}[ 1, 1]", V);
                }



                string[] seperator = { "||" };
                string[] value = V.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                int j1, j2, j3, j4, j5;
                for (int i = 0; i < value.Length; i++)
                {
                    j1 = value[i].IndexOf('{');
                    j2 = value[i].IndexOf('}');
                    j3 = value[i].IndexOf('[');
                    j4 = value[i].IndexOf(',');
                    j5 = value[i].IndexOf(']');

                  
                    if (typeName != "String")
                    {
                        Value2.Add(new ValueOfTriple(value[i].Substring(j1 + 1, j2 - j1 - 1).Replace(" ", "")));
                    }
                    else
                        Value2.Add(new ValueOfTriple(value[i].Substring(j1 + 1, j2 - j1 - 1)));

                    MinProb.Add(Convert.ToDouble(value[i].Substring(j3 + 1, j4 - j3 - 1)));
                    MaxProb.Add(Convert.ToDouble(value[i].Substring(j4 + 1, j5 - j4 - 1)));
                }

            }
            catch
            {
                MessageBox.Show("Syntax Error! Cannot convert to Probabilistic Triple!");
            }
        }

        public ProbTriple(ProbTriple triple)
        {
            // TODO: Complete member initialization

            // this.Value = new List<object>();
            this.MinProb = new List<double>();
            this.MaxProb = new List<double>();

            // Giá trị mới
            this.Value2 = new List<ValueOfTriple>();

            foreach (var item in triple.Value2)
            {
                this.Value2.Add(item);
            }

            foreach (double item in triple.MinProb)
            {
                this.MinProb.Add(item);
            }

            foreach (double item in triple.MaxProb)
            {
                this.MaxProb.Add(item);
            }


        }

        // Xuất bộ ba xác suất ra chuỗi giá trị
        public string GetStrValue()
        {
            string strValue = "";
 
            for (int i = 0; i < Value2.Count; i++)
            {
                strValue += "{ ";
                strValue += Value2[i].GetStrValue();
                strValue += " }";
                strValue += "[ ";
                strValue += MinProb[i];
                strValue += ", ";
                strValue += MaxProb[i];
                strValue += " ]";
                strValue += "  ||  ";
            }
            if(strValue != "")
                strValue = strValue.Remove(strValue.Length - 6); // loại bỏ kí tự '||' thừa

            return strValue;
        }

        #endregion


        internal bool isProbTripleValue(string Value)
        {
          //  Value = Value.Replace(" ", "");
            if (!Value.Contains("||") && !Value.Contains("{") && !Value.Contains("}") && !Value.Contains("[") && !Value.Contains("]"))
            {
                Value = String.Format("{{{0}}}[ 1, 1]", Value);
            }


            string[] seperator = { "||" };
            string[] value = Value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            int j1, j2, j3, j4, j5;
            double sumMinProb = 0.0;

            for (int i = 0; i < value.Length; i++)
            {
                j1 = value[i].IndexOf('{');
                j2 = value[i].IndexOf('}');
                j3 = value[i].IndexOf('[');
                j4 = value[i].IndexOf(',');
                j5 = value[i].IndexOf(']');
                if (j1 < 0 || j2 < 0 || j3 < 0 || j4 < 0 || j5 < 0) return false;
                if (j1 >= j2 - 1) return false;
                if (j2 >= j3) return false;
                if (j3 >= j4 - 1) return false;
                if (j4 >= j5 - 1) return false;


                try
                {
                    double minProb = Double.Parse((value[i].Substring(j3 + 1, j4 - j3 - 1)));
                    double maxProb = Double.Parse((value[i].Substring(j4 + 1, j5 - j4 - 1)));

                    if (maxProb > 1)
                        return false;

                    if (minProb > maxProb)
                        return false;
                    sumMinProb += minProb;

                }
                catch (Exception)
                {
                    return false;
                }
                    
              


            }

            if (sumMinProb > 1.0)
                return false;


            return true;
        }
    }
}
