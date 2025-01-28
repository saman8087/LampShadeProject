using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
       
        public string Name { get;   set; }
        public string Title { get;   set; }
        public string Description { get;   set; }
        public string Picture { get;   set; }
        public string PictureAlt { get;   set; }
        public string PictureTitle { get;   set; }

    
        public string MetaDescription { get;   set; }

        public string Keywords { get;   set; }
        public string Slug { get;   set; }
    }
}
