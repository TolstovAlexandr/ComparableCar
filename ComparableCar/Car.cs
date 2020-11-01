using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparableCar
{
    public class Car : IComparable
    {
        // Constant for maximum speed.
        public const int MaxSpeed = 100;

        public int CarID { get; set; }
        // Car properties.
        public int CurrentSpeed { get; set; } = 0;
        public string PetName { get; set; } = "";
        // Is the car still operational?
        private bool carIsDead;
        // A car has-a radio.
        private Radio theMusicBox = new Radio();
        // Constructors.
        public Car() { }
        public Car(string name, int speed)
        {
            CurrentSpeed = speed;
            PetName = name;
        }
        public void CrankTunes(bool state)
        {
            // Delegate request to inner object.
            theMusicBox.TurnOn(state);
        }
        public Car(string name, int currSp, int id)
        {
            CurrentSpeed = currSp;
            PetName = name;
            CarID = id;
        }
        // See if Car has overheated.
        public void Accelerate(int delta)
        {
            if (carIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed >= MaxSpeed)
                {
                    carIsDead = true;
                    CurrentSpeed = 0;
                    // Use the "throw" keyword to raise an exception.
                    //throw new Exception($"{PetName} has overheated!");
                    CarIsDeadException ex = new CarIsDeadException($"{PetName} has overheated!",
                        "You have a lead foot", DateTime.Now);
                        ex.HelpLink = "http://www.CarsRUs.com";
                    throw ex;
                }
                else
                    Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
            }
        }

        public int CompareTo(object obj)
        {
            Car temp = obj as Car;
            if (temp != null)
                return this.CarID.CompareTo(temp.CarID);
            else
                throw new ArgumentException("Parameter is not a Car!");
        }

        public static IComparer SortByPetName
        { get { return (IComparer)new PetNameComparer(); } }
    }
}
