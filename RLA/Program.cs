namespace RLA
{
    internal class Program
    {
        static Random Rnd = new Random();
        static List<(int X, int Y)> neighbors = new List<(int, int)>()
        {
            (-1,-1),
            (0, -1),
            (1, -1),
            (-1, 0),
            (1, 0),
            (-1, 1),
            (0, 1),
            (1, 1),
        };
        static void Main(string[] args)
        {
                Console.WriteLine("Вам представляется уникальная возможность \n" +
                    " понаблюдать... Введите размеры поля (Д*Ш).");
                int length = int.Parse(Console.ReadLine());
                int wide = int.Parse(Console.ReadLine());
                // Создать начальную конфигурацию
                int[,] field = new int[length, wide];
                Console.WriteLine("Теперь выбираем центр кластеризации (х, у)");
                CenterCluster(field);
                Console.WriteLine("Давайте выберем пористость");
                int numberZero = int.Parse(Console.ReadLine());
                 Console.WriteLine("Вероятность агрегации частиц");
                 int probabilityAggregation = int.Parse(Console.ReadLine());

            int hole = 0;
                // Если не достигли пористости
                //Активность на поле
                // Если на поле есть клетка с состоянием 2, то
                // Ищем 2
                // Проверяем соседей двойки
                // Если в соседях есть 1, то 2 переходит в 1
                // В противном случае 2 меняется состоянием со случайном соседней клеткой
                // В противном случае
                // Создаем двойку в случайной клетке 0
                (int i, int j) globulaCoords = (0, 0);
                bool isGlobulaOnField = false;
                do
                {
                    if (isGlobulaOnField == true)
                    {
                        if (IsCenterInNeighborCells(field, globulaCoords.i, globulaCoords.j) == true
                            && Rnd.Next(0, 101) <= probabilityAggregation)
                        {
                            field[globulaCoords.i, globulaCoords.j] = 1;
                            isGlobulaOnField = false;
                        }
                        else
                        {
                            globulaCoords = Move(field, globulaCoords.i, globulaCoords.j);
                        }
                    }
                    else
                    {
                        globulaCoords = GlobulaGenerate(field, length, wide);
                        isGlobulaOnField = true;
                    }
                    hole = HoleCount(field, length, wide);
                    Draw(field);
                }
                while (hole != numberZero);
                Console.WriteLine("Пористость достигнута!");
            }
            static void CenterCluster(int[,] field)
            {
                int clusterX = int.Parse(Console.ReadLine());
                int clusterY = int.Parse(Console.ReadLine());
                field[clusterX, clusterY] = 1;
            }
            // Correct
            static int HoleCount(int[,] field, int length, int wide)
            {
                int hole = length * wide;
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < wide; j++)
                    {
                        if (field[i, j] == 1) hole--;
                    }
                }
                return hole;
            }
            static void HoleCheck(int[,] field, int length, int wide, int numberZero)
            {
                int hole = 0;
                do
                {
                    hole = HoleCount(field, length, wide);
                }
                while (hole != numberZero);
            }
            static (int i, int j) GlobulaGenerate(int[,] field, int length, int wide)
            {
                int x = 0;
                int y = 0;
                do
                {
                    x = Rnd.Next(length);
                    y = Rnd.Next(wide);
                }
                while (field[x, y] == 1);
                field[x, y] = 2;
                return (x, y);
            }


            static void Draw(int[,] field)
            {
                Thread.Sleep(2500);
                Console.Clear();

                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        Console.Write(field[i, j]);
                    }
                    Console.WriteLine();
                }

            }
            static (int, int) Move(int[,] field, int i, int j)
            {

                int randNumber = Rnd.Next(8);
                field[i, j] = 0;

                switch (randNumber)
                {
                    case (0):
                        if (i - 1 >= 0)
                        {
                            i -= 1;
                        }
                        break;
                    case (1):
                        if (i + 1 < field.GetLength(0))
                        {
                            i += 1;
                        }
                        break;
                    case (2):
                        if (j + 1 < field.GetLength(1))
                        {
                            j += 1;
                        }

                        break;
                    case (3):
                        if (j - 1 >= 0)
                        {
                            j -= 1;
                        }
                        break;
                    case (4):
                        if (i + 1 < field.GetLength(0) && j + 1 < field.GetLength(1))
                        {
                            i += 1;
                            j += 1;
                        }

                        break;
                    case (5):
                        if (i + 1 < field.GetLength(0) && j - 1 >= 0)
                        {
                            i += 1;
                            j -= 1;
                        }

                        break;
                    case (6):
                        if (i - 1 >= 0 && j + 1 < field.GetLength(1))
                        {
                            i -= 1;
                            j += 1;
                        }

                        break;
                    case (7):
                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            i -= 1;
                            j -= 1;
                        }

                        break;
                }
                field[i, j] = 2;
                return (i, j);

            }
            static bool IsCenterInNeighborCells(int[,] field, int i, int j)
            {

                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;
                        if (0 <= i + dx && i + dx < field.GetLength(0) &&
                            0 <= j + dy && j + dy < field.GetLength(1))
                        {
                            if (field[i + dx, j + dy] == 1)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
    }
}
