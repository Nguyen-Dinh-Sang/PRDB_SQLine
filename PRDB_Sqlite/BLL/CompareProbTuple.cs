using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDB_Sqlite.BLL
{
    public class CompareProbTuple
    {
        private ProbRelation relationData;
        private List<Double> flags;
        ProbRelation relationResult;

        public CompareProbTuple(ProbRelation probRelation)
        {
            this.relationData = probRelation;
            this.flags = new List<Double>(new Double[probRelation.tuples.Count]);
            this.relationResult = new ProbRelation();
        }

        public ProbRelation equal()
        {
            //so sánh 2 dòng với nhau, mỗi dòng là 1 ProbTuple
            //so sánh 2 ô với nhau, mỗi ô là 1 ProbTriple
            //so sánh 2 tập hợp với nhau, mỗi tập hợp là 1 ValueOfTriple

            Dictionary<int, ProbTuple> mapResult = new Dictionary<int, ProbTuple>();
            for(int i = 0; i < relationData.tuples.Count; i++)
            {
                ProbTuple row1 = relationData.tuples[i];
                
                if (flags[i] == 0)
                {
                    mapResult.Add(i, row1);
                    flags[i] = 1;
                    
                    for (int j = 0; j < relationData.tuples.Count; j++)
                    {
                        ProbTuple row2 = relationData.tuples[j];

                        if (flags[j] == 0 && i != j && compareRow(row1, row2))
                        {
                            flags[j] = 1;
                            ProbTuple old = mapResult[i];
                            mapResult[i] = mergeRow(old, row2);
                        }
                    }
                }
            }

            foreach (KeyValuePair<int, ProbTuple> keyValue in mapResult)
            {
                relationResult.tuples.Add(keyValue.Value);
            }

            return relationResult;
        }

        private ProbTuple mergeRow(ProbTuple row1, ProbTuple row2)
        {
            ProbTuple result = row1;
            int sizeOfRow = row1.Triples.Count;
            for (int i = 0; i < sizeOfRow; i++)
            {
                result.Triples[i] = mergeCell(row1.Triples[i], row2.Triples[i]);
            }

            return result;
        }

        private ProbTriple mergeCell(ProbTriple cell1, ProbTriple cell2)
        {
            ProbTriple result = cell1;
            int sizeOfCell = cell1.Value2.Count;
            List<Double> flagsOfSet2 = new List<Double>(new Double[cell1.Value2.Count]);

            for (int i = 0; i < sizeOfCell; i++)
            {
                ValueOfTriple set1 = cell1.Value2[i];

                for (int j = 0; j < sizeOfCell; j++)
                {
                    ValueOfTriple set2 = cell2.Value2[j];

                    if (flagsOfSet2[j] == 0 && compareTriple(set1, set2))
                    {
                        flagsOfSet2[j] = 1;
                        result.MaxProb[i] = (cell1.MaxProb[i] + cell2.MaxProb[j]) - (cell1.MaxProb[i] * cell2.MaxProb[j]);

                        result.MinProb[i] = (cell1.MinProb[i] + cell1.MinProb[j]) - (cell1.MinProb[i] * cell1.MinProb[j]);
                    }
                }
            }

            return result;
        }

        private Boolean compareRow(ProbTuple row1, ProbTuple row2)
        {
            int sizeOfRow = row1.Triples.Count;

            for (int i = 0; i < sizeOfRow; i++)
            {
                if(!compareCell(row1.Triples[i], row2.Triples[i]))
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
            for (int i = 0; i < sizeOfCell; i ++)
            {
                ValueOfTriple set1 = cell1.Value2[i];

                for(int j = 0; j < sizeOfCell; j++)
                {
                    ValueOfTriple set2 = cell2.Value2[j];

                    if (flagsOfSet2[j] == 0 && compareTriple(set1, set2))
                    {
                        flagsOfSet2[j] = 1;
                    }
                }
            }

            foreach(Double flag in flagsOfSet2)
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

            foreach(String data in set1.Value)
            {
                check.Add(data.ToLower().Trim(), data);
            }

            foreach(String data in set2.Value)
            {
                if (!check.ContainsKey(data.ToLower().Trim())) {
                    return false;
                }
            }

            return true;
        }
    }
}
