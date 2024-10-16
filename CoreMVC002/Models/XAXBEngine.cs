using System;

namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        public string Secret { get; set; }
        public string Guess { get; set; }
        public string Result { get; set; }
        public string Store {  get; set; }
        public int GuessCount {  get; set; }
        public XAXBEngine()
        {
            // TODO 0 - randomly 
            string randomSecret = "1234";
            Secret = randomSecret;
            Guess = null;
            Result = null;
        }
        private string GenerateRandomNumberString()
        {
            Random random = new Random();

            // 使用 HashSet 來確保生成的數字不重複
            HashSet<int> digits = new HashSet<int>();

            // 生成 4 個不重複的隨機數字
            while (digits.Count < 4)
            {
                int digit = random.Next(0, 10); // 在 0 到 9 之間生成一個隨機數字
                digits.Add(digit); // 將生成的數字添加到 HashSet 中，如果重複則不會添加
            }

            // 將 HashSet 中的數字轉換為字符串並返回
            return string.Join("", digits);
        }

        public XAXBEngine(string secretNumber)
        {
            Secret = secretNumber;
            Guess = null;
            Result = null;
        }
        //
        public int numOfA(string guessNumber)
        {
            int Acount = 0;
            for (int i=0;i<4;i++) 
            {
                if (Secret[i] == guessNumber[i])
                {
                    Acount++;
                }
            }
            return Acount;
        }
        //  
        public int numOfB(string guessNumber)
        {
            int Bcount = 0;
            for (int i=0;i<4;i++) 
            {
                if (Secret[i] != guessNumber[i] && Secret.Contains(guessNumber[i]))
                {
                    Bcount++;
                } 
            }
            return Bcount;
        }
        //
        public bool IsGameOver(string guessNumber)
        {
            // TODO 3
            return false;
        }

    }

}
