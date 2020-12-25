using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day25
{
    public class Day25Part1
    {
        private List<long> input = new List<long>();

        /*
         * Set the value to itself multiplied by the subject number.
         * Set the value to the remainder after dividing the value by 20201227.
         * 
         * Transforming the subject number of 17807724 (the door's public key) 
         * with a loop size of 8 (the card's loop size) produces the encryption key, 14897079.
         * 
         * Transforming the subject number of 5764801 (the card's public key) 
         * with a loop size of 11 (the door's loop size) produces the same encryption key: 14897079.
         */
        private void Day25()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            long cardKey = input[0], 
                 doorKey = input[1];
            long cardLoopSize = GetLoopsize(cardKey), 
                 doorLoopSize = GetLoopsize(doorKey);
            long cardEncryptionKey = GetEncryptionKey(cardKey, doorLoopSize), 
                 doorEncryptionKey = GetEncryptionKey(doorKey, cardLoopSize);

            long ans = 0;
            if (cardEncryptionKey == doorEncryptionKey)
            {
                ans = cardEncryptionKey;
            }

            watch.Stop();
            Console.WriteLine($"Answer: {ans} took {watch.ElapsedMilliseconds} ms");
        }

        private long GetLoopsize(long key)
        {
            long value = 1, loopSize = 0;
            while (value != key)
            {
                value = Calculation(value, 7);
                loopSize++;
            }
            return loopSize;
        }

        private long GetEncryptionKey(long key, long loopSize)
        {
            long value = 1;
            for (int i = 0; i < loopSize; i++)
            {
                value = Calculation(value, key);
            }
            return value;
        }

        private long Calculation(long value, long seed)
        {
            value *= seed;
            value %= 20201227;
            return value;
        }

        private void ReadData()
        {
            string path = @"C:\Users\andre\Desktop\AdventOfCode2020\Day 25\input.txt";
            input = File.ReadAllLines(path).Select(long.Parse).ToList();
        }

        public void TestCase()
        {
            ReadData();
            Day25();
        }
    }
}
