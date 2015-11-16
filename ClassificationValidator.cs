using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining_Assignment_4
{
    class ClassificationValidator
    {
        public ClassificationValidator()
        {
        }

        /// <summary>
        /// 计算分类误差
        /// </summary>
        /// <param name="testSet">测试集</param>
        /// <returns>误差</returns>
        public double GetTestError(List<BinaryClassData> testSet)
        {
            int cntError=0;
            foreach (var data in testSet)
            {
                if (data.TrueLabel != data.DeclaredLabel) cntError++;
            }
            return (double)cntError / (double)testSet.Count;
        }
    }
}
