using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataMining_Assignment_4
{
    class ClassifierTest
    {
        /// <summary>
        /// 测试集
        /// </summary>
        public List<BinaryClassData> TestSet
        {
            get
            {
                return testSet;
            }
        }
        private List<BinaryClassData> testSet; 

        public ClassifierTest()
        {
            testSet = new List<BinaryClassData>();
        }

        public void ReadTestData(string filepath)
        {
            StreamReader sr = new StreamReader(filepath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                /*
                 * 数据格式
                 * a1,a2,a3,...an,label
                 * 表示一组数据 x=(a1,a2,a3...an) 类标label
                 */
                int[] data = line.Split(',').Select<string, int>(x => Convert.ToInt32(x)).ToArray();
                Vector features = new Vector(data.Take(data.Length - 1).ToArray<int>());
                testSet.Add(new BinaryClassData(features, (int)data[data.Length - 1]));
            }
            sr.Close();
        }

        public void ApplyClassifier(PegesosClassifier Classifier, string filepath)
        {
            StreamWriter sw = new StreamWriter(filepath);
            ClassificationValidator validator = new ClassificationValidator();
            for (int t = 0; t < 10; t++)
            {
                Vector w = Classifier.listW[t];
                foreach (var data in testSet)
                {
                    
                    int declaredLabel = Classifier.ApplyClassifier(data, w);
                    data.DeclaredLabel = declaredLabel;
                }
                double testError = validator.GetTestError(testSet);
                sw.WriteLine("t=" + ((double)(t + 1) / (double)10).ToString("0.0") + "T : " + testError.ToString("0.000"));
            }
            sw.Close();
        }
    }
}
