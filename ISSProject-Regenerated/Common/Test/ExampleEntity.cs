using ISSProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject
{
    internal class ExampleEntity : IKeyedEntity<int>
    {

        private readonly int id = 0;
        public string name = "";

        public ExampleEntity(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int GetId()
        {
            return id;
        }


    }
}
