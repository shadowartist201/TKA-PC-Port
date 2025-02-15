﻿using System;
using System.Configuration;
using System.IO;

namespace Helicopter
{
    public static class Storage
    {
        public static bool vibrationOn_ = false;

        public static bool fullScreenOn_ = false;

        public static int resValue_ = 1;

        public static int musicValue_ = 7;

        public static int FXValue_ = 7;

        private static StreamReader reader;

        private static StreamWriter writer;

        private static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static void LoadOptionInfo()
        {
            string fullPath = Path.Combine(filePath, "TKA/savedata.txt");
            if (!File.Exists(fullPath))
            {
                if (!Directory.Exists(Path.Combine(filePath, "TKA")))
                {
                    Directory.CreateDirectory(Path.Combine(filePath, "TKA"));
                }
                File.Create(fullPath);
            }
            reader = new StreamReader(fullPath);
            if (reader.Peek() == 'O')
            {
                reader.ReadLine(); //Read Options
                try
                {
                    vibrationOn_ = Convert.ToBoolean(Convert.ToInt32(reader.ReadLine()));
                    fullScreenOn_ = Convert.ToBoolean(Convert.ToInt32(reader.ReadLine()));
                    resValue_ = Convert.ToInt32(reader.ReadLine());
                    musicValue_ = Convert.ToInt32(reader.ReadLine());
                    FXValue_ = Convert.ToInt32(reader.ReadLine());
                }
                catch (FormatException f)
                {
                    Console.WriteLine("Failed to process options data:");
                    Console.WriteLine(f.Message);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Failed to read options data:");
                    Console.WriteLine(e.Message);
                }
            }
            //reader.Close();
        }

        public static void SaveOptionInfo(int[] settings)
        {
            writer = new StreamWriter(Path.Combine(filePath, "TKA/savedata.txt"));
            try
            {
                writer.WriteLine("Options");
                for (int i = 0; i < settings.Length; i++)
                {
                    writer.WriteLine(settings[i]);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to write options data:");
                Console.WriteLine(e.Message);
            }
            //writer.Close();
        }

        public static ScoreInfo LoadScoreInfo()
        {
            ScoreInfo result = new ScoreInfo(0);
            string fullPath = Path.Combine(filePath, "TKA/savedata.txt");
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
            }
            //reader = new StreamReader(fullPath);
            if (reader.Peek() == 'S')
            {
                reader.ReadLine(); //Read Score
                try
                {
                    result.stageIndexes_ = ReadArray();
                    result.catIndexes_ = ReadArray();
                    result.scores_ = ReadArray();
                    result.highScore_ = Convert.ToInt32(reader.ReadLine());
                    result.seaHigh_ = Convert.ToInt32(reader.ReadLine());
                    result.cloudHigh_ = Convert.ToInt32(reader.ReadLine());
                    result.lavaHigh_ = Convert.ToInt32(reader.ReadLine());
                    result.meatHigh_ = Convert.ToInt32(reader.ReadLine());
                    result.ronHigh_ = Convert.ToInt32(reader.ReadLine());
                    result.nyanHigh_ = Convert.ToInt32(reader.ReadLine());
                }
                catch (FormatException f)
                {
                    Console.WriteLine("Failed to process score data:");
                    Console.WriteLine(f.Message);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Failed to read score data:");
                    Console.WriteLine(e.Message);
                }
            }
            reader.Close();
            return result;
        }

        public static void SaveScoreInfo(ScoreInfo input)
        {
            //writer = new StreamWriter(Path.Combine(filePath, "TKA/savedata.txt"));
            try
            {
                writer.WriteLine("Score");
                writer.WriteLine(StoreArray(input.stageIndexes_));
                writer.WriteLine(StoreArray(input.catIndexes_));
                writer.WriteLine(StoreArray(input.scores_));
                writer.WriteLine(input.highScore_);
                writer.WriteLine(input.seaHigh_);
                writer.WriteLine(input.cloudHigh_);
                writer.WriteLine(input.lavaHigh_);
                writer.WriteLine(input.meatHigh_);
                writer.WriteLine(input.ronHigh_);
                writer.WriteLine(input.nyanHigh_);
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to write score data:");
                Console.WriteLine(e.Message);
            }
            writer.Close();
        }

        private static int[] ReadArray()
        {
            int[] inputArray = new int[10];
            try
            {
                string test = reader.ReadLine();
                string[] nums = test.Split(' ');
                for (int i = 0; i < 10; i++)
                {
                    if (i < nums.Length)
                    {
                        inputArray[i] = Convert.ToInt32(nums[i]);
                    }
                    else
                    {
                        inputArray[i] = 0;
                    }
                }
                return inputArray;
            }
            catch (FormatException f)
            {
                Console.WriteLine("Failed to process score data:");
                Console.WriteLine(f.Message);
                return null;
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to read score data:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private static string StoreArray(int[] inputArray)
        {
            string output = "";
            for (int i = 0; i < inputArray.Length; i++)
            {
                output += Convert.ToString(inputArray[i]);
                output += " ";
            }
            return output;
        }
    }
}
