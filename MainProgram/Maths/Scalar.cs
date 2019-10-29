using System;

namespace MainProgram.Maths
{
    public class Scalar
    {
        public float Value { get; set; }
        
        public Scalar(float value)
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
                Value = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
