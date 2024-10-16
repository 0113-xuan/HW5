using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC002.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC002.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // 初始化秘密數字
            string secretNumber = GenerateSecretNumber();

            // 傳遞到 View 內部後再回到 Controller
            TempData["secretNumber"] = secretNumber;

            // 初始化猜測記錄（如果不存在）
            if (TempData["GuessHistory"] == null)
            {
                TempData["GuessHistory"] = new List<string>();
            }

            //初始化猜測次數
            if (TempData["GuessCount"] == null)
            {
                TempData["GuessCount"] = 0;
            }

            // 創建猜測模型: 猜測數字+比對結果+比對邏輯
            var model = new XAXBEngine(secretNumber);

            // 使用強型別
            return View(model);
        }

        [HttpPost]
        public ActionResult Guess(XAXBEngine model)
        {
            string secretNumber = TempData["secretNumber"] as string;
            model.Secret = secretNumber;
            // 檢查猜測結果
            //model.Result = GetGuessResult(model.Guess);
            int Acount = model.numOfA(model.Guess); 
            int Bcount = model.numOfB(model.Guess);
            model.Result = $"{Acount}A{Bcount}B";
            
            //紀錄每次猜測次數和結果
            string guessHistory = TempData["GuessHistory"] as string ?? "";
            string guessRecord = $"{model.Guess}:{model.Result}";
            guessHistory += guessRecord + "\n";
            TempData["GuessHistory"] = guessHistory;

            //讀猜測次數
            int Count = TempData["GuessCount"] != null ? (int)TempData["GuessCount"] : 0;

            //增加猜測次數
            Count++;
            TempData["GuessCount"] = Count;

            //更新模型中的GuessCount
            model.GuessCount = Count;

            //將猜測記錄加入模型中
            model.Store = guessHistory;
            TempData.Keep("secretNumber");
            return View("Index", model);
        }

        // ------ 遊戲相關之邏輯 ------
        private string GenerateSecretNumber()
        {
            // 生成一個隨機的4位數字作為秘密數字
            // 你可以根據需要自定義生成規則
            return "1234";
        }
        [HttpPost]
        public ActionResult Restart()
        {
            // 清空 TempData 的數據，重新初始化遊戲
            TempData.Remove("secretNumber");
            TempData.Remove("GuessCount");
            TempData.Remove("GuessHistory");

            // 重定向回 Index，重新開始遊戲
            return RedirectToAction("Index");
        }

        private string GetGuessResult(string guess)
        {
            // 檢查猜測結果，並返回結果字符串
            string secretNumber = TempData["secretNumber"] as string;
            // 利用Keep(...) 方法, or 再次回存！
            // TempData["SecretNumber"] = secretNumber;
            TempData.Keep("SecretNumber");

            // 你可以根據遊戲規則自定義檢查邏輯
            if (secretNumber.Equals(guess))
                return "4A0B";
            else
                return "?A?B";
        }
    }
}

