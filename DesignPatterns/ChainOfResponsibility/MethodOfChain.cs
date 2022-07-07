namespace ChainOfResponsibility;

public class MethodOfChain {
    public class Creature {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense) {
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }
    
    // Think it is a base class, a linked list
    // This is not an abstract class because we're using it as a root object, definition of the linked list
    public class CreatureModifier {
        protected Creature creature;
        protected CreatureModifier next; // Linked list ! 

        public CreatureModifier(Creature creature) {
            this.creature = creature;
        }

        public void Add(CreatureModifier cm) {
            if (next != null) next.Add(cm); // if there is already a modifier, add cm to end of the list
            else next = cm; // else, set it directly 
        }

        public virtual void Handle() => next?.Handle();
    }

    public class NoBonusesModifier : CreatureModifier {
        public NoBonusesModifier(Creature creature) : base(creature) { }

        public override void Handle() {
            // Stop the process, do not call the remaining linked list
        }
    }

    public class DoubleAttackModifier : CreatureModifier {
        public DoubleAttackModifier(Creature creature) : base(creature) { }

        public override void Handle() {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreasedDefenseModifier : CreatureModifier {
        public IncreasedDefenseModifier(Creature creature) : base(creature) { }

        public override void Handle() {
            Console.WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
            base.Handle();
        }
    }

    public static void main() {
        var goblin = new Creature("Goblin", 2, 2);
        Console.WriteLine(goblin);

        var root = new CreatureModifier(goblin);
        
        // root.Add(new NoBonusesModifier(goblin));

        Console.WriteLine("Let's double the goblin's attack");
        root.Add(new DoubleAttackModifier(goblin));

        Console.WriteLine("Let's increase the goblin's defense");
        root.Add(new IncreasedDefenseModifier(goblin));
    
        root.Handle();
        
        Console.WriteLine(goblin);
    }
}