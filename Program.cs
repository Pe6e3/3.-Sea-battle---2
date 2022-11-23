Console.Title = ("Морской бой");
int[,] field = new int[11, 11];
int countReps = 0;





void newShip(int shipLength)
{
    while (true)
    {
        Console.WriteLine($"Попыток создать корабль: {++countReps}");

        int checkMast = 0;
        bool checkShip = true;

        int directionX;
        int directionY;
        int[] shipX = new int[shipLength+1];
        int[] shipY = new int[shipLength+1];

        Random rnd = new Random();
        shipX[1] = rnd.Next(1,11);
        shipY[1] = rnd.Next(1,11);
        directionX = rnd.Next(1, 3) * 2 - 3;    // выбираем в каких направлениях будет разворачиваться корабль 
        directionY = rnd.Next(1, 3) * 2 - 3;
        int temp = rnd.Next(2);
        if (temp == 0) directionX = 0;   // выбираем в каком направлении Х или У корабль разворачиваться Не будет ( чтобы не разворачивался наискосок)
        if (temp == 1) directionY = 0;

        for (int i = 2; i < shipLength+1; i++)
        {
            shipX[i] = shipX[i - 1] + directionX;  // виртуально пристраиваем мачту к кораблю втупую. Потом проверим - можно ее там ставить было ии нет. Если можно удет - построим корабль в поле field
            shipY[i] = shipY[i - 1] + directionY;

            if (shipX[i] < 1 || shipX[i] > 10 || shipY[i] < 1 || shipY[i] > 10) // если координаты получись отрицательные или больше 10 - обнуляет их, а потом с помощью переменной checkMast и оператора if пропускает итерацию цикла, чтобы начать строить корабль заново.
            {
                checkMast = 1;

                checkShip = false;
                shipX[i] = 0;
                shipY[i] = 0;
                break;
            }
            if (checkMast == 1) continue;
                    }

        if (checkShip)
        {
            for (int i = 1; i < shipLength + 1; i++) // перебирает все координаты нового корабля и проверяет, нет ли на этом месте или в пределах одной клетки от каждой мачты другого корабля.
            {
                int x = shipX[i]; //это просто чтобы визуально разгрузить формулы ниже
                int y = shipY[i];
                if (x < 1 || x > 10 || y < 1 || y > 10) // проверка на выход за границы поля.
                {
                    checkMast = 1;
                    shipX[i] = 0;
                    shipY[i] = 0;
                    break;
                }
                if (checkMast == 1) continue;


                checkShip = false; // сразу указываем, что корабль не ставим и запускаем кучу проверок. Если хоть одна не пройдет - цикл оборвется. Если все прошли, то в конце поставится отметка, что корабль ставим
                if (field[x, y] == 1) break;

                if (x > 1)
                    if (field[x - 1, y] == 1) break;
                if (y > 1)
                    if (field[x, y - 1] == 1) break;
                if (x > 1 && y > 1)
                    if (field[x - 1, y - 1] == 1) break;
                if (x < 10)
                    if (field[x + 1, y] == 1) break;
                if (y < 10)
                    if (field[x, y + 1] == 1) break;
                if (x < 10 && y < 10)
                    if (field[x + 1, y + 1] == 1) break;
                if (x < 10 && y > 1)
                    if (field[x + 1, y - 1] == 1) break;
                if (x > 1 && y < 10)
                    if (field[x - 1, y + 1] == 1) break;
                checkShip = true;


                System.Console.WriteLine($"X:{x}   Y:{y} - без соседей");

            }

        }

        if (checkShip)
            for (int i = 1; i < shipLength+1; i++)
                field[shipX[i], shipY[i]] = 1;
        if (checkShip) break;

    }

}



void MakeField()
{
    // Console.Clear();
    char verbs = 'A';
    Console.WriteLine("       1     2     3     4     5     6     7     8     9     10");
    Console.WriteLine("     ____________________________________________________________");
    Console.WriteLine();
    for (int i = 1; i <= 10; i++)
    {
        string s = " ";
        if (i == 10) s = "";
        //Console.Write(verbs + "   ");
        Console.Write(i + s+ "   ");
        for (int j = 1; j <= 10; j++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|  ");
            if (field[i, j] == 1) Console.ForegroundColor = ConsoleColor.DarkBlue;
            if (field[i, j] == 2) Console.ForegroundColor = ConsoleColor.Yellow;
            if (field[i, j] == 3) Console.ForegroundColor = ConsoleColor.Red;

            if (field[i, j] == 0) Console.Write(" "); //0 - пустая
            if (field[i, j] == 1) Console.Write("O"); //1 - корабль
            if (field[i, j] == 2) Console.Write("."); //2 - промах
            if (field[i, j] == 3) Console.Write("X"); //3 - попадание

            Console.Write("  ");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("|");
        Console.WriteLine("     ____________________________________________________________");
        Console.WriteLine("");
        verbs++;
    }
}




newShip(4);
newShip(3);
newShip(3);
newShip(2);
newShip(2);
newShip(2);
newShip(1);
newShip(1);
newShip(1);
newShip(1);
MakeField();
