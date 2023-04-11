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

            fighters.Add(new Wrestler("Хабиб", 100, 15, 100));
            fighters.Add(new Kickboxer("Жан-Клод Ван Дамм", 100, 15));
            fighters.Add(new Boxer("Тайсон", 100, 20, 67));
            fighters.Add(new Karateka("Ип Ман", 100, 10));
            fighters.Add(new TaekwondoPractitioner("Марк Дакаскос", 100, 20));

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

                        Console.WriteLine($"\nВы выбрали первого бойца - {rightFighter.NameWarrior}.");
                        Console.Clear();

                        ShowListFighters(fighters);

                        Console.Write("\nВыберите бойца с левой стороны - ");
                        string userInputLeftFighter = Console.ReadLine();

                        isSuccess = int.TryParse(userInputLeftFighter, out int leftFighterIndex);

                        if (isSuccess)
                        {
                            for (int j = 0; j < fighters.Count; j++)
                            {
                                if (leftFighterIndex == j)
                                {
                                    Fighter leftFighter = fighters[leftFighterIndex];

                                    fighters.Remove(leftFighter);

                                    Console.WriteLine($"\nВы выбрали второго бойца - {rightFighter.NameWarrior}.");
                                    Console.WriteLine("\nБОЙ НАЧИНАЕТСЯ!");
                                    Console.WriteLine();

                                    while (rightFighter.HealthWarrior >= 0 && leftFighter.HealthWarrior >= 0)
                                    {
                                        rightFighter.TakeDamage(leftFighter.DealDamage(rightFighter));
                                        leftFighter.TakeDamage(rightFighter.DealDamage(leftFighter));
                                        rightFighter.ShowInfoFighter();
                                        leftFighter.ShowInfoFighter();

                                        Console.WriteLine("---------------------------------------------------");
                                        Console.ReadKey();

                                        if (rightFighter.HealthWarrior <= 0)
                                        {
                                            Console.WriteLine("Победа бойца - " + rightFighter.NameWarrior);
                                            return;
                                        }
                                        else if (leftFighter.HealthWarrior <= 0)
                                        {
                                            Console.WriteLine("Победа бойца - " + leftFighter.NameWarrior);
                                            return;
                                        }
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
        protected string _nameWarrior;
        protected int _healthWarrior;
        protected int _damageWarrior;

        public Fighter(string nameCombatant, int healthCombatant, int damageCombatant)
        {
            _nameWarrior = nameCombatant;
            _healthWarrior = healthCombatant;
            _damageWarrior = damageCombatant;
        }

        public string NameWarrior
        {
            get
            {
                return _nameWarrior;
            }
        }

        public int HealthWarrior
        {
            get
            {
                return _healthWarrior;
            }
        }

        public virtual void TakeDamage(int damage)
        {
            _healthWarrior -= damage;
        }

        public virtual int DealDamage(Fighter fighter)
        {
            return _damageWarrior;
        }

        public virtual void ShowInfoFighter()
        {
            Console.WriteLine("Имя - " + _nameWarrior + ", Здоровье - " + _healthWarrior + " хп; " + "Урон - " + _damageWarrior + ";");
        }
    }

    class Wrestler : Fighter
    {
        private int _wrestlerEndurance = 100;
        private int _usingSkillDoubleAttack = 25;

        public Wrestler(string name, int health, int damage, int endurance) : base(name, health, damage)
        {
            _wrestlerEndurance = endurance;
        }

        public override void ShowInfoFighter()
        {
            base.ShowInfoFighter();
            Console.WriteLine("Выносливость - " + _wrestlerEndurance);
        }

        public override int DealDamage(Fighter fighter)
        {
            return UseDoubleAttack(fighter);
        }

        private int UseDoubleAttack(Fighter fighter)
        {
            Random random = new Random();

            int activationDoubleAttack = 40;
            int minimumActivationDoubleAttack = 1;
            int maximumActivationDoubleAttack = 100;

            if (activationDoubleAttack > random.Next(minimumActivationDoubleAttack, maximumActivationDoubleAttack))
            {
                _wrestlerEndurance -= _usingSkillDoubleAttack;

                if (TryDoubleAttack())
                {
                    int damage = _damageWarrior;
                    fighter.TakeDamage(damage);
                    return _damageWarrior;
                }
                else
                {
                    _wrestlerEndurance = 0;
                    return _damageWarrior;
                }
            }
            else
            {
                return _damageWarrior;
            }
        }

        private bool TryDoubleAttack()
        {
            if (_wrestlerEndurance >= 0)
            {
                Console.WriteLine("Боец " + _nameWarrior + " применяет способность двойная атака, используя " + _usingSkillDoubleAttack + "% выносливости.");
                return true;
            }
            else
            {
                _wrestlerEndurance = 0;
                return false;
            }
        }
    }

    class Kickboxer : Fighter
    {
        private int _beginningDoubleDamageCountdown = 0; 
        private int _endDoubleDamageCountdown = 3;

        public Kickboxer(string name, int health, int damage) : base(name, health, damage) { }

        public override int DealDamage(Fighter fighter)
        {
            return UseDoubleDamage();
        }

        private int UseDoubleDamage()
        {
            int doubleDamage = 30;
            int damage = 15;

            if (_endDoubleDamageCountdown <= _beginningDoubleDamageCountdown)
            {
                Console.WriteLine($"Двойной урон наносит {_nameWarrior}");

                if (_endDoubleDamageCountdown == _beginningDoubleDamageCountdown)
                {
                    _endDoubleDamageCountdown += _endDoubleDamageCountdown;
                }

                return _damageWarrior = doubleDamage;
            }
            else
            {
                this._beginningDoubleDamageCountdown++;
                return _damageWarrior = damage;
            }
        }
    }

    class Boxer : Fighter
    {
        private int _armorProfessionalBoxer = 100;

        public Boxer(string name, int health, int damage, int armor) : base(name, health, damage)
        {
            _armorProfessionalBoxer = armor;
        }

        public override void ShowInfoFighter()
        {
            base.ShowInfoFighter();
            Console.WriteLine("Защита - " + _armorProfessionalBoxer);
        }

        public override void TakeDamage(int damage)
        {
            BlockDamage(damage);
        }

        private void BlockDamage(int damage)
        {
            Random random = new Random();

            int halfBlockedDamage = 2;
            int activatingDamageLock = 35;
            int minimumActivatingDamageLock = 1;
            int maximumActivatingDamageLock = 100;

            if (activatingDamageLock > random.Next(minimumActivatingDamageLock, maximumActivatingDamageLock))
            {
                int blockedDamage = damage / halfBlockedDamage;
                Console.WriteLine(_nameWarrior + " блокирует - " + blockedDamage + " урона.");
                _armorProfessionalBoxer -= blockedDamage;

                TryTakeDefenseDamage();
            }
            else
            {
                _armorProfessionalBoxer -= damage;

                TryTakeDefenseDamage();
            }
        }

        private bool TryTakeDefenseDamage()
        {
            if (_armorProfessionalBoxer >= 0)
            {
                return true;
            }
            else
            {
                _healthWarrior += _armorProfessionalBoxer;
                _armorProfessionalBoxer = 0;
            }

            return false;
        }
    }

    class Karateka : Fighter
    {
        public Karateka(string name, int health, int damage) : base(name, health, damage) { }

        public override int DealDamage(Fighter fighter)
        {
            return UseBleedingDamage(fighter);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        private int UseBleedingDamage(Fighter fighter)
        {
            Random random = new Random();

            int activationBleedingDamage = 45;
            int minimumBleedingDamage = 1;
            int maximumBleedingDamage = 100;
            int bleedingDamage = random.Next(10, 25);

            if (activationBleedingDamage > random.Next(minimumBleedingDamage, maximumBleedingDamage))
            {
                Console.WriteLine(_nameWarrior + ", выполняет режущий удар с логтя, у противника кровотечение на - " + bleedingDamage + " урона.");
                fighter.TakeDamage(bleedingDamage);
                return _damageWarrior;
            }
            else
            {
                return _damageWarrior;
            }
        }       
    }

    class TaekwondoPractitioner : Fighter
    {
        public TaekwondoPractitioner(string name, int health, int damage) : base(name, health, damage) { }

        public override int DealDamage(Fighter fighter)
        {
            return UseReflectionDamage(fighter);
        }

        public override void TakeDamage(int damage)
        {
            if (TryReflectionDamageFighter())
            {
                UseEvasionDamage();
            }
            else
            {
                base.TakeDamage(damage);
            }
        }

        private bool TryReflectionDamageFighter()
        {
            Random random = new Random();

            int activationReflectionDamage = 30;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int UseReflectionDamage(Fighter fighter) 
        {
            Random random = new Random();

            int activationReflectionDamage = 50;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                fighter.TakeDamage(fighter.DealDamage(fighter));
                Console.WriteLine(_nameWarrior + " отражает атаку противника на " + fighter.DealDamage(fighter) + " урона.");
                return _damageWarrior;
            }
            else
            {
                return _damageWarrior;
            }
        }

        private void UseEvasionDamage()
        {            
            int damage = 0;
            Console.WriteLine(_nameWarrior + " уклоняется от атаки получив " + damage + " урона.");
            _healthWarrior -= damage;
        }
    }
}
