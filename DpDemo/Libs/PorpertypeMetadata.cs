using System;

namespace DpDemo.Libs
{
    public class PorpertypeMetadata
    {
        public Type Type { get; set; } 
        public object Value { get; set; }

        public PorpertypeMetadata(object defaultValue)
        {
            this.Value = defaultValue;
        }
    }
}