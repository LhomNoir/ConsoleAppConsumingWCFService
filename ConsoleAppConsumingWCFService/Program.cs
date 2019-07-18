using ConsoleAppConsumingWCFService.Helpers;
using ConsoleAppConsumingWCFService.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleAppConsumingWCFService
{
    enum Mood
    {
        Angry,
        Happy,
        Distributeur
    }

    public enum Season
    {
        [Display(Name = "It's autumn")]
        Autumn,

        [Display(Name = "It's winter")]
        Winter,

        [Display(Name = "It's spring")]
        Spring,

        [Display(Name = "It's summer")]
        Summer
    }


    class Program
    {       
        private static Person person;
        private static List<Person> persons;
       
        static void Main(string[] args)
        {
            person = new Person();

            persons = new List<Person>();

            GetPersons();

            //InsertPerson();

            UpdatePerson();

            Console.WriteLine("");

            Console.ReadKey();
        }        

        static IList<Person> GetPersons()
        {
            Service1Client service1Client = new Service1Client();

            var veta = service1Client.GetAllPersons().ToList();

            persons = service1Client.GetPersons().ToList();
            if (persons != null  && persons.Count > 0)
            {
                Console.WriteLine($"Voici les données : [{persons.Count}]");
            }

            return null;
        }

        static void InsertPerson()
        {
            person.Name = "Kamto";
            person.Age = 33;

            Service1Client service1Client = new Service1Client();

            if (service1Client.InsertPerson(person) == 1)
            {
                Console.WriteLine("Bravo ! Insertion effectuée avec succès");
            }
        }

        static void UpdatePerson()
        {
            person.Id = 3;
            person.Name = "Kamto";
            person.Age = 26;

            Service1Client service1Client = new Service1Client();

            if (service1Client.UpdatePerson(person) == 1)
            {
                Console.WriteLine("Bravo ! modification effectuée avec succès");
            }
        }

        static void DeletePerson()
        {
            person.Id = 2;
            person.Name = "Pup Daday";
            person.Age = 26;

            Service1Client service1Client = new Service1Client();

            if (service1Client.DeltePerson(person) == 1)
            {
                Console.WriteLine("Bravo ! modification effectuée avec succès");
            }
        }
    }
}
