using System;

namespace OOP8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandСhooseFighters = "1";
            const string CommandExit = "2";

            Arena arena = new Arena();

            bool isWorking = true;

            Console.WriteLine($"{CommandСhooseFighters} - ВЫБРАТЬ БОЙЦОВ" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                if (CommandСhooseFighters == userInput)
                {
                    arena.Fight();
                }
                else if (userInput == CommandExit)
                {
                    isWorking = false;
                }
                else
                {
                    Console.WriteLine($"Ошибка. Введите {CommandСhooseFighters} или {CommandExit}");
                }
            }
        }
    }

    class Arena
    {
        public void Fight()
        {
            List<Fighter> fighters = new List<Fighter>();

            fighters.Add(new Wrestler("Хабиб", 1000, 20));
            fighters.Add(new Kickboxer("Жан-Клод Ван Дамм", 1000, 15));
            fighters.Add(new Boxer("Тайсон", 100, 20));
            fighters.Add(new Karateka("Ип Ман", 100, 17));
            fighters.Add(new TaekwondoPractitioner("Марк Дакаскос", 100, 16));

            Console.WriteLine();

            ShowListFighters(fighters);

            Console.Write("\nВыберите бойца c правой стороны - ");
            string userInputRightFighter = Console.ReadLine();

            bool isSuccess = int.TryParse(userInputRightFighter, out int rightFighterIndex);

            if (isSuccess)
            {
                for (int i = 0; i < fighters.Count; i++)
                {
                    if (rightFighterIndex == i)
                    {
                        Fighter rightFighter = fighters[rightFighterIndex];

                        fighters.Remove(rightFighter);

                        Console.WriteLine($"\nВы выбрали первого бойца - {rightFighter.Name}.");
                        Console.Clear();

                        ShowListFighters(fighters);

                        Console.Write("\nВыберите бойца с левой стороны - ");
                        string userInputLeftFighter = Console.ReadLine();

                        isSuccess = int.TryParse(userInputLeftFighter, out int leftFighterIndex);

                        if (isSuccess)
                        {
                            for(int j = 0; j < fighters.Count; j++)
                            {
                                if (leftFighterIndex == j)
                                {
                                    Fighter leftFighter = fighters[leftFighterIndex];

                                    fighters.Remove(leftFighter);

                                    Console.WriteLine($"\nВы выбрали второго бойца - {rightFighter.Name}.");
                                    Console.WriteLine("\nБОЙ НАЧИНАЕТСЯ!");
                                    Console.WriteLine();

                                    while (rightFighter.Health > 0 && leftFighter.Health > 0)
                                    {
                                        rightFighter.TakeDamage(leftFighter.DealDamage());
                                        leftFighter.TakeDamage(rightFighter.DealDamage());
                                        rightFighter.ShowInfoFighter();
                                        leftFighter.ShowInfoFighter();

                                        Console.ReadKey();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка. Данный номер отсутствует.");
                                }
                            }                        
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка. Данный номер отсутствует.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка. Введите номер из списка бойцов.");
            }
        }

        private void ShowListFighters(List<Fighter> fighters)
        {
            for (int i = 0; i < fighters.Count; i++)
            {
                Console.Write(i + ". ");
                fighters[i].ShowInfoFighter();
            }
        }
    }

    class Fighter
    {
        protected string _name;
        protected int _health;
        protected int _damage;

        public Fighter(string name, int health, int damage)
        {
            _name = name;
            _health = health;
            _damage = damage;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }
        }

        public virtual void TakeDamage(int damage)
        {
            _health -= _damage;
        }

        public virtual int DealDamage()
        {
            return _damage;
        }

        public void ShowInfoFighter()
        {
            Console.WriteLine("Имя - " + _name + ", Здоровье - " + _health + " хп; " + "Урон - " + _damage);
        }
    }

    class Wrestler : Fighter
    {
        private int _fighterMinimumStamina = 0;
        private int _fighterMaximumStamina = 100;

        public Wrestler(string name, int health, int damage) : base(name, health, damage) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public override int DealDamage()
        {
            
        }

        private int DoubleAttack()
        {
            
        }

        private bool TryDoubleAttack()
        {
            int enduranceDoubleAttack = 25;
            _fighterMaximumStamina -= enduranceDoubleAttack; 

            if (_fighterMaximumStamina >= _fighterMinimumStamina)
            {
                Console.WriteLine("У " + _name + " " + _fighterMaximumStamina + " и он применяет две атаки подряд.");

                return true;
            }
            else
            {
                _fighterMaximumStamina = 0;
                return false;
            }
        }
    }

    class Kickboxer : Fighter
    {
        private int _number = 0;
        private int _frequencyDoubleDamage = 3;

        public Kickboxer(string name, int health, int damage) : base(name, health, damage) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public override int DealDamage()
        {
            return DoubleDamage();
        }

        private int DoubleDamage()
        {
            int doubleDamage = 30;
            int damage = 15;

            if (_frequencyDoubleDamage <= _number)
            {
                Console.WriteLine($"Двойной урон наносит {_name}");

                if (_frequencyDoubleDamage == _number)
                {
                    _frequencyDoubleDamage += _frequencyDoubleDamage;
                }

                return _damage = doubleDamage;
            }
            else
            {
                this._number++;
                return _damage = damage;
            }
        }
    }

    class Boxer : Fighter
    {
        public Boxer(string name, int health, int damage) : base(name, health, damage) { }

        public int Armor; //защита

        public override void DealDamage(int damage)
        {

        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Reflection();
        }

        private int Vampirism() //вампиризм отнимает здоровье и прибавляет его себе
        {
            //_health += _damage;
            //return _health;
        }

        private void Reflection() // отражает урон в противника
        {

        }
    }

    class Karateka : Fighter
    {
        public Karateka(string name, int health, int damage) : base(name, health, damage) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            BleedingDamage();
        }

        public override void DealDamage(int damage)
        {

        }

        private void BleedingDamage() // шанс на кровотечение, здоровье отнимется
        {

        }
    }

    class TaekwondoPractitioner : Fighter
    {
        public TaekwondoPractitioner(string name, int health, int damage) : base(name, health, damage) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Evasion();
        }

        public override void DealDamage(int damage)
        {

        }

        private void Evasion() // шанс уклонится и не получить урон
        {

        }
    }
}
