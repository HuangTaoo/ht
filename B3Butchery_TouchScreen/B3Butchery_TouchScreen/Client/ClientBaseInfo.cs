using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3HuaDu_TouchScreen.Client
{
  public class ClientBaseInfo
  {
    public long ID { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }
}
