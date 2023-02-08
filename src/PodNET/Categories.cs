using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace SynthesisCode.Open.PodNET
{
    public class PodCategory
    {
        public string Name { get; set; }
        public List<PodCategory> SubCategories { get; set; }

        public PodCategory() //string name)
        {
            //Name = name;
            SubCategories = new List<PodCategory>();
        }
    }
    public class PodCategories
    {
        public List<PodCategory> ChannelCategories { get; private set; }

        public  PodCategories()
        {
            //Categories = new List<PodCategory>();

            XDocument CategoriesDocument = XDocument.Load(Path.Combine("..\\..\\..\\..\\PodNET\\Data", "Categories.xml"));

            IEnumerable<PodCategory> Categories = from Category in CategoriesDocument.Root.Elements("category")
                                                  select new PodCategory
                                                  {
                                                      Name = Category.Attribute("name").Value,
                                                      SubCategories = (from SubCategory in Category.Elements("subcategory")
                                                                       select new PodCategory
                                                                       {
                                                                           Name = (string)SubCategory.Value
                                                                       }).ToList()
                                                  };
          ChannelCategories = Categories.ToList();
        }
    }
}
