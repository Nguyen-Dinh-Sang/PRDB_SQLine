using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRDB_Sqlite.BLL
{
   public class QueryExecution
   {
       public List<ProbRelation> selectedRelations { get; set; }
       public ProbRelation relationResult { get; set; }
       public List<ProbAttribute> selectedAttributes;
       public ProbDatabase probDatabase { get; set; }
       public string conditionString { get; set; }
       public bool flagNaturalJoin { get; set; }      
       private string OperationNaturalJoin = string.Empty;
       public string MessageError { get; set; }
       public ProbRelation DescartesAndNaturalJoin { get; set; }

       public QueryExecution(ProbDatabase probDatabase)
       {
           this.selectedRelations = new List<ProbRelation>();
           this.selectedAttributes = new List<ProbAttribute>();
           this.relationResult = new ProbRelation();
           this.selectedAttributes = new List<ProbAttribute>();
           this.probDatabase = probDatabase;
           this.flagNaturalJoin = false;
       }

      private static string StandardizeQuery(string queryString)
       {
            Console.WriteLine("StandardizeQuery");
           try
           {
               string result = "";
               string S = queryString;
               for (int i = 0; i < S.Length; i++)
                   if (S[i] == ' ')
                   {
                       if (S[i - 1] != ' ') 
                           result += S[i];
                   }
                   else 
                       result += S[i];

               result = result.Replace("\n", " ");
               return result.ToLower();
           }
           catch (Exception)
           {              
               return null;
           }
       }
       
    
       #region new

     

       private ProbRelation Descartes()
       {
            Console.WriteLine("Descartes");
            ProbRelation relation = new ProbRelation();
          

            relation.ListRenameRelation.Add(this.selectedRelations[0].RelationName);
            foreach (ProbAttribute attr in this.selectedRelations[0].Scheme.Attributes)
            {
                if(attr.AttributeName.IndexOf(".") == -1)
                    attr.AttributeName = this.selectedRelations[0].RelationName + "." + attr.AttributeName;
            }          
            relation.Scheme.Attributes.AddRange(this.selectedRelations[0].Scheme.Attributes);

           
            relation.ListRenameRelation.Add(this.selectedRelations[1].RelationName);
            foreach (ProbAttribute attr in this.selectedRelations[1].Scheme.Attributes)
            {
                if (attr.AttributeName.IndexOf(".") == -1)
                     attr.AttributeName = this.selectedRelations[1].RelationName +"." +attr.AttributeName;
            }
            
            relation.Scheme.Attributes.AddRange(this.selectedRelations[1].Scheme.Attributes);
           

           foreach (ProbTuple tupleOne in this.selectedRelations[0].tuples)
           {               

               foreach (ProbTuple tupleTwo in this.selectedRelations[1].tuples)
               {
                  ProbTuple value = new ProbTuple();
                  value.Triples.AddRange(tupleOne.Triples);
                  value.Triples.AddRange(tupleTwo.Triples);
                  relation.tuples.Add(value);
               }
           }

           return relation;           
       }
       private static ProbTriple JoinTwoTriple(ProbTriple tripleOne, ProbTriple tripleTwo, ProbAttribute attribute, string OperationNaturalJoin)
       {
            Console.WriteLine("JoinTwoTriple");
           ProbTriple triple = new ProbTriple();

            for (int i = 0; i < tripleOne.Value2.Count; i++)
            {
                // Tập hợp trong 1 bộ 3
                ValueOfTriple valueOfTriple = tripleOne.Value2[i];

                for (int ii = 0; ii < valueOfTriple.Value.Count; ii++)
                {
                    for (int j = 0; j < tripleTwo.Value2.Count; j++)
                    {
                        // Tập hợp trong 1 bộ 3
                        ValueOfTriple valueOfTriple2 = tripleTwo.Value2[j];

                        for (int jj = 0; jj < valueOfTriple2.Value.Count; jj++)
                        {
                            if (SelectCondition.EQUAL(valueOfTriple.Value[ii].ToString().Trim(), valueOfTriple2.Value[jj].ToString().Trim(), attribute.Type.DataType))
                            {
                                switch (OperationNaturalJoin)
                                {
                                    case "in":
                                        triple.Value2.Add(tripleOne.Value2[i]);
                                        triple.MinProb.Add(tripleOne.MinProb[i] * tripleTwo.MinProb[j]);
                                        triple.MaxProb.Add(tripleOne.MaxProb[i] * tripleTwo.MaxProb[j]);

                                        break;

                                    case "ig":

                                        triple.Value2.Add(tripleOne.Value2[i]);
                                        triple.MinProb.Add(Math.Max(0, tripleOne.MinProb[i] + tripleTwo.MinProb[j] - 1));
                                        triple.MaxProb.Add(Math.Min(tripleOne.MaxProb[i], tripleTwo.MaxProb[j]));
                                        break;

                                    case "me":

                                        triple.Value2.Add(tripleOne.Value2[i]);
                                        triple.MinProb.Add(0);
                                        triple.MaxProb.Add(0);
                                        break;
                                    default: break;
                                }

                            }
                        }
                    }
                }
            }

            return triple.Value2.Count <= 0 ? null : triple;
       }

        private ProbRelation NaturalJoin()
       {
            Console.WriteLine("NaturalJoin");
           ProbRelation relation = Descartes();
           List<int> indexsRemove = new List<int>();
          
           for (int i = 0; i < relation.Scheme.Attributes.Count - this.selectedRelations[1].Scheme.Attributes.Count ; i++)
           {

               for (int j = this.selectedRelations[1].Scheme.Attributes.Count ; j < relation.Scheme.Attributes.Count; j++)
               {
                   if ( i != j && relation.Scheme.Attributes[i].Type.DataType == relation.Scheme.Attributes[j].Type.DataType)
                   {
                       string attributeOne = relation.Scheme.Attributes[i].AttributeName.Substring(relation.Scheme.Attributes[i].AttributeName.IndexOf(".")+1);
                       string attributeTwo = relation.Scheme.Attributes[j].AttributeName.Substring(relation.Scheme.Attributes[j].AttributeName.IndexOf(".")+1);

                       if (attributeOne.Equals(attributeTwo, StringComparison.CurrentCultureIgnoreCase))
                       {
                           indexsRemove.Add(j);

                           for (int k = relation.tuples.Count - 1; k >= 0; k--)
                           {
                               ProbTriple triple = JoinTwoTriple(relation.tuples[k].Triples[i], relation.tuples[k].Triples[j], relation.Scheme.Attributes[i], this.OperationNaturalJoin);
                               if (triple != null)
                               {
                                   relation.tuples[k].Triples[i] = triple;
                                   relation.tuples[k].Triples[j] = triple;
                               }
                               else
                               {
                                   relation.tuples.RemoveAt(k);

                               }
                           }
                       }
                   }
               }
           }
                      
           for (int i = 0; i < indexsRemove.Count; i++)
           {
               
               foreach (ProbTuple tuple in relation.tuples)
               {
                   tuple.Triples.RemoveAt( indexsRemove[i] );     
               }
               relation.Scheme.Attributes.RemoveAt(indexsRemove[i]);
               this.selectedAttributes.RemoveAt(indexsRemove[i]);
           }


         

           
           OperationNaturalJoin = string.Empty;
           flagNaturalJoin = false;
           return relation;
       }
       
       private bool CheckStringQuery(string stringQuery)
       {
            Console.WriteLine("CheckStringQuery");
           //////////////////////// Check Syntax ////////////////////////
           int indexSelect = stringQuery.IndexOf("select");
           int indexFrom = stringQuery.IndexOf("from");
           int indexWhere = stringQuery.IndexOf("where");
           int indexStart = stringQuery.IndexOf("*");
           int indexLastSelect = stringQuery.LastIndexOf("select");
           int indexLastFrom = stringQuery.IndexOf("from");
           int indexLastWhere = stringQuery.IndexOf("where");
           int indexLastStart = stringQuery.IndexOf("*");
           

           if (indexSelect == -1)
           {
               MessageError = "Syntax Error! Not Found keyword 'select' ";
               return false;
           }

        


           if (indexFrom == -1)
           {
               MessageError = "Syntax Error! Not Found FROM statement";
               return false;
           }




           if (indexWhere != -1)
           {

               if (indexSelect < indexFrom && indexFrom < indexWhere)
               {                  

               }
               else
               {
                   MessageError = "Syntax Error! The keyword must theo order 'Select From Where' ";
                   return false;
               }

           }
           else
           {
               if (indexSelect < indexFrom && indexWhere == -1)
               {

               }
               else
               {
                   MessageError = "Syntax Error! The keyword must theo order 'Select From ' ";
                   return false;
               }

           
           }

          


           


           if (indexSelect != indexLastSelect)
           {
               MessageError = "Syntax Error! In query statement only have a keyword 'select' ";
               return false;
           }

           if (indexFrom != indexLastFrom)
           {
               MessageError = "Syntax Error! In query statement only have a keyword 'from' ";
               return false;
           }

           if (indexLastWhere != indexWhere)
           {
               MessageError = "Syntax Error! In query statement only have a keyword 'where' ";
               return false;
           }
             

           #region check kí tự đặc biệt

           char[] SpecialCharacter = new char[] { '~', '!', '@', '#', '$', '%', '^', '&', '[', ']', '(', ')', '+', '`', ';', '<', '>', '?', '/', ':', '\"', '\'', '=', '{', '}', '\\', '|' };
           string specialcharacter = string.Empty;
           for (int i = 0; i < SpecialCharacter.Length; i++)
               specialcharacter += SpecialCharacter[i];

           string subString = string.Empty;
           if (stringQuery.Contains("where"))
           {
               int pOne = stringQuery.IndexOf("select") + 6;
               int pTwo = stringQuery.IndexOf("where") - 1;
               subString = stringQuery.Substring(pOne, pTwo);
               for (int i = 0; i < subString.Length; i++)
               {
                   if (specialcharacter.Contains(subString[i]))
                   {

                       MessageError = String.Format("Error: Do not input the special character '{0}' in query statement", subString[i]);
                       return false;

                   }
               }
           }
           else
           {
               for (int i = 0; i < stringQuery.Length; i++)
               {
                   if (specialcharacter.Contains(stringQuery[i]))
                   {
                       MessageError = String.Format("Error: Do not input the special character '{0}' in query statement", stringQuery[i]);
                       return false;

                   }
               }

           }
           #endregion





           return true;
       }
       private static bool CheckStringAttribute(string stringAttribute)
       {
            Console.WriteLine("CheckStringAttribute");
           string subString = stringAttribute;

           if (subString.Trim().Length <= 0)
               return false;

           for (int i = 0; i < subString.Length - 1; i++)
           {
               if (subString.ElementAt(i) == subString.ElementAt(i + 1) && subString.ElementAt(i) == ',')
                   return false;
           }

           if (subString.LastIndexOf(",") == subString.Length - 1)
           {
               return false;
           }


           return true;
       }
       private List<ProbAttribute> GetAttribute(string valueString)
       {
            Console.WriteLine("GetAttribute");
           List<ProbAttribute> listProbAttribute = new List<ProbAttribute>();
           //////////////////////// Get Attributes //////////////////////
           int posOne, posTwo;


           // * là chọn tất cả các thuộc tính
           if (valueString.Contains("*"))
           {
               posOne = valueString.IndexOf("*");                                                   // start postion of attributes
               posTwo = valueString.IndexOf("from ") - 1;

               if (posOne > posTwo)
               {
                   MessageError = "Incorrect syntax near 'from'.";
                   return null;
               }


               if (posOne < valueString.IndexOf("select") )
               {
                   MessageError = "Incorrect syntax near 'select'.";
                   return null;
               }

               if (valueString.Contains("where") && posOne > valueString.IndexOf("where"))
               {
                   MessageError = "Incorrect syntax near 'where'.";
                   return null;
               }

               if (posOne != valueString.LastIndexOf("*"))
               {
                   MessageError = "Incorrect syntax near 'select'.";
                   return null;
               }
               
               // end postion of attributes
               string attributes = valueString.Substring(posOne, posTwo - posOne + 1);

               // Nếu như phia sau dấu * có bất kì kí tự nào thì sẽ thông báo lỗi
               if (attributes.Trim().Length > 1)
               {
                   MessageError = "Incorrect syntax near 'select'.";                  
                   return null;
               }
                              
               // thực hiện sao chép toàn bộ thuộc tính của các quan hệ vào danh sách thuộc tính chọn
               for (int i = 0; i < this.selectedRelations.Count; i++)
               {
                    foreach (ProbAttribute attr in this.selectedRelations[i].Scheme.Attributes)
                   {
                       attr.AttributeName = String.Format("{0}.{1}", this.selectedRelations[i].RelationName, attr.AttributeName);
                       listProbAttribute.Add(attr);
                   }
                   
               }

               return listProbAttribute;

           }
           else // ngược lại là xuất theo thuộc tính chỉ định
           {

               posOne = valueString.IndexOf("select") + 6;                                                   // start postion of attributes
               posTwo = valueString.IndexOf("from ") - 1;                                                    // end postion of attributes


               string attributes = valueString.Substring(posOne, posTwo - posOne + 1);
               
               //kiểm tra cú pháp của chuổi thuộc tính
               if (!QueryExecution.CheckStringAttribute(attributes))
               {
                   MessageError = "Incorrect syntax near 'select'.";                                                
                   return null;

               }
               else
               {
                   string[] seperator = { "," };
                   string[] attribute = attributes.Split(seperator, StringSplitOptions.RemoveEmptyEntries); // split thành mảng các thuộc tính                    

                   foreach (string str in attribute)
                   {
                       if (!str.Contains("."))
                       {
                           string attributeName = str.Trim();
                           int countOne = 0;
                           int countSameAttribute = 0;
                           foreach (ProbRelation relation in this.selectedRelations)
                           {
                               List<string> listOfAttributeName = relation.Scheme.ListOfAttributeNameToLower();
                               if (listOfAttributeName.Contains(attributeName.ToLower()))
                               {
                                   ProbAttribute attr = new ProbAttribute(relation.Scheme.Attributes[listOfAttributeName.IndexOf(attributeName)]);
                                   attr.AttributeName = String.Format("{0}.{1}", relation.RelationName, attr.AttributeName);
                                   listProbAttribute.Add(attr);
                                   countSameAttribute++;
                               }
                               else
                               {
                                   countOne++;
                               }

                           }

                           if (countOne == this.selectedRelations.Count)
                           {
                               MessageError = String.Format(" Invalid attribute name '{0}'.", attributeName);
                                 return null;

                           }

                           if (countSameAttribute == this.selectedRelations.Count && this.selectedRelations.Count >= 2)
                           {
                               MessageError = String.Format(" Ambiguous attribute name '{0}'.", attributeName);
                               return null;

                           }
                       }
                       else
                       {
                           string[] array = str.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                           if (array.Length != 2)
                           {
                               MessageError = "Incorrect syntax near the keyword 'select'.";
                               return null;
                           }

                           ProbRelation relation = this.selectedRelations.SingleOrDefault(c => c.RelationName.Trim() == array[0].Trim());

                           if (relation == null)
                           {
                               MessageError = String.Format("The multi-part identifier '{0}' could not be bound.", str);
                               return null;
                           }

                           ProbAttribute attr = new ProbAttribute(relation.Scheme.Attributes.SingleOrDefault(c => c.AttributeName.Trim().ToLower() == array[1].Trim()));
                           attr.AttributeName = String.Format("{0}.{1}", relation.RelationName, attr.AttributeName);

                           if (attr == null)
                           {
                               MessageError = "Invalid attribute name '" + array[1] + "'.";
                               return null;
                           }


                           listProbAttribute.Add(attr);

                       }
                   }

                  return listProbAttribute;


               }
           }

            
       }
       private List<ProbRelation> GetAllRelation(string valueString)
       {
            Console.WriteLine("GetAllRelation");
           int posOne;
           int posTwo;
           string relationsString = string.Empty;
           List<string> listOfRelationName = this.probDatabase.ListOfRelationNameToLower();
           string[] seperator = {"," };
           string[] relations;
           List<ProbRelation> probRelations = new List<ProbRelation>();

           //////////////////////// Get Relations ///////////////////////
           posOne = valueString.IndexOf("from") + 4;

           if (!valueString.Contains("where"))
               posTwo = valueString.Length - 1;
           else
               posTwo = valueString.IndexOf("where") - 1;

           relationsString = valueString.Substring(posOne, posTwo - posOne + 1);   // Get Relation in the Query Text     


           if (relationsString.Trim().Length <= 0)
           {
               MessageError = "No relation exists in the query !";       
              return null;
           }

           if (relationsString.Contains(","))
           {
               relations = relationsString.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
           }
            else
               if (relationsString.Contains(" natural join in") || relationsString.Contains(" natural join ig") || relationsString.Contains(" natural join me"))
               {
                   relations = new string[2];

                   if (relationsString.Contains(" natural join in"))
                   {
                       relations[0] = relationsString.Substring(0, relationsString.IndexOf("natural join in")).Trim();
                       relations[1] = relationsString.Substring(relationsString.IndexOf("natural join in") + 15).Trim();
                       OperationNaturalJoin = "in";
                   }
                   else
                       if (relationsString.Contains(" natural join ig"))
                       {
                           relations[0] = relationsString.Substring(0, relationsString.IndexOf("natural join ig")).Trim();
                           relations[1] = relationsString.Substring(relationsString.IndexOf("natural join ig") + 15).Trim();
                           OperationNaturalJoin = "ig";
                       }
                       else
                       {
                           relations[0] = relationsString.Substring(0, relationsString.IndexOf("natural join me")).Trim();
                           relations[1] = relationsString.Substring(relationsString.IndexOf("natural join me") + 15).Trim();
                           OperationNaturalJoin = "me";
                       }
                                      
                   foreach (string item in relations)
                   {
                       if (item == "")
                       {
                           MessageError = "Incorrect syntax near 'from'.";             
                           return null;
                       }
                       string[] listTmp = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                       if (listTmp.Length != 1)
                       {
                           MessageError = "Incorrect syntax near 'from'.";
                           return null;
                       }


                   }
                   flagNaturalJoin = true;
               }
               else
               {
                   relations = new string[1];
                   relations[0] = relationsString;
               }

           

           if (relations.Length > 2)
           {
               MessageError = "The query only accept one or two relation.";                 
               return null;
           }


           if (relations.Length == 1)
           {
               string[] listTmp = relations[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

               if (listTmp.Length > 2)
               {
                   MessageError = "Incorrect syntax near 'from'.";
                   return null;
               }

               if (!listOfRelationName.Contains(listTmp[0].ToLower()))
               {
                   MessageError = String.Format("Invalid relation name '{0}'.", listTmp[0]);
                   return null;
               }


           }
           else
           {

               for (int i = 0; i < relations.Length; i++)
               {
                   string[] listTmp = relations[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                   if (listTmp.Length > 2)
                   {
                       MessageError = "Incorrect syntax near 'from'.";
                       return null;
                   }

                   if (!listOfRelationName.Contains(listTmp[0].ToLower()))
                   {
                       MessageError = String.Format("Invalid relation name '{0}'.", listTmp[0]);
                       return null;
                   }
               }
           }


           for (int i = 0; i < relations.Length; i++)
           {
               string[] listTmp = relations[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
               ProbRelation tmp = this.probDatabase.Relations.SingleOrDefault(c => c.RelationName.ToLower().Equals(listTmp[0], StringComparison.OrdinalIgnoreCase));

               
               ProbRelation rela = new ProbRelation();
               if (listTmp.Length == 2)
               {
                   rela.RelationName = listTmp[1];
               }
               else
                   rela.RelationName = tmp.RelationName;

               rela.Scheme = new ProbScheme(-1, tmp.Scheme.SchemeName, tmp.Scheme.Attributes);

               foreach (ProbTuple item in tmp.tuples)
               {
                   ProbTuple tuple = new ProbTuple(item);
                   rela.tuples.Add(tuple);
               }
                                             
               
               probRelations.Add(rela);
           }

           
           if (probRelations.Count == 2)
           {

               if ( probRelations[0].RelationName == probRelations[1].RelationName )
               {
                   MessageError = String.Format("The correlation name '{0}' is specified multiple times in a FROM clause.", probRelations[0].RenameRelationName);     
                   return null;
               }           

               
           }

           foreach (ProbRelation item in probRelations)
           {
               if (item.RelationName != string.Empty)
               {
                   try
                   {
                       int value = Convert.ToInt32(item.RelationName);
                       MessageError = String.Format("Incorrect syntax near '{0}'.", item.RelationName);                
                       return null;

                   }
                   catch (Exception)
                   {
                   }
               }
           }
                               
           return probRelations;
       }
       private static string GetCondition(string valueString)
       {
            Console.WriteLine("GetCondition");
           string conditionString = string.Empty;
           int posOne;

           ///////////////////// Get Select Condition /////////////////
           if (valueString.Contains("where "))
           {
               posOne = valueString.IndexOf("where") + 5;
               conditionString = valueString.Substring(posOne);   // Get Select Condition in the Query Text
           }
           else
               conditionString = string.Empty;

           return conditionString;
       }
       private static bool checkCondition(string conditionString)
       {


           string tmpWhere = conditionString;
           for (int i = 0; i < tmpWhere.Length - 1; )
           {
               int j = i + 1;
               if (tmpWhere[i] == '\'')
               {

                   for (int k = j; k < tmpWhere.Length -1; k++)
                   {
                       if (tmpWhere[k] == '\'' && tmpWhere[k] == ')')
                       {
                           j = k + 1;
                           break;
                       }      

                   }
               }
               i = j;
           }


           return true;
       }
       private bool QueryAnalyze(String queryString)
       {
            Console.WriteLine("QueryAnalyze");
           try
           {  
               string S = queryString;
               //Kiểm tra câu truy vấn có hợp lệ
               if (!this.CheckStringQuery(S))
               {             
                   return false;
               }


               //Get All Relation
               this.selectedRelations = GetAllRelation(S);
               if (this.selectedRelations == null)
               {
                     return false;
               }
               

               //Get All Attribute
               this.selectedAttributes = GetAttribute(S);
               if (this.selectedAttributes == null)
                   return false;

               this.conditionString = GetCondition(S);

               return true;

           }
           catch (Exception )
           {
               return false;
           }
       }
         

       #endregion      
       
       private static ProbRelation getRelationBySelectAttribute( ProbRelation probRelation, List<ProbAttribute> attributes)
       {
            Console.WriteLine("getRelationBySelectAttribute");
           ProbRelation relation = new ProbRelation();
           relation.RelationName = probRelation.RelationName;            


           List<int> indexs = new List<int>();
            List<int> indexRemove = new List<int>();
           foreach (ProbAttribute attr in attributes)
           {
               for (int i = 0; i < probRelation.Scheme.Attributes.Count; i++)
               {
                   if (probRelation.Scheme.Attributes[i].AttributeName.Trim().ToLower() == attr.AttributeName.Trim().ToLower())
                   {
                       indexs.Add(i);
                       break;                       
                   }
               }
           }



           foreach (ProbTuple item in probRelation.tuples)
           {   
               ProbTuple tuple = new ProbTuple();
               for (int i = 0; i < indexs.Count; i++)
               {
                   tuple.Triples.Add(item.Triples[indexs[i]]);
               }
               relation.tuples.Add(tuple);
           }

               
           relation.Scheme.Attributes = attributes;
      
         return relation;      
       }   
        

        bool unionIntersectExcept(String queryString)
        {
            Console.WriteLine("unionIntersectExcept: " + queryString);
            String query = queryString;
            String subQuery1;
            String subQuery2;

            if (query.Contains("union"))
            {
                if (query.Contains("union all"))
                {
                    subQuery1 = subBefore(queryString, "union all");
                    subQuery2 = subAfter(queryString, "union all");

                    ProbRelation relationResult = new ProbRelation();

                    if (ExecuteQuery(subQuery1))
                    {
                        relationResult = this.relationResult;

                        if (ExecuteQuery(subQuery2))
                        {
                            foreach (ProbTuple tuple in this.relationResult.tuples)
                            {
                                relationResult.tuples.Add(tuple);
                            }
                            this.relationResult = relationResult;
                            return true;
                        } else
                        {
                            return false;
                        }
                    } else
                    {
                        return false;
                    }
                }

                if (query.Contains("union ⊕_ig"))
                {
                    subQuery1 = subBefore(queryString, "union ⊕_ig");
                    subQuery2 = subAfter(queryString, "union ⊕_ig");

                    ProbRelation relationResult = new ProbRelation();

                    if (ExecuteQuery(subQuery1))
                    {
                        relationResult = this.relationResult;

                        if (ExecuteQuery(subQuery2))
                        {
                            foreach (ProbTuple tuple in this.relationResult.tuples)
                            {
                                relationResult.tuples.Add(tuple);
                            }

                            CompareProbTuple compareProbTuple = new CompareProbTuple(relationResult);
                            this.relationResult = compareProbTuple.equal("⊕_ig");

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (query.Contains("union ⊕_in"))
                {
                    subQuery1 = subBefore(queryString, "union ⊕_in");
                    subQuery2 = subAfter(queryString, "union ⊕_in");

                    ProbRelation relationResult = new ProbRelation();

                    if (ExecuteQuery(subQuery1))
                    {
                        relationResult = this.relationResult;

                        if (ExecuteQuery(subQuery2))
                        {
                            foreach (ProbTuple tuple in this.relationResult.tuples)
                            {
                                relationResult.tuples.Add(tuple);
                            }

                            CompareProbTuple compareProbTuple = new CompareProbTuple(relationResult);
                            this.relationResult = compareProbTuple.equal("⊕_in");

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                if (query.Contains("union ⊕_me"))
                {
                    subQuery1 = subBefore(queryString, "union ⊕_me");
                    subQuery2 = subAfter(queryString, "union ⊕_me");

                    ProbRelation relationResult = new ProbRelation();

                    if (ExecuteQuery(subQuery1))
                    {
                        relationResult = this.relationResult;

                        if (ExecuteQuery(subQuery2))
                        {
                            foreach (ProbTuple tuple in this.relationResult.tuples)
                            {
                                relationResult.tuples.Add(tuple);
                            }

                            CompareProbTuple compareProbTuple = new CompareProbTuple(relationResult);
                            this.relationResult = compareProbTuple.equal("⊕_me");

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        String subBefore(String queryString, String code)
        {
            int index = queryString.IndexOf(code);
            return queryString.Substring(0, index).Trim();
        }

        String subAfter(String queryString, String code)
        {
            int index = queryString.IndexOf(code) + code.Length;
            return queryString.Substring(index).Trim();
        }

        internal bool ExecuteQuery(String queryText)
       {
            String queryString = StandardizeQuery(queryText);
            Console.WriteLine("ExecuteQuery: " + queryString);
            String query = queryString;
            if (query.Contains("union") || query.Contains("intersect") || query.Contains("except"))
            {
                return unionIntersectExcept(queryString);
            } else
            {
                try
                {
                    if (!QueryAnalyze(queryString)) return false;

                    if (this.selectedRelations.Count == 2)
                    {
                        if (flagNaturalJoin != true)
                            this.selectedRelations[0] = Descartes();
                        else
                            this.selectedRelations[0] = NaturalJoin();
                    }
                    else
                    {
                        foreach (ProbAttribute attr in this.selectedRelations[0].Scheme.Attributes)
                        {
                            if (!attr.AttributeName.Contains("."))
                                attr.AttributeName = String.Format("{0}.{1}", this.selectedRelations[0].RelationName, attr.AttributeName);
                        }

                    }


                    if (!queryString.Contains("where"))
                    {
                        this.relationResult = getRelationBySelectAttribute(this.selectedRelations[0], this.selectedAttributes);

                        Console.WriteLine("Compare and Return");

                        CompareProbTuple compareProbTuple = new CompareProbTuple(this.relationResult);

                        relationResult = compareProbTuple.equal("⊕_in");

                        return true;
                    }
                    else
                    {
                        // Đang làm dỡ phần này
                        SelectCondition Condition = new SelectCondition(this.selectedRelations[0], this.conditionString);
                        if (!Condition.CheckConditionString())
                        {
                            this.MessageError = Condition.MessageError;
                            return false;
                        }

                        foreach (ProbTuple tuple in this.selectedRelations[0].tuples)
                        {
                            if (Condition.Satisfied(tuple))
                                this.relationResult.tuples.Add(tuple);
                        }

                        if (Condition.MessageError != string.Empty)
                        {
                            this.MessageError = Condition.MessageError;
                            return false;
                        }

                        if (Condition.conditionString == string.Empty)
                        {
                            this.MessageError = Condition.MessageError;
                            return false;
                        }

                        this.relationResult.Scheme = this.selectedRelations[0].Scheme;
                        this.relationResult = getRelationBySelectAttribute(this.relationResult, this.selectedAttributes);
                    }
                }
                catch
                {
                    return false;
                }
                return true;
            }
       }
   }
}
