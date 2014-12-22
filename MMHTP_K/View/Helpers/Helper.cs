using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MMHTP_K.Model;
using Newtonsoft.Json;

namespace MMHTP_K.View.Helpers
{
    public class Helper
    {
        private static readonly WinApi.RECT CalculatedRect = new WinApi.RECT {Left = 299,Bottom = 619,Right = 1068,Top = 100};                                           //For coordinates correction
        public static async Task ShowFileMenu()
        {
            var points = await GetFileMenuHelper();
            var hwnd = WinApi.FindWindow(null, "Mathematical modeling");
            var rct = new WinApi.RECT();
            WinApi.GetWindowRect(hwnd, ref rct);
            var leftDisposition = CalculatedRect.Left - rct.Left;
            var topDisposition = CalculatedRect.Top - rct.Top;
            foreach (var point in points)
            {
                var currPoint = point;
                await Task.Run(() => WinApi.SetCursorPos(leftDisposition+currPoint.X, topDisposition+currPoint.Y));
                Thread.Sleep((int)currPoint.Delay);
            }
            var lastPoint = points.Last();
            WinApi.ClickLeftMouseButton(lastPoint.X, lastPoint.Y);
        }

        public static async Task ShowOpenFileMenu()
        {
            var points = await GetOpenFileHelper();
            var hwnd = WinApi.FindWindow(null, "Mathematical modeling");
            var rct = new WinApi.RECT();
            WinApi.GetWindowRect(hwnd, ref rct);
            var leftDisposition = CalculatedRect.Left - rct.Left;
            var topDisposition = CalculatedRect.Top - rct.Top;
            foreach (var point in points)
            {
                var currPoint = point;
                await Task.Run(() => WinApi.SetCursorPos(leftDisposition + currPoint.X, topDisposition + currPoint.Y));
                Thread.Sleep((int)currPoint.Delay);
            }
        }


        public static async Task ShowInputFlyout()
        {
            var points = await GetFlyoutHelper();
            var hwnd = WinApi.FindWindow(null, "Mathematical modeling");
            var rct = new WinApi.RECT();
            WinApi.GetWindowRect(hwnd, ref rct);
            var leftDisposition = CalculatedRect.Left - rct.Left;
            var topDisposition = CalculatedRect.Top - rct.Top;
            foreach (var point in points)
            {
                var currPoint = point;
                await Task.Run(() => WinApi.SetCursorPos(leftDisposition + currPoint.X, topDisposition + currPoint.Y));
                Thread.Sleep((int)currPoint.Delay);
            }
            var lastPoint = points.Last();
            WinApi.ClickRightMouseButton(lastPoint.X, lastPoint.Y);
        }

        public static async Task<ObservableCollection<MouseMovement>> GetFileMenuHelper()
        {
            try
            {
                string json; // Строка JSON
                using (var srd = new StreamReader(new FileStream("fileHelp.txt", // Вывод в файл
                    FileMode.Open, FileAccess.Read)))
                {
                    json = await srd.ReadToEndAsync(); // Считываем весь файл в строку
                    srd.Close(); // Закрываем поток считывания
                }
                return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<MouseMovement>>(json));
                    // Возвращаем строку с данными

            }
            catch (Exception)
            {
                MessageBox.Show("Файл помощи отсутствует");
            }
            return null;
        }

        public static async Task<ObservableCollection<MouseMovement>> GetOpenFileHelper()
        {
            try
            {
                string json; // Строка JSON
                using (var srd = new StreamReader(new FileStream("openHelp.txt", // Вывод в файл
                    FileMode.Open, FileAccess.Read)))
                {
                    json = await srd.ReadToEndAsync(); // Считываем весь файл в строку
                    srd.Close(); // Закрываем поток считывания
                }
                return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<MouseMovement>>(json));
                // Возвращаем строку с данными

            }
            catch (Exception)
            {
                MessageBox.Show("Файл помощи отсутствует");
            }
            return null;
        }

        public static async Task<ObservableCollection<MouseMovement>> GetFlyoutHelper()
        {
            try
            {
                string json; // Строка JSON
                using (var srd = new StreamReader(new FileStream("flyoutHelp.txt", // Вывод в файл
                    FileMode.Open, FileAccess.Read)))
                {
                    json = await srd.ReadToEndAsync(); // Считываем весь файл в строку
                    srd.Close(); // Закрываем поток считывания
                }
                return await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<MouseMovement>>(json));
                // Возвращаем строку с данными

            }
            catch (Exception)
            {
                MessageBox.Show("Файл помощи отсутствует");
            }
            return null;
        }
    }
}
