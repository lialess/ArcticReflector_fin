using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcticReflector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Тестовые данные
            //ясная погода, интенсивность солнечного излучения 77 кВт * ч/м2 за месяц, в ср 114.6 Вт * ч/м2 в час, Москва, февраль
            //размер фотоэлемента и активной части зеркал 0.0095*0.0095 м, панель расположена под 35 градусов к горизонату, 4 отражателя
            Console.Write("Введите размер солнечной панели (в квадратных метрах): ");
            double temp;
            double sizeM2 = double.Parse(Console.ReadLine());
            double[] mirrorCorner = { 45, 15, 165, 135 }; //угол попадания перпендикулярных к солнечной панели солнечных лучей относительно перпендикуляра к зеркалам
            Console.Write("Введите часовую инсоляцию (в Вт*ч/м2): ");
            double ins = double.Parse(Console.ReadLine());
            double resInsol = 0;
            Console.WriteLine($"\nСуммарное часовое солнечное излучение, попадающее на солнечную панель при отражении: ");
            for (int j = 10; j <= 90; j += 10)
            {
                for (int i = 0; i < mirrorCorner.Length; i++)
                {
                    if (mirrorCorner[i] < j)
                    {
                        temp = 90 + mirrorCorner[i] * 2 - j;
                        resInsol += ins * Math.Cos((temp <= 90) ? temp * 0.0174533 : (temp - 90) * 0.0174533) * sizeM2;
                    }
                    else if (mirrorCorner[i] > 90)
                    {
                        if (j < mirrorCorner[i] - 90)
                        {
                            resInsol += ins * Math.Sin(j * 0.0174533) * sizeM2;
                        }
                        else
                        {
                            temp = 2 * j + (180 - mirrorCorner[i]) - 90;
                            resInsol += ins * Math.Cos((temp <= 90) ? temp * 0.0174533 : (temp - 90) * 0.0174533) * sizeM2;
                        }
                    }
                }
                Console.WriteLine($"Угол {j} градусов к солнечной панели: {resInsol * 0.85:f3} Вт*ч");
                resInsol = 0;
            }
            Console.WriteLine($"\nСуммарное среднее часовое солнечное излучение, попадающее на солнечную панель со схожими с отражателями параметрами: ");
            for (int j = 10; j <= 90; j += 10)
            {
                for (int i = 0; i < mirrorCorner.Length; i++)
                {
                    if (mirrorCorner[i] < j)
                    {
                        resInsol += ins * Math.Cos(90 - (j - mirrorCorner[i]) * 0.0174533) * sizeM2;
                    }
                    else if (mirrorCorner[i] > 90)
                    {
                        if (j < mirrorCorner[i] - 90)
                        {
                            resInsol += ins * Math.Sin((j + (180 - mirrorCorner[i])) * 0.0174533) * sizeM2;
                        }
                        else
                        {
                            resInsol += ins * Math.Cos((180 - j - (180 - mirrorCorner[i])) * 0.0174533) * sizeM2;
                        }
                    }
                }
                Console.WriteLine($"Угол {j} градусов к солнечной панели: {resInsol * 0.85:f3} Вт*ч");
                resInsol = 0;
            }
            Console.ReadKey();
        }
    }
}
