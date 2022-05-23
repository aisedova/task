using System;
using System.IO;
using System.Threading;
using System.Text.Json;

namespace ConsoleApp13
{
    public class Person
    {
        public Int32 Id { get; set; }
        public Guid TransportId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int32 SequenceId { get; set; }
        public String[] CreditCardNumbers { get; set; }
        public Int32 Age { get; set; }
        public String[] Phones { get; set; }
        public Int64 BirthDate { get; set; }
        public Double Salary { get; set; }
        public Boolean IsMarred { get; set; }
        public Gender Gender { get; set; }
        public Child[] Children { get; set; }

        public  Person()
        {
            Random rand = new Random();
            int stringlen = rand.Next(3, 10);
            int randName, randSurname;
            string strName = "", strSurame = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {
                randName = rand.Next(0, 26);
                letter = Convert.ToChar(randName + 65);
                strName = strName + letter;
                randSurname = rand.Next(0, 26);
                letter = Convert.ToChar(randSurname + 65);
                strSurame = strSurame + letter;
            }
            FirstName = strName;
            LastName = strSurame;
            Id = rand.Next(100);
            SequenceId = rand.Next(1000);
            Age = rand.Next(18, 60);
           var G = rand.Next(0, 2);
            if (G == 0)
            {
               Gender = Gender.Male;
                IsMarred = true;
            }
            else
            {
                Gender = Gender.Female;
                IsMarred = false;
            }
            if (rand.Next(0, 2) == 0)
            {
                IsMarred = true;
            }
            else
            {
                IsMarred = false;
            }
            Salary = rand.Next(15000, 150000);
            Phones = new string[10];
            Phones[0] = "8";
            Phones[1] = "9";
            for (int i=2; i<Phones.Length; i++)
            {
                Phones[i] = rand.Next(9).ToString();
            }
            
            Children = new Child[rand.Next(1, 4)];
            Console.WriteLine("CHI  "  + Children.Length);
            for (int i =0; i<Children.Length; i++)
            {
                Children[i] = new Child();
            }
            TransportId = Guid.NewGuid();
            BirthDate = rand.Next(1950,2003);
            CreditCardNumbers = new string[10];
                for (int i = 0; i < CreditCardNumbers.Length; i++)
            {
                CreditCardNumbers[i] =  rand.Next(9).ToString();
            }
            Thread.Sleep(8);
        }
        
    }
    public class Child
    {
        public Int32 Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int64 BirthDate { get; set; }
        public Gender Gender { get; set; }

       public Child()
        {
            Random rand = new Random();
            Id  = 10000 + rand.Next(100);
            int randName, randSurname;
            string strName = "", strSurame = "";
            char letter;
            int stringlen = rand.Next(3, 10);
            for (int i = 0; i < stringlen; i++)
            {
                randName = rand.Next(0, 26);
                letter = Convert.ToChar(randName + 65);
                strName = strName + letter;
                randSurname = rand.Next(0, 26);
                letter = Convert.ToChar(randSurname + 65);
                strSurame = strSurame + letter;
            }
            FirstName = strName;
            LastName = strSurame;
            BirthDate = rand.Next(2004, 2022);
        }
    }
    public enum Gender
    {
        Male ,
        Female
    }
    class Program
    {
        public static void Main(string[] args)
        {           
            Person[] person = new Person[3], person1 = new Person[3];
            for (int i = 0; i < person.Length; i++)
            {
                person[i] = new Person();
            }
            string personJson = "";
            for (int i = 0; i < person.Length; i++)
            {
                 personJson = personJson + JsonSerializer.Serialize(person[i], typeof(Person)) +"\n";
            }
            
            StreamWriter file = File.CreateText("person.json");
            file.WriteLine(personJson);
            file.Close();

            if (File.Exists("person.json"))
            {
                string data = "";
                data = File.ReadAllText("person.json");
                var data1 = data.Split();
                for (int i = 0; i < person.Length; i++)
                {
                    person1[i] = JsonSerializer.Deserialize<Person>(data1[i]);
                }

                int personCount = 0;
                long ageChild = 0;
                for (int i = 0; i < person1.Length; i++)
                {
                    personCount = personCount + (person1[i].Children.Length - 1);
                    for (int j = 0; j < person1[i].Children.Length; j++)
                    {
                        ageChild = ageChild + (2022 - person1[i].Children[j].BirthDate);
                    }
                }
                var a = ageChild / personCount;
                Console.WriteLine($"количество людей : {personCount + person1.Length}");
                Console.WriteLine($"количество кртных карт  : {personCount}");
                Console.WriteLine($"средний возраст детей  : {a}");
                Console.Read();
            }
        }
    }
}
