using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace B3ButcheryCE
{
    public class Option
    {
        public Option(string name, long value)
        {
            Debug.Assert(!string.IsNullOrEmpty(name), "option's name can not be null");
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public long Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
