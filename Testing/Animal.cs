using System;

namespace Zoo
{
    public class Animal : INameable
    {
        public string Name {get; set;}

        public Animal (string name)
        {
            Name = name;
        }
    }
}