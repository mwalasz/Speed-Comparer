using System;

namespace MainProgram.Maths
{
    public class Scalar
    {
        public int Value { get; set; }
        
        public Scalar(int value)
        {
            Value = value;
        }

        public Scalar(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new System.ArgumentException("message", nameof(value));
            }
            else
            {
                Value = Convert.ToInt32(value);
            }
        }
    }
}
