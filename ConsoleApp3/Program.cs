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
                    arena.Work();
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
        public void Work()
        {
            List<Fighter> fighters = new List<Fighter>
            {
                new Wrestler(nameof(Wrestler), 400, 30, 100),
                new Kickboxer(nameof(Kickboxer), 400, 5),
                new Boxer(nameof(Boxer), 400, 30, 100),
                new Karateka(nameof(Karateka), 400, 20),
                new TaekwondoPractitioner(nameof(TaekwondoPractitioner), 400, 20)
            };

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
                rightFighter.Attack(leftFighter);
                leftFighter.Attack(rightFighter);
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
            Console.WriteLine("Name - " + Name + ", Health - " + Health + " хп; " + "Damage - " + Damage + ";");
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);
        }
    }

    class Boxer : Fighter
    {
        private int _armorProfessionalPuncher = 100;

        public Boxer(string nameCombatant, int healthCombatant, int damageCombatant, int armorPuncher) : base(nameCombatant, healthCombatant, damageCombatant)
        {
            _armorProfessionalPuncher = armorPuncher;
            Name = nameof(Boxer);
        }

        public override void ShowInfoWarriors()
        {
            base.ShowInfoWarriors();
            Console.WriteLine("Защита - " + _armorProfessionalPuncher);
        }

        public override void TakeDamage(int damage)
        {
            damage = 0;
            base.TakeDamage(damage);           
        }

        public override void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);

            BlockDamage(fighter);
        }

        private void BlockDamage(Fighter fighter)
        {
            Random random = new Random();

            int halfBlockedDamage = 2;
            int activatingDamageLock = 40;
            int minimumActivatingDamageLock = 1;
            int maximumActivatingDamageLock = 100;

            if (activatingDamageLock > random.Next(minimumActivatingDamageLock, maximumActivatingDamageLock))
            {              
                int blockedDamage = fighter.DamageWarrior / halfBlockedDamage;
                Console.WriteLine(Name + " блокирует - " + blockedDamage + " урона.");
                _armorProfessionalPuncher -= blockedDamage;

                base.TakeDamage(blockedDamage);

                TryTakeDefenseDamage();
            }
            else
            {
                _armorProfessionalPuncher -= fighter.DamageWarrior;

                base.TakeDamage(fighter.DamageWarrior);

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

        public override void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);

            UseDoubleAttack(fighter);
        }

        private void UseDoubleAttack(Fighter fighter)
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
                    fighter.TakeDamage(Damage);
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

        public override void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);

            UseDoubleDamage(fighter);
        }

        private void UseDoubleDamage(Fighter fighter)
        {
            if (_endDoubleDamageCountdown <= _beginningDoubleDamageCountdown)
            {
                Console.WriteLine($"Двойной урон наносит {Name}");

                if (_endDoubleDamageCountdown == _beginningDoubleDamageCountdown)
                {
                    _endDoubleDamageCountdown += _endDoubleDamageCountdown;
                }

                fighter.TakeDamage(Damage);
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

        public override void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);

            UseBleedingDamage(fighter);
        }

        private void UseBleedingDamage(Fighter fighter)
        {
            Random random = new Random();

            int activationBleedingDamage = 40;
            int minimumBleedingDamage = 1;
            int maximumBleedingDamage = 100;
            int bleedingDamage = random.Next(10, 30);

            if (activationBleedingDamage > random.Next(minimumBleedingDamage, maximumBleedingDamage))
            {
                Console.WriteLine(Name + ", выполняет режущий удар с логтя, у противника кровотечение на - " + bleedingDamage + " урона.");
                fighter.TakeDamage(bleedingDamage);
            }
        }
    }

    class TaekwondoPractitioner : Fighter
    {
        public TaekwondoPractitioner(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);

            UseReflectionDamage(fighter);
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

        private void UseReflectionDamage(Fighter fighter)
        {
            Random random = new Random();

            int activationReflectionDamage = 30;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                Console.WriteLine(Name + " отражает атаку противника на " + fighter.DamageWarrior + " урона.");
                fighter.TakeDamage(fighter.DamageWarrior);
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
