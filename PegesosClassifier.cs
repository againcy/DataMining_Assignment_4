using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataMining_Assignment_4
{
    class PegesosClassifier
    {
        /// <summary>
        /// 训练集
        /// </summary>
        public List<BinaryClassData> TrainingSet
        {
            get
            {
                return trainingSet;
            }
        }
        private List<BinaryClassData> trainingSet;
       
        private BinaryClassData[] arrTrainingSet;//数组表示的训练集，方便取随机
        private int cntFeatures;//特征数
        private UniRandomGenerator random;
        
        private Vector W;
        /// <summary>
        /// 存储t=0.1T,0.2T,...,1T的所有W值
        /// </summary>
        public Vector[] listW;

        public delegate Vector SubgradiantFunction(double z, Vector x, int label);

        public PegesosClassifier()
        {
            trainingSet = new List<BinaryClassData>();
        }

        /// <summary>
        /// 读入训练集数据
        /// </summary>
        /// <param name="filepath">训练集路径</param>
        public void ReadTraningData(string filepath)
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
                cntFeatures = features.value.Length;
                trainingSet.Add(new BinaryClassData(features, (int)data[data.Length - 1]));
            }
            sr.Close();
        }

        /// <summary>
        /// Pegesos算法执行器
        /// </summary>
        /// <param name="lambda"></param>
        /// <param name="T">迭代次数</param>
        /// <param name="Subgradiant">次梯度函数</param>
        public void GeneratePegesos(double lambda, int T, SubgradiantFunction Subgradiant)
        {
            //随机数相关初始化
            arrTrainingSet = trainingSet.ToArray();
            random = new UniRandomGenerator(trainingSet.Count);

            listW = new Vector[10];
            //初始化w=0向量
            W = new Vector(cntFeatures);

            //开始迭代
            int cntT = 0;
            for (int t = 1; t <= T; t++)
            {
                double eta = 1 / (double)(lambda * t);
                int randNum = random.GetNext();
                Vector X = arrTrainingSet[randNum].Features;
                Vector v = Subgradiant(W * X, X, arrTrainingSet[randNum].TrueLabel);
                W = (1 - eta * lambda) * W - eta * v;
                //记录W的中间值
                if (t>=((double)(cntT+1)/(double)10)*T)
                {
                    listW[cntT] = new Vector(W);
                    cntT++;
                }
            }
        }

        /// <summary>
        /// 应用分类器
        /// </summary>
        /// <param name="test">测试数据</param>
        /// <param name="w"></param>
        /// <returns>1或-1</returns>
        public int ApplyClassifier(BinaryClassData test, Vector w)
        {
            if (w * test.Features >= 0) return 1;
            else return -1;
        }

    }
}
