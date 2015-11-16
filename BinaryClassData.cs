using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining_Assignment_4
{
    class BinaryClassData
    {
        /// <summary>
        /// 数据的特征
        /// </summary>
        public Vector Features
        {
            get
            {
                return features;
            }
        }
        private Vector features;

        /// <summary>
        /// 数据真实类标（-1或1）
        /// </summary>
        public int TrueLabel
        {
            get
            {
                return trueLabel;
            }
        }
        private int trueLabel;

        /// <summary>
        /// 数据经过分类器后得出的类标（-1或1）
        /// </summary>
        public int DeclaredLabel
        {
            get
            {
                return declaredLabel;
            }
            set
            {
                declaredLabel = value;
            }
        }
        private int declaredLabel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_features">特征</param>
        /// <param name="_trueLabel">类标</param>
        public BinaryClassData(Vector _features, int _trueLabel)
        {
            features = _features;
            trueLabel = _trueLabel;
        }
    }
}
