using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.BLL
{
    public class Except
    {
        private ProbRelation relationData1;
        private ProbRelation relationData2;
        private List<Double> flags;
        ProbRelation relationResult;

        public Except(ProbRelation probRelation1, ProbRelation probRelation2)
        {
            this.relationData1 = probRelation1;
            this.relationData2 = probRelation2;
            this.flags = new List<Double>(new Double[probRelation1.tuples.Count]);
            this.relationResult = new ProbRelation();
        }

        public ProbRelation equal()
        {
            //so sánh 2 dòng với nhau, mỗi dòng là 1 ProbTuple
            //so sánh 2 ô với nhau, mỗi ô là 1 ProbTriple
            //so sánh 2 tập hợp với nhau, mỗi tập hợp là 1 ValueOfTriple
            Console.WriteLine(relationData1.tuples.Count + "/" + relationData2.tuples.Count);

            for (int i = 0; i < relationData1.tuples.Count; i++)
            {
                ProbTuple row1 = relationData1.tuples[i];

                for (int j = 0; j < relationData2.tuples.Count; j++)
                {
                    ProbTuple row2 = relationData2.tuples[j];

                    if (flags[i] == 0 && compareRow(row1, row2))
                    {
                        flags[i] = 1;
                    }
                }
            }

            for (int i = 0; i < relationData1.tuples.Count; i++)
            {
                if(flags[i] == 0)
                {
                    relationResult.tuples.Add(relationData1.tuples[i]);
                }
            }

            return relationResult;
        }

        private Boolean compareRow(ProbTuple row1, ProbTuple row2)
        {
            int sizeOfRow = row1.Triples.Count;

            for (int i = 0; i < sizeOfRow; i++)
            {
                if (!compareCell(row1.Triples[i], row2.Triples[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private Boolean compareCell(ProbTriple cell1, ProbTriple cell2)
        {
            if (cell1.Value2.Count != cell2.Value2.Count)
            {
                return false;
            }

            List<Double> flagsOfSet2 = new List<Double>(new Double[cell1.Value2.Count]);

            // Vì 2 mảng lúc này dài bằng nhau
            int sizeOfCell = cell1.Value2.Count;
            for (int i = 0; i < sizeOfCell; i++)
            {
                ValueOfTriple set1 = cell1.Value2[i];

                for (int j = 0; j < sizeOfCell; j++)
                {
                    ValueOfTriple set2 = cell2.Value2[j];

                    if (flagsOfSet2[j] == 0 && compareTriple(set1, set2))
                    {
                        flagsOfSet2[j] = 1;
                    }
                }
            }

            foreach (Double flag in flagsOfSet2)
            {
                if (flag == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private Boolean compareTriple(ValueOfTriple set1, ValueOfTriple set2)
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