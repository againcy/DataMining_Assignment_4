using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining_Assignment_4
{
    class Program
    {
        public static Vector HingeLoss(double innerProduction, Vector x, int label)
        {
            if (label * innerProduction < 1) return -1 * label * x;
            else return new Vector(x.value.Count());
        }
        public static Vector LogLoss(double innerProduction, Vector x, int label)
        {
            double k = -1 * ((double)label / (1 + Math.Exp(label * innerProduction)));
            return k * x;

        }

        static void PrintLog(string str)
        {
            Console.WriteLine(System.DateTime.Now.ToLongTimeString() + " " + str);
        }
        static void Test_hingeloss()
        {
            //dataset 1
            PrintLog("dataset 1 Hingeloss...");
            PegesosClassifier pegesos1 = new PegesosClassifier();
            pegesos1.ReadTraningData(@"dataset1-a8a-training.txt");
            pegesos1.GeneratePegesos(0.0001, pegesos1.TrainingSet.Count * 5, HingeLoss);
            ClassifierTest test1 = new ClassifierTest();
            test1.ReadTestData(@"dataset1-a8a-testing.txt");
            test1.ApplyClassifier(pegesos1, @"dataset1-a8a_HingeLoss_result.txt");
            PrintLog("dataset 1 Hingeloss Over...");

            //dataset 2
            PrintLog("dataset 2 Hingeloss...");
            PegesosClassifier pegesos2 = new PegesosClassifier();
            pegesos2.ReadTraningData(@"dataset1-a9a-training.txt");
            pegesos2.GeneratePegesos(0.00005, pegesos2.TrainingSet.Count * 5, HingeLoss);
            ClassifierTest test2 = new ClassifierTest();
            test2.ReadTestData(@"dataset1-a9a-testing.txt");
            test2.ApplyClassifier(pegesos2, @"dataset1-a9a_HingeLoss_result.txt");
            PrintLog("dataset 2 Hingeloss Over...");
            
        }

        static void Test_logloss()
        {
            //dataset 1
            PrintLog("dataset 1 LogLoss...");
            PegesosClassifier pegesos1 = new PegesosClassifier();
            pegesos1.ReadTraningData(@"dataset1-a8a-training.txt");
            pegesos1.GeneratePegesos(0.0001, pegesos1.TrainingSet.Count * 5, LogLoss);
            ClassifierTest test1 = new ClassifierTest();
            test1.ReadTestData(@"dataset1-a8a-testing.txt");
            test1.ApplyClassifier(pegesos1, @"dataset1-a8a_LogLoss_result.txt");
            PrintLog("dataset 1 LogLoss Over...");

            //dataset 2
            PrintLog("dataset 2 LogLoss...");
            PegesosClassifier pegesos2 = new PegesosClassifier();
            pegesos2.ReadTraningData(@"dataset1-a9a-training.txt");
            pegesos2.GeneratePegesos(0.00005, pegesos2.TrainingSet.Count * 5, LogLoss);
            ClassifierTest test2 = new ClassifierTest();
            test2.ReadTestData(@"dataset1-a9a-testing.txt");
            test2.ApplyClassifier(pegesos2, @"dataset1-a9a_LogLoss_result.txt");
            PrintLog("dataset 2 LogLoss Over...");
        }
        static void Main(string[] args)
        {
            Test_hingeloss();
            Test_logloss();
            Console.WriteLine("结束...");
            Console.ReadLine();
        }
    }
}
