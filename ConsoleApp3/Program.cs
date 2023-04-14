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
                    arena.BattleFighters();
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
        public void BattleFighters()
        {
            List<Fighter> fighters = new List<Fighter>();

            fighters.Add(new Wrestler("Хабиб", 380, 25, 100));
            fighters.Add(new Kickboxer("Жан-Клод Ван Дамм", 340, 20));
            fighters.Add(new Boxer("Тайсон", 270, 30, 100));
            fighters.Add(new Karateka("Ип Ман", 400, 20));
            fighters.Add(new TaekwondoPractitioner("Марк Дакаскос", 333, 30));

            Console.WriteLine();

            ShowListWarriors(fighters);

            ChooseFighters(fighters);
        }

        private void ChooseFighters(List<Fighter> fighters)
        {
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

                        Console.Clear();

                        ShowListWarriors(fighters);

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

                                    FightFighters(rightFighter, leftFighter);

                                    ShowWinningFighter(rightFighter, leftFighter);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FightFighters(Fighter rightFighter, Fighter leftFighter) 
        {
            Console.WriteLine("\nБОЙ НАЧИНАЕТСЯ!");
            Console.WriteLine();

            while (rightFighter.HealthWarrior >= 0 && leftFighter.HealthWarrior >= 0)
            {
                rightFighter.DealDamage(rightFighter, leftFighter);
                leftFighter.DealDamage(leftFighter, rightFighter);
                rightFighter.ShowInfoWarriors();
                leftFighter.ShowInfoWarriors();

                Console.WriteLine("---------------------------------------------------");
                Console.ReadKey();
            }
        }

        private void ShowWinningFighter(Fighter rightFighter, Fighter leftFighter)
        {
            if (rightFighter.HealthWarrior <= 0)
            {
                Console.WriteLine("Победа бойца - " + leftFighter.NameWarrior);
                return;
            }
            else if (leftFighter.HealthWarrior <= 0)
            {
                Console.WriteLine("Победа бойца - " + rightFighter.NameWarrior);
                return;
            }
        }

        private void ShowListWarriors(List<Fighter> fighters)
        {
            for (int i = 0; i < fighters.Count; i++)
            {
                Console.Write(i + ". ");
                fighters[i].ShowInfoWarriors();
            }
        }
    }

    class Fighter
    {
        protected string Name;
        protected int Health;
        protected int Damage;

        public Fighter(string nameCombatant, int healthCombatant, int damageCombatant)
        {
            Name = nameCombatant;
            Health = healthCombatant;
            Damage = damageCombatant;
        }

        public string NameWarrior
        {
            get
            {
                return Name;
            }
        }

        public int DamageWarrior
        {
            get
            {
                return Damage;
            }
        }

        public int HealthWarrior
        {
            get
            {
                return Health;
            }
        }

        public virtual void ShowInfoWarriors()
        {
            Console.WriteLine("Имя - " + Name + ", Здоровье - " + Health + " хп; " + "Урон - " + Damage + ";");
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual void DealDamage(Fighter rightFighter, Fighter leftFighter)
        {
            rightFighter.TakeDamage(leftFighter.DamageWarrior);
        }
    }

    class Boxer : Fighter
    {
        private int _armorProfessionalPuncher = 100;

        public Boxer(string nameCombatant, int healthCombatant, int damageCombatant, int armorPuncher) : base(nameCombatant, healthCombatant, damageCombatant)
        {
            _armorProfessionalPuncher = armorPuncher;
        }

        public override void ShowInfoWarriors()
        {
            base.ShowInfoWarriors();
            Console.WriteLine("Защита - " + _armorProfessionalPuncher);
        }

        public override void DealDamage(Fighter rightFighter, Fighter leftFighter)
        {
            BlockDamage(leftFighter);
        }

        private void BlockDamage(Fighter leftFighter)
        {
            Random random = new Random();

            int halfBlockedDamage = 2;
            int activatingDamageLock = 40;
            int minimumActivatingDamageLock = 1;
            int maximumActivatingDamageLock = 100;

            if (activatingDamageLock > random.Next(minimumActivatingDamageLock, maximumActivatingDamageLock))
            {
                int blockedDamage = leftFighter.DamageWarrior / halfBlockedDamage;
                Console.WriteLine(Name + " блокирует - " + blockedDamage + " урона.");
                _armorProfessionalPuncher -= blockedDamage;

                TryTakeDefenseDamage();
            }
            else
            {
                _armorProfessionalPuncher -= leftFighter.DamageWarrior;

                TryTakeDefenseDamage();
            }
        }

        private bool TryTakeDefenseDamage()
        {
            if (_armorProfessionalPuncher >= 0)
            {
                return true;
            }
            else
            {
                Health += _armorProfessionalPuncher;
                _armorProfessionalPuncher = 0;
            }

            return false;
        }
    }

    class Wrestler : Fighter
    {
        private int _samboWarriorEndurance = 100;
        private int _usingSkillDoubleAttack = 25;

        public Wrestler(string nameCombatant, int healthCombatant, int damageCombatant, int samboCombatantEndurance) : base(nameCombatant, healthCombatant, damageCombatant)
        {
            _samboWarriorEndurance = samboCombatantEndurance;
        }

        public override void ShowInfoWarriors()
        {
            base.ShowInfoWarriors();
            Console.WriteLine("Выносливость - " + _samboWarriorEndurance);
        }

        public override void DealDamage(Fighter rightFighter, Fighter leftFighter)
        {
            rightFighter.TakeDamage(leftFighter.DamageWarrior);

            UseDoubleAttack(rightFighter, leftFighter);
        }

        private void UseDoubleAttack(Fighter rightFighter, Fighter leftFighter)
        {
            Random random = new Random();

            int activationDoubleAttack = 35;
            int minimumActivationDoubleAttack = 1;
            int maximumActivationDoubleAttack = 100;

            if (activationDoubleAttack > random.Next(minimumActivationDoubleAttack, maximumActivationDoubleAttack))
            {
                _samboWarriorEndurance -= _usingSkillDoubleAttack;

                if (TryDoubleAttack())
                {
                    leftFighter.TakeDamage(rightFighter.DamageWarrior);
                }
                else
                {
                    _samboWarriorEndurance = 0;
                }
            }
        }

        private bool TryDoubleAttack()
        {
            if (_samboWarriorEndurance >= 0)
            {
                Console.WriteLine("Боец " + Name + " применяет способность двойная атака, используя " + _usingSkillDoubleAttack + "% выносливости.");
                return true;
            }
            else
            {
                _samboWarriorEndurance = 0;
                return false;
            }
        }
    }

    class Kickboxer : Fighter
    {
        private int _beginningDoubleDamageCountdown = 0;
        private int _endDoubleDamageCountdown = 3;

        public Kickboxer(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void DealDamage(Fighter rightFighter, Fighter leftFighter)
        {
            rightFighter.TakeDamage(leftFighter.DamageWarrior);

            UseDoubleDamage(rightFighter, leftFighter);
        }

        private void UseDoubleDamage(Fighter rightFighter, Fighter leftFighter)
        {
            if (_endDoubleDamageCountdown <= _beginningDoubleDamageCountdown)
            {
                Console.WriteLine($"Двойной урон наносит {Name}");

                if (_endDoubleDamageCountdown == _beginningDoubleDamageCountdown)
                {
                    _endDoubleDamageCountdown += _endDoubleDamageCountdown;
                }

                leftFighter.TakeDamage(rightFighter.DamageWarrior);
            }
            else
            {
                this._beginningDoubleDamageCountdown++;
            }
        }
    }

    class Karateka : Fighter
    {
        public Karateka(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void DealDamage(Fighter rightFighter, Fighter leftFighter)
        {
            rightFighter.TakeDamage(leftFighter.DamageWarrior);

            UseBleedingDamage(leftFighter);
        }

        private void UseBleedingDamage(Fighter leftFighter)
        {
            Random random = new Random();

            int activationBleedingDamage = 40;
            int minimumBleedingDamage = 1;
            int maximumBleedingDamage = 100;
            int bleedingDamage = random.Next(10, 30);

            if (activationBleedingDamage > random.Next(minimumBleedingDamage, maximumBleedingDamage))
            {
                Console.WriteLine(Name + ", выполняет режущий удар с логтя, у противника кровотечение на - " + bleedingDamage + " урона.");
                leftFighter.TakeDamage(bleedingDamage);
            }
        }
    }

    class TaekwondoPractitioner : Fighter
    {
        public TaekwondoPractitioner(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void DealDamage(Fighter rightFighter, Fighter leftFighter)
        {
            rightFighter.TakeDamage(leftFighter.DamageWarrior);

            UseReflectionDamage(leftFighter);
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

            int activationReflectionDamage = 25;
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

        private void UseReflectionDamage(Fighter leftFighter)
        {
            Random random = new Random();

            int activationReflectionDamage = 30;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                Console.WriteLine(Name + " отражает атаку противника на " + leftFighter.DamageWarrior + " урона.");
                leftFighter.TakeDamage(leftFighter.DamageWarrior);
            }
        }

        private void UseEvasionDamage()
        {
            int damage = 0;
            Console.WriteLine(Name + " уклоняется от атаки получив " + damage + " урона.");
            Health -= damage;
        }
    }
}
