using System;
using System.Collections.Generic;

namespace EventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CollegeClassModel history = new CollegeClassModel("History 101", 3);
            CollegeClassModel math = new CollegeClassModel("Calculus 201", 2);

            history.EnrollmentFull += CollegeClass_EnrollmentFull;

            history.SignUpStudent("Tim Corey").PrintToConsole();
            history.SignUpStudent("Sue Storm").PrintToConsole();
            history.SignUpStudent("Joe Schmo").PrintToConsole();
            history.SignUpStudent("Tootie Fruiti").PrintToConsole();
            history.SignUpStudent("Lou Begga").PrintToConsole();
            Console.WriteLine();

            math.EnrollmentFull += CollegeClass_EnrollmentFull;

            math.SignUpStudent("Joe Schmo").PrintToConsole();
            math.SignUpStudent("Tootie Fruiti").PrintToConsole();
            math.SignUpStudent("Lou Begga").PrintToConsole();
            Console.ReadLine();
        }

        private static void CollegeClass_EnrollmentFull(object sender, string e)
        {
            CollegeClassModel model = (CollegeClassModel)sender;
            Console.WriteLine($"{ model.CourseTitle }: Full");
        }

    }

    public static class ConsoleHelpers
    {
        public static void PrintToConsole(this string message)
        {
            Console.WriteLine(message);
        }
    }
    public class CollegeClassModel
    {
        public event EventHandler<string> EnrollmentFull;
        private List<string> enrolledStudents = new List<string>();
        private List<string> waitingList = new List<string>();

        public string CourseTitle { get; private set; }
        public int MaximumStudents { get; private set; }
        public CollegeClassModel(string title, int maximumStudents)
        {
            CourseTitle = title;
            MaximumStudents = maximumStudents;
        }

        public string SignUpStudent(string studentName)
        {
            string output = "";
            //check to see if we are maxed out
            if (enrolledStudents.Count < MaximumStudents)
            {
                enrolledStudents.Add(studentName);
                output = $"{studentName} was enrolled in {CourseTitle}";
                //The ? means if it is not null do this next thing
                if (enrolledStudents.Count == MaximumStudents)
                {
                    EnrollmentFull?.Invoke(this, $"{ CourseTitle} enrollment is now full.");
                }
                
            }
            else
            {
                waitingList.Add(studentName);
                output = $"{studentName} was added to the waitlist in {CourseTitle}";
            }

            return output;
        }
    }
}
