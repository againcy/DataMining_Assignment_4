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

        /// <summary>
        /// 读入测试数据
        /// </summary>
        /// <param name="filepath">测试数据文件路径</param>
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

        /// <summary>
        /// 应用分类器
        /// </summary>
        /// <param name="Classifier">分类器</param>
        /// <param name="filepath">结果文件路径</param>
        public void ApplyClassifier(PegesosClassifier Classifier, string filepath)
        {
            StreamWriter sw = new StreamWriter(filepath);
            for (int t = 0; t < 10; t++)
            {
                Vector w = Classifier.listW[t];
                foreach (var data in testSet)
                {
                    
                    int declaredLabel = Classifier.ApplyClassifier(data, w);
                    data.DeclaredLabel = declaredLabel;
                }
                double testError = GetTestError();
                sw.WriteLine(((double)(t + 1) / (double)10).ToString("0.0") + "T : " + testError.ToString("0.000"));
            }
            sw.Close();
        }

        /// <summary>
        /// 获取分类误差
        /// </summary>
        /// <returns></returns>
        private double GetTestError()
        {
            int cntError = 0;
            foreach (var data in testSet)
            {
                if (data.TrueLabel != data.DeclaredLabel) cntError++;
            }
            return (double)cntError / (double)testSet.Count;
        }
    }
}
