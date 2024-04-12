using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace tinkstaje
{
    internal class Program
    {
        static void Main(string[] args)
        {
            sixth();
        }

        static void first()//TRUE FALSE MY OPINION => RESULT TRUE TRUE
        {
            string first = "ab";
            string second = "a" + "b";

            Console.WriteLine(first == second);
            Console.WriteLine((object)first == (object)second);
        }

        static void first_info()
        {
            var s1 = string.Format("{0}{1}", "abc", "cba");
            var s2 = "abc" + "cba";
            var s3 = "abccba";

            Console.WriteLine(s1 == s2);
            Console.WriteLine((object)s1 == (object)s2);
            Console.WriteLine(s2 == s3);
            Console.WriteLine((object)s2 == (object)s3);
        }

        static void second()//First MY OPINION first => RESULT ERROR
        {
            Dictionary<Key, string> dictionary = new Dictionary<Key, string>();
            Key firstKey = new Key(1);
            dictionary.Add(firstKey, "First");
            Key secondKey = new Key(2);
            dictionary.Add(secondKey, "Second"); //ON THIS LANE// одинаковые хеш коды форсят на equals отличаются ли вообще обьекты
            Console.WriteLine(dictionary[firstKey]); 


        }

        static void third()//First MY OPINION "abd" => RESULT "abd"
        {
            string key = "a";

            try
            {
                throw new ArgumentException();
            }
            catch (ArgumentException)
            {
                key += "b";
            }
            catch (Exception)
            {
                key += "c";
            }
            finally
            {
                key += "d";
            }
            Console.WriteLine(key);
        }

        static void fourth() // MY OPINION CONSTRUCTED DISPOSED DISPOSED EXCEPTION HANDLED => Resulst same as mine
        {
            try
            {
                using (Robot robot = new Robot())
                {
                    robot.Dispose();// гпт написал, что не рекомедуется внутри using но тут вроде все ок
                    throw new InvalidOperationException();
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Exception handled");
            }
        }

        static void fifth() //my false = real false
        {
            int first = 0;
            object second = (object)first;// упаковка это создание обьекта нового => связи не будет))
            //first = 5;
            Increment(ref first);
            Console.WriteLine(first == (int)second);
            void Increment(ref int source)
            {
                source++;
            }

        }
        
        static void sixth() // static robot ultrabot robot => same as mine
        {
            Ultrabot ultrabot = new Ultrabot();
            Robot1 robot = new Robot1();
        }

    }

    internal class Robot1
    {
        static Robot1() => Console.WriteLine("Static"); // выполняется даже когда создают экземпляр производного класса
        public Robot1() => Console.WriteLine("Robot");

    }

    internal class Ultrabot : Robot1
    {
        public Ultrabot() : base() => Console.WriteLine("Ultrabot");
    }
    internal class Robot : IDisposable
    {
        public Robot() => Console.WriteLine("Constructed");
        public void Dispose() => Console.WriteLine("Disposed");
    }

    internal class Key
    {
        public int Marker { get; }
        public Key(int market) => Marker = market;
        public override int GetHashCode()
        {
            return Marker / 10;
        }

        public override bool Equals(object? other)
        {
            return other is Key ? other.GetHashCode() == GetHashCode() : base.Equals(other);
        }
    }
}

