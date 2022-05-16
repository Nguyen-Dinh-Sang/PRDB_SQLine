using PRDB_Sqlite.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.Util
{
    public class HandleEqual
    {
        private ProbRelation relationData;
        ProbRelation relationResult;
        private int attributesOne;
        private int attributesTwo;
        private double min;
        private double max;
        private String condition;

        public HandleEqual(ProbRelation probRelation, int attributesOne, int attributesTwo, double min, double max, String condition)
        {
            this.relationData = probRelation;
            this.relationResult = new ProbRelation();
            this.attributesOne = attributesOne;
            this.attributesTwo = attributesTwo;
            this.min = min;
            this.max = max;
            this.condition = condition;
        }

        public ProbRelation getResult()
        {
            for (int k = 0; k < relationData.tuples.Count; k++)
            {
                ProbTuple row = relationData.tuples[k];

                /* giá trị trên mỗi cell là tập các giá trị khác nhau, không thể giống nhau vì vô nghĩa.
                 * a || a  hoặc a && a nếu khoảng xác suất khác nhau cũng vô nghĩa.
                 * */

                //
                ProbTriple cell1;
                ProbTriple cell2;

                cell1 = row.Triples[attributesOne];
                cell2 = row.Triples[attributesTwo];

                CompareTwoTriple compareTwoTriple = new CompareTwoTriple();

                List<Double> flagsOfSet1 = new List<Double>(new Double[cell1.Value2.Count]);

                for (int i = 0; i < cell1.Value2.Count; i++)
                {
                    ValueOfTriple set1 = cell1.Value2[i];

                    for (int j = 0; j < cell2.Value2.Count; j++)
                    {
                        ValueOfTriple set2 = cell2.Value2[j];

                        if (compareTwoTriple.compareTriple(set1, set2))
                        {
                            flagsOfSet1[i] = 1;
                            double sumMin = -1;
                            double sumMax = -1;

                            Console.WriteLine("Condition: " + this.condition);

                            switch (this.condition)
                            {
                                case "⊗_ig":
                                    {
                                        sumMin = Math.Max(0, (cell1.MinProb[i] + cell2.MinProb[j] - 1));
                                        sumMax = Math.Min(cell1.MaxProb[i], cell2.MaxProb[j]);
                                        break;
                                    }
                                case "⊗_in":
                                    {
                                        sumMin = cell1.MinProb[i] * cell2.MinProb[j];
                                        sumMax = cell1.MaxProb[i] * cell2.MaxProb[j];
                                        break;
                                    }

                                case "⊗_me":
                                    {
                                        sumMin = 0;
                                        sumMax = 0;
                                        break;
                                    }
                                    
                                //
                                case "⊕_ig":
                                    {
                                        sumMin = Math.Max(cell1.MinProb[i], cell2.MinProb[j]);
                                        sumMax = Math.Min(1, (cell1.MaxProb[i] + cell2.MaxProb[j]));
                                        break;
                                    }
                                case "⊕_in":
                                    {
                                        sumMin = (cell1.MinProb[i] + cell2.MinProb[j]) - (cell1.MinProb[i] * cell2.MinProb[j]);
                                        sumMax = (cell1.MaxProb[i] + cell2.MaxProb[j]) - (cell1.MaxProb[i] * cell2.MaxProb[j]);
                                        break;
                                    }
                                case "⊕_me":
                                    {
                                        sumMin = Math.Min(1, (cell1.MinProb[i] + cell2.MinProb[j]));
                                        sumMax = Math.Min(1, (cell1.MaxProb[i] + cell2.MaxProb[j]));
                                        break;
                                    }


                                //
                                case "⊖_ig":
                                    {
                                        sumMin = Math.Max(0, (cell1.MinProb[i] - cell2.MaxProb[j]));
                                        sumMax = Math.Min(cell1.MaxProb[i], (1 - cell2.MinProb[j]));
                                        break;
                                    }
                                case "⊖_in":
                                    {
                                        sumMin = cell1.MinProb[i] * (1 - cell2.MaxProb[j]);
                                        sumMax = cell1.MaxProb[i] * (1 - cell2.MinProb[j]);
                                        break;
                                    }
                                case "⊖_me":
                                    {
                                        sumMin = cell1.MinProb[i];
                                        sumMax = Math.Min(cell1.MaxProb[i], (1 - cell2.MinProb[j]));
                                        break;
                                    }
                            }

                            row.Triples[attributesOne].MinProb[i] = sumMin;
                            row.Triples[attributesOne].MaxProb[i] = sumMax;

                            row.Triples[attributesTwo].MinProb[j] = sumMin;
                            row.Triples[attributesTwo].MaxProb[j] = sumMax;  
                        }
                    }
                }


                bool check = true;
                for (int l = 0; l < cell1.Value2.Count; l++)
                {
                    if(flagsOfSet1[l] == 1)
                    {
                        if (!checkMinMax(row.Triples[attributesOne].MinProb[l], row.Triples[attributesOne].MaxProb[l]))
                        {
                            check = false;   
                        }
                    }
                }

                if(check)
                {
                    relationResult.tuples.Add(row);
                }

            }

            return relationResult;
        }

        private ProbTuple checkRow(ProbTuple row)
        {
            ProbTriple cell1;
            ProbTriple cell2;

            cell1 = row.Triples[attributesOne];
            cell2 = row.Triples[attributesTwo];

            CompareTwoTriple compareTwoTriple = new CompareTwoTriple();

            for (int i = 0; i < cell1.Value2.Count; i++)
            {
                ValueOfTriple set1 = cell1.Value2[i];

                for (int j = 0; j < cell2.Value2.Count; j++)
                {
                    ValueOfTriple set2 = cell2.Value2[j];

                    if (compareTwoTriple.compareTriple(set1, set2))
                    {
                        Console.WriteLine(set1.GetStrValue() + "/" + set2.GetStrValue());
                        Double sumMin = new Double();
                        Double sumMax = new Double();

                        switch (this.condition)
                        {
                            case "⊗_ig":
                                {
                                    sumMin = Math.Max(0, (cell1.MinProb[i] + cell2.MinProb[j] - 1));
                                    sumMax = Math.Min(cell1.MaxProb[i], cell2.MaxProb[j]);
                                    break;
                                }
                            case "⊗_in":
                                {
                                    sumMin = cell1.MinProb[i] * cell2.MinProb[j];
                                    sumMax = cell1.MaxProb[i] * cell2.MaxProb[j];
                                    break;
                                }

                            case "⊗_me":
                                {
                                    sumMin = 0;
                                    sumMax = 0;
                                    break;
                                }
                        }

                        if (!checkMinMax(sumMin, sumMax))
                        {
                            return null;
                        } else
                        {
                            row.Triples[attributesOne].MinProb[i] = sumMin;
                            row.Triples[attributesOne].MaxProb[i] = sumMax;

                            row.Triples[attributesTwo].MinProb[j] = sumMin;
                            row.Triples[attributesTwo].MaxProb[j] = sumMax;
                        }
                    }
                }
            }

            return row;
        }

        private bool checkMinMax(double min2, double max2)
        {
            decimal thisMin = (decimal)this.min;
            decimal thisMax = (decimal)this.max;

            decimal min = (decimal)min2;
            decimal max = (decimal)max2;
            
            if (thisMin <= min && max <= thisMax)
            {
                return true;
            }

            return false;
        }
    }
}
