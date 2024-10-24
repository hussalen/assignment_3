using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class ParentExtent
    {
        private static List<Parent> Parents_List
        {
            get { return Parents_List.ToList(); }
        }

        private static void addParent(Parent parent)
        {
            if (parent != null)
            {
                Parents_List.Add(parent);
                return;
            }
            throw new ArgumentException("Parent cannot be null!");
        }
    }
}
