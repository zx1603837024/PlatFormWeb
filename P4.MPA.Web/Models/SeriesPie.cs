using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class SeriesPie
    {
        /// <summary>
        /// sereis序列组id
        /// </summary>
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// series序列组名称
        /// </summary>
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组呈现图表类型(line、column、bar等)
        /// </summary>
        public string type
        {
            get;
            set;
        }
        public List<EqModel> data
        {
            get;
            set;
        }


        public string radius
        {
            get;
            set;
        }

        public class EqModel
        {
            public int value
            {
                get;
                set;
            }

            public string name
            {
                get;
                set;
            }
        }
    }
}